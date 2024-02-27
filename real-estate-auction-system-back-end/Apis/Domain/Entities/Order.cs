using Domain.Enums;

namespace Domain.Entities
{
    public class Order:BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public double Price { get; set; }
        public OrderStatus status { get; set; }
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
        public int RealEstateId { get; set; }
        public virtual RealEstate RealEstate { get; set; }

    }
}
