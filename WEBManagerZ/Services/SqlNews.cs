using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBManagerZ.Data;
using WEBManagerZ.Models;

namespace WEBManagerZ.Services
{
    public class SqlNews
    {
        private readonly ManagerZContext _dbContexet;

        public SqlNews(ManagerZContext dbContext)
        {
            _dbContexet = dbContext;
        }

        public News Create(News news)
        {
            _dbContexet.News.Add(news);
            _dbContexet.SaveChanges();

           return new News();
        }

        public List<News> ListNews()
        {
            return _dbContexet.News.OrderBy(n => n.Date).ToList();
        }

        public News GetOne(int id)
        {
            return _dbContexet.News.FirstOrDefault(n => n.Id == id);
        }

        public News DeleteOne(News news)
        {
            _dbContexet.Remove(news);

            _dbContexet.SaveChanges();

            return new News();
        }
    }
}
