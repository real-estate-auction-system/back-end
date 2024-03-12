using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.UserViewModels
{
    public class UpdateAccountRequest
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public DateTime DoB { get; set; }
    }
}
