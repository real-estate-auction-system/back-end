using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TypeOfRealEstate : BaseEntity
    {
        public string Name { get; set; }
        public virtual IList<RealEstate> RealEstates { get; set; }
    }
}
