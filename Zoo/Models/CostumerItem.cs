using System;
using System.Collections.Generic;

#nullable disable

namespace Zoo.Models
{
    public partial class CostumerItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int CostumerId { get; set; }

        public virtual Costumer Costumer { get; set; }
        public virtual Item Item { get; set; }
    }
}
