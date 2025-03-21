using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using WareHouseManagerWebApp.Data;
using WareHouseManagerWebApp.Model;


namespace WareHouseManagerWebApp.Service
{
    public class userService
    {
        private readonly AppDbContext _dbContext;

        public userService( AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<userModel> LoginAsync(string username, string password)
        {
            try
            {
                var user = await _dbContext.User.FirstOrDefaultAsync( u => u.Username == username && u.Password == password );
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
