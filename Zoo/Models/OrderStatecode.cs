using System;
using System.Collections.Generic;

#nullable disable

namespace Zoo.Models
{
    public partial class OrderStatecode
    {
        public OrderStatecode()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
