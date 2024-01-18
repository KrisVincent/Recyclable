using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recyclable.Interface;
using Recyclable.Models;
using Recyclable.Repository;

namespace Recyclable.Controllers
{
    public class RecyclableTypeController : Controller
    {
        public IRecyclableTypeRepository recyclableTypeRepository;
        public RecyclableTypeController(IRecyclableTypeRepository _recyclableTypeRepository)
        {
            recyclableTypeRepository = _recyclableTypeRepository;
        }

 
        public IActionResult List()
        {
            IEnumerable<RecyclableType> recyclableTypes = recyclableTypeRepository.GetRecyclableTypes();

            return View(recyclableTypes);
        }

 
        public IActionResult Add()
        {
            return View();
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(RecyclableType recyclableType)
        {
            if (recyclableType.MaxKg >= recyclableType.MinKg &&
                recyclableType.MaxKg > 0 && recyclableType.MinKg > 0 &&
                recyclableType.Rate > 0)
            { 
                recyclableTypeRepository.AddRecyclableType(recyclableType);

                return RedirectToAction("List");
            }

            
            return View(recyclableType);

        }

        public IActionResult Edit(int id)
        {

            var recyclableType = recyclableTypeRepository.GetRecyclableType(id);


            return View(recyclableType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RecyclableType recyclableType)
        {
             
            if (recyclableType.MaxKg >= recyclableType.MinKg &&
                recyclableType.MaxKg > 0 && recyclableType.MinKg > 0 &&
                recyclableType.Rate > 0)
            {
                recyclableTypeRepository.UpdateRecycleType(recyclableType);

                return RedirectToAction("List");
            }

            return View(recyclableType);
        }
 
        public IActionResult ItemList(int id)
        {
            // Get recyclable items based on the type id
            var recyclableItems = recyclableTypeRepository.GetRecyclableItems(id);

             

            return View(recyclableItems);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
      
            RecyclableType recyclableType= recyclableTypeRepository.GetRecyclableType(id);
 
            if (recyclableType == null)
            {
                 
                return NotFound();
            }

            return View(recyclableType);
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmation(int id)
        {

            recyclableTypeRepository.DeleteRecycleType(id);


            return RedirectToAction("List");

        }
    }
}
