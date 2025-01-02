﻿using DevIO.Business.Entidades;
using DevIO.Business.Interfaces;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Repositorio
{
    public abstract class Repositorio<TEntity>
        : IRepositorio<TEntity>
        where TEntity : Entity, new()
    {

        protected readonly AppDbContext Db;
        protected readonly DbSet<TEntity> DbSet;


        protected Repositorio(AppDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }


        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }


        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }


        public virtual async Task Remover(Guid id)
        {
            //var entidade = await DbSet.FindAsync(id);
            DbSet.Remove(new TEntity { Id = id}); //criando a variavel da instancia
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}