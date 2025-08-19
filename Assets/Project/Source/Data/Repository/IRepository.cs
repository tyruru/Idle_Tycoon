using System.Collections.Generic;

public interface IRepository<TEntity> where TEntity : IStringId
{
    public TEntity GetById(string id);
    public List<TEntity> GetAll();
}
