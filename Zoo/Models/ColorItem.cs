using System;
using System.Collections.Generic;

#nullable disable

namespace Zoo.Models
{
    public partial class ColorItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int ColorId { get; set; }

        public virtual Color Color { get; set; }
        public virtual Item Item { get; set; }
    }
}
