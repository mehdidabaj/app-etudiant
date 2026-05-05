using GestionCommandes.App.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionCommandes.App.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly AppDbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    protected Repository(AppDbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public virtual IReadOnlyList<TEntity> GetAll()
    {
        return DbSet.ToList();
    }

    public virtual TEntity? GetById(int id)
    {
        return DbSet.Find(id);
    }

    public virtual void Add(TEntity entity)
    {
        DbSet.Add(entity);
        Context.SaveChanges();
    }

    public virtual void Update(TEntity entity)
    {
        DbSet.Update(entity);
        Context.SaveChanges();
    }

    public virtual void Delete(int id)
    {
        TEntity? entity = GetById(id);

        if (entity is null)
        {
            return;
        }

        DbSet.Remove(entity);
        Context.SaveChanges();
    }
}
