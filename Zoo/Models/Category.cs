using System;
using System.Collections.Generic;

#nullable disable

namespace Zoo.Models
{
    public partial class Category
    {
        public Category()
        {
            Items = new HashSet<Item>();
            CategoryItems = new HashSet<CategoryItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ImageId { get; set; }

        public virtual Image Image { get; set; }
        public virtual ICollection<CategoryItem> CategoryItems { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
