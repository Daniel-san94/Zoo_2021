using System;
using System.Collections.Generic;

#nullable disable

namespace Zoo.Models
{
    public partial class Costumer
    {
        public Costumer()
        {
            CostumerItems = new HashSet<CostumerItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ImageId { get; set; }

        public virtual Image Image { get; set; }
        public virtual ICollection<CostumerItem> CostumerItems { get; set; }
    }
}
