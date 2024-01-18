using System;
using System.Collections.Generic;

namespace Recyclable.Models
{
    public partial class RecyclableItem
    {
        public int Id { get; set; }
        public int? TypeId { get; set; }
        public decimal? Weight { get; set; }
        public decimal? ComputedRate { get; set; }
        public string? ItemDescription { get; set; }

        public virtual RecyclableType? Type { get; set; }
    }
}
