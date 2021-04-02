using System;
using System.Collections.Generic;

#nullable disable

namespace Zoo.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int OrderStatecodeId { get; set; }

        public virtual OrderStatecode OrderStatecode { get; set; }

        //Ehelyett majd AspNetUser kell
        //public virtual User User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
