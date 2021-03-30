using System;
using System.Collections.Generic;

#nullable disable

namespace Zoo.Models
{
    public partial class LocalItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int LocalId { get; set; }

        public virtual Item Item { get; set; }
        public virtual Local Local { get; set; }
    }
}
