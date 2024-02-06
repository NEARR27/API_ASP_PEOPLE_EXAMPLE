﻿namespace PeopleApi.Repository
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> Get();

        Task<TEntity> GetById(int id);

        Task Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task Saver();

        IEnumerable<TEntity> Search(Func<TEntity, bool> filter);
    }
    
}
