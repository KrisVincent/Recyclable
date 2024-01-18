using System;
using System.Collections.Generic;

namespace Recyclable.Models
{
    public partial class RecyclableType
    {
        public RecyclableType()
        {
            RecyclableItems = new HashSet<RecyclableItem>();
        }

        public int Id { get; set; }
        public string? Type { get; set; }
        public decimal? Rate { get; set; }
        public decimal? MinKg { get; set; }
        public decimal? MaxKg { get; set; }

        public virtual ICollection<RecyclableItem> RecyclableItems { get; set; }
    }
}
