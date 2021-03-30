using System;
using System.Collections.Generic;

#nullable disable

namespace Zoo.Models
{
    public partial class SizeItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int SizeId { get; set; }

        public virtual Item Item { get; set; }
        public virtual Size Size { get; set; }
    }
}
