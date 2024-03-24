using DataAccessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAOs
{
    public class UserAccountDAO
    {
        public async Task<UserAccount> Login(string email, string password)
        {
            UserAccount userAccount;
            using (var context = new WatercolorsPainting2024DBContext())
            {
                userAccount = await context.UserAccounts.Where(x => x.UserEmail.ToLower().Equals(email.ToLower()) && x.UserPassword.Equals(password)).FirstOrDefaultAsync();
            }
            return userAccount;
        }
    }
}
