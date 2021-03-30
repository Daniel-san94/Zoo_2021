using System;
using System.Collections.Generic;

#nullable disable

namespace Zoo.Models
{
    public partial class Item
    {
        public Item()
        {
            CategoryItems = new HashSet<CategoryItem>();
            ColorItems = new HashSet<ColorItem>();
            CostumerItems = new HashSet<CostumerItem>();
            LocalItems = new HashSet<LocalItem>();
            OrderItems = new HashSet<OrderItem>();
            SizeItems = new HashSet<SizeItem>();
        }

        public int Id { get; set; }
        public int ImageId { get; set; }
        public int LocalId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Image Image { get; set; }
        public virtual Local Local { get; set; }
        public virtual ICollection<CategoryItem> CategoryItems { get; set; }
        public virtual ICollection<ColorItem> ColorItems { get; set; }
        public virtual ICollection<CostumerItem> CostumerItems { get; set; }
        public virtual ICollection<LocalItem> LocalItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<SizeItem> SizeItems { get; set; }
    }
}
