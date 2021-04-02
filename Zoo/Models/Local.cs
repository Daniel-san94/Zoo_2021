using System;
using System.Collections.Generic;

#nullable disable

namespace Zoo.Models
{
    public partial class Local
    {
        public Local()
        {
            AspNetUsers = new HashSet<AspNetUser>();
            Items = new HashSet<Item>();
            LocalItems = new HashSet<LocalItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int? ImageId { get; set; }

        public virtual Image Image { get; set; }
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<LocalItem> LocalItems { get; set; }
    }
}
