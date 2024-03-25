using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class NewsImage : BaseEntity
    {
        public string ImageURL { get; set; }
        public int NewsId { get; set; }
        public virtual News News { get; set; }
    }
}
