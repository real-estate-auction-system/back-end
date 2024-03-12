using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.UserViewModels
{
    public class AccountResponse
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime DoB { get; set; }
    }
}
