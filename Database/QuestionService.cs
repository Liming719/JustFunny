using JustFunny.Models;
using Microsoft.EntityFrameworkCore;

namespace JustFunny.Database
{
    public class QuestionService : DataService<Question>
    {
        public QuestionService(DbContext dbContext, DbSet<Question> dbSet) : base(dbContext, dbSet)
        {
        }

        public async override Task<IEnumerable<Question>> GetAsync(string action, Dictionary<string, string> requestList)
        {
            IEnumerable<Question> questionList = new List<Question>();

            await Task.Run(() =>
            {
                switch (action)
                {
                    case "GetAll":
                        questionList = this.DbSet.ToList();
                        break;
                    case "DeleteAll":
                        foreach (var id in this.DbSet.Select(e => e.ID))
                        {
                            var entity = new Question { ID = id };
                            DbSet.Attach(entity);
                            DbSet.Remove(entity);
                        }
                        DbContext.SaveChanges();
                        break;
                }
            });

            return questionList;
        }
    }
}
