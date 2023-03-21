using Microsoft.EntityFrameworkCore;

namespace JustFunny.Database
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAsync(string action, Dictionary<string, string> requestList);
        Task Insert(T entity);
        Task<bool> Update(T entity);
    }
    public abstract class DataService<T> : IDataService<T> where T : class
    {
        public DbContext DbContext { get; set; }
        public DbSet<T> DbSet { get; set; }
        public DataService(DbContext dbContext, DbSet<T> dbSet)
        {
            DbContext = dbContext;
            DbSet = dbSet;
        }
        public abstract Task<IEnumerable<T>> GetAsync(string action, Dictionary<string, string> requestList);

        public async Task Insert(T entity)
        {
            this.DbSet.Add(entity);
            await this.DbContext.SaveChangesAsync();
        }

        public async Task<bool> Update(T entity)
        {
            // retrieve the existing entity from the database
            var existingEntity = this.DbSet.Find(entity);

            // check if the entity was found
            if (existingEntity != null)
            {
                // detach the existing entity from the DbContext
                this.DbContext.Entry(existingEntity).State = EntityState.Detached;

                // attach the updated entity to the DbContext and mark it as modified
                this.DbSet.Update(entity);

                // save the changes to the database
                await this.DbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
