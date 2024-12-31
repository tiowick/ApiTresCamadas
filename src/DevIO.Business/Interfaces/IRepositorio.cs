﻿using DevIO.Business.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface IRepositorio<TEntity>  //entidade generica
        : IDisposable where TEntity : Entity
       
    {

        Task Adicionar(TEntity entity);

        Task<TEntity> ObterPorId(Guid id);

        Task<List<TEntity>> ObterTodos();

        Task Atualizar(TEntity entity);

        Task Remover(TEntity entity);

        Task<int> SaveChanges();

        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);   

    }
}
