using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class News : BaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual IList<NewsImage> NewsImages { get; set; }

        //public string image { get; set; }
        public DateTime time { get; set; }
        public NewsStatus Status { get; set; }
        public int AccountId { get; set; }
        public virtual Account Account { get; set; } 
    }
}
