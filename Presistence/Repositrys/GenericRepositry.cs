﻿using Domain.ContractInterFaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositrys
{
    public class GenericRepositry<TEntity, Tkey>(StoreDBContext context) : IGenericRepository<TEntity, Tkey>
        where TEntity:ModelBase<Tkey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        => await context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetByIdAsync(Tkey id)
        => await context.Set<TEntity>().FindAsync(id);

        public void Add(TEntity entity)
        =>context.Set<TEntity>().Add(entity);

        public void Update(TEntity entity)
         => context.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity)
         => context.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> Spec)
        {
            return await SpecificationEvaluator.CreateQuery(context.Set<TEntity>(), Spec).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(ISpecifications<TEntity, Tkey> Spec)
        {
            return await SpecificationEvaluator.CreateQuery(context.Set<TEntity>(), Spec).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecifications<TEntity, Tkey> Spec)
         => await SpecificationEvaluator.CreateQuery(context.Set<TEntity>(),Spec).CountAsync();
           
        
    }
}
