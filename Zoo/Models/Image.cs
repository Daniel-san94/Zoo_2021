using System;
using System.Collections.Generic;

#nullable disable

namespace Zoo.Models
{
    public partial class Image
    {
        public Image()
        {
            Categories = new HashSet<Category>();
            Costumers = new HashSet<Costumer>();
            Items = new HashSet<Item>();
            Locals = new HashSet<Local>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Costumer> Costumers { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Local> Locals { get; set; }
    }
}
