using Microsoft.EntityFrameworkCore;
using JustFunny.Models;

namespace JustFunny.Database
{
    public class UserService : DataService<User>
    {
        public UserService(DbContext dbContext, DbSet<User> dbSet) : base(dbContext, dbSet)
        {
        }

        public override async Task<IEnumerable<User>> GetAsync(string action, Dictionary<string, string> requestList)
        {
            IEnumerable<User> userList = new List<User>();

            await Task.Run(() =>
            {
                switch (action)
                {
                    case "GetByName":
                        string userName = string.Empty;
                        if (requestList.TryGetValue("name", out userName))
                            userList = GetUserByName(userName);                        
                        break;
                }
            });

            return userList;
        }

        private IEnumerable<User> GetUserByName(string userName)
        {
            return this.DbSet.Where(x => x.Name == userName);
        }
    }
}
