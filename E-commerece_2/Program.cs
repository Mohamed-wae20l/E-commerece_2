
using Abstraction;
using Azure;
using Domain.ContractInterFaces;
using E_commerece_2.CustomlizeMiddelWere;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Repositrys;
using Presistence.Data;
using Sarvices;
using Sarvices.MappingProfiles;
using Shareds.ErrorMiddelWare;
using StackExchange.Redis;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace E_commerece_2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<StoreDBContext>(options =>
            {
                var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(ConnectionString);
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //services for me
            builder.Services.AddScoped<IDBInializer, DBinalizer>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);
            builder.Services.AddScoped<IServicesManger,ServicesManger>();

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (Context) =>
                {
                    var Errors = Context.ModelState.Where(M => M.Value.Errors.Any())
                              .Select(M => new ValidationError()
                              {
                                 Field = M.Key,
                                 Errors = M.Value.Errors.Select(E => E.ErrorMessage)
                              });
                              //---------------------------
                              var Response = new ValidationErrorToReturn()
                              {
                                  ValidationErrors = Errors
                              };
                              //----------------------------
                              return new BadRequestObjectResult(Response);


                };
            });
            builder.Services.AddScoped<IBasketRepository, BasketRepostiory>();
            builder.Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisconnectionString"));
            });

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            //
            app.UseMiddleware<CustomExceptionMiddleWare>();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app .UseStaticFiles();//????? ?????
            app.UseAuthorization();

            
            #region Update-database ????? ?????
            using var scope = app.Services.CreateScope();
            var dbinializer = scope.ServiceProvider.GetRequiredService<IDBInializer>();
            await dbinializer.InializeAsync(); 
            #endregion
           
            app.MapControllers();

            app.Run();
        }
       
    }
}
