using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Recyclable.Models
{
    public class RecyclableItemViewModel
    {
        public RecyclableItem RecyclableItem { get; set; }
        public string SelectedRecyclableType { get; set; } // Property for the selected type
        public string SelectedRecyclableTypeText { get; set; }
        public int SelectedRecyclableTypeId { get; set; }
        public double SelectedRecylableMinKG { get; set; }
        public double SelectedRecylableMaxKG { get; set; }
        public IEnumerable<SelectListItem> RecyclableTypes { get; set; }

        public RecyclableItemViewModel()
        {
            RecyclableItem = new RecyclableItem();
            RecyclableTypes = new List<SelectListItem>();
        }
    }



}
