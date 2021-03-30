using System;
using System.Collections.Generic;

#nullable disable

namespace Zoo.Models
{
    public partial class Color
    {
        public Color()
        {
            ColorItems = new HashSet<ColorItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ColorItem> ColorItems { get; set; }
    }
}
