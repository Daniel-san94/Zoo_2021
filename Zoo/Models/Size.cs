using System;
using System.Collections.Generic;

#nullable disable

namespace Zoo.Models
{
    public partial class Size
    {
        public Size()
        {
            SizeItems = new HashSet<SizeItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<SizeItem> SizeItems { get; set; }
    }
}
