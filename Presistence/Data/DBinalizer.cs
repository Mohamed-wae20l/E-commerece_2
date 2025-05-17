using Domain.ContractInterFaces;
using Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public class DBinalizer(StoreDBContext context) : IDBInializer
    {
        
        public async Task InializeAsync()
        {
            
            #region  Update-database بتعمل اتمتك
            if ((await context.Database.GetPendingMigrationsAsync()).Any())
            {
                await context.Database.MigrateAsync();
            } 
            #endregion

            #region بنضيف داتا بس من ملفات خارجيه 
            try
            {
                if (!context.Set<ProductBrand>().Any())
                {
                   

                    var data = await File.ReadAllTextAsync(@"..\Presistence\Data\Seads\brands.json");
                    var Objects = JsonSerializer.Deserialize<List<ProductBrand>>(data);//بحول من jsonالي list

                    if (Objects is not null && Objects.Any())
                    {
                        context.Set<ProductBrand>().AddRange(Objects);
                        await context.SaveChangesAsync();
                    }
                }
                if (!context.Set<ProductType>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Presistence\Data\Seads\types.json");
                    var Objects = JsonSerializer.Deserialize<List<ProductType>>(data);

                    if (Objects is not null && Objects.Any())
                    {
                        context.Set<ProductType>().AddRange(Objects);
                        await context.SaveChangesAsync();
                    }
                }
                if (!context.Set<Product>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Presistence\Data\Seads\products.json");
                    var Objects = JsonSerializer.Deserialize<List<Product>>(data);

                    if (Objects is not null && Objects.Any())
                    {
                        context.Set<Product>().AddRange(Objects);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                //code
            }
            #endregion
        }
       
    }
}
