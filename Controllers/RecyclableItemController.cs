using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recyclable.Interface;
using Recyclable.Models;
using Recyclable.Repository;
using System;

namespace Recyclable.Controllers
{
    public class RecyclableItemController : Controller
    {
        public IRecyclableTypeRepository recyclableTypeRepository;
        public IRecyclableItemRepository recyclableItemRepository;
    
        public RecyclableItemController(IRecyclableTypeRepository _recyclableTypeRepository, IRecyclableItemRepository _recyclableItemRepository)
        {
            recyclableTypeRepository = _recyclableTypeRepository;
            recyclableItemRepository = _recyclableItemRepository;
        }
        // GET: RecyclableItemController
        public ActionResult List()
        {
            IEnumerable<RecyclableItem> recyclableItems = recyclableItemRepository.GetAllRecyclableItem();

            return View(recyclableItems);
        }

   
        public ActionResult Add()
        {
            var recyclableTypes = recyclableTypeRepository.GetRecyclableTypes()
            .Select(t => new SelectListItem
            {
                Value = t.Rate.ToString(),  
                Text = t.Type  
            });

            var viewModel = new RecyclableItemViewModel
            {
                RecyclableTypes = recyclableTypes
            };

            return View(viewModel);

 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(RecyclableItemViewModel viewModel)
        {
            string type = viewModel.SelectedRecyclableTypeText;
            if (ModelState.IsValid)
            {
 
                var recyclableType = recyclableTypeRepository.GetRecyclableTypeByType(type);

                var typeId = recyclableType.Id;

                if (recyclableType.MaxKg >= viewModel.RecyclableItem.Weight && 
                    recyclableType.MinKg <= viewModel.RecyclableItem.Weight )
                {
                    var recyclableItem = new RecyclableItem
                    {
                        TypeId = typeId,
                        Weight = viewModel.RecyclableItem.Weight,
                        ComputedRate = viewModel.RecyclableItem.ComputedRate,
                        ItemDescription = viewModel.RecyclableItem.ItemDescription
                    };

                    recyclableItemRepository.AddRecyclableItem(recyclableItem);

                    return RedirectToAction("List");
                }
                
            }

            viewModel.RecyclableTypes = recyclableTypeRepository.GetRecyclableTypes()
           .Select(t => new SelectListItem { Value = t.Rate.ToString(), Text = t.Type });

            return View(viewModel);
        }


        public ActionResult Edit(int id)
        {

            RecyclableItem recyclableItem = recyclableItemRepository.GetRecyclableItem(id);

            IEnumerable<RecyclableType> recyclableTypes = recyclableTypeRepository.GetRecyclableTypes();

            RecyclableItemViewModel viewModel = new RecyclableItemViewModel
            {
                
                SelectedRecyclableTypeId = id,
                RecyclableItem = recyclableItem,
                RecyclableTypes = recyclableTypes.Select(t => new SelectListItem
                {
                    Value = t.Rate.ToString(), 
                    Text = t.Type  
                })
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RecyclableItemViewModel viewModel)
        {
          
            string type = viewModel.SelectedRecyclableTypeText;

            if (ModelState.IsValid)
            {

                var recyclableType = recyclableTypeRepository.GetRecyclableTypeByType(type);

                var typeId = recyclableType.Id;

                if (recyclableType.MaxKg >= viewModel.RecyclableItem.Weight &&
                    recyclableType.MinKg <= viewModel.RecyclableItem.Weight)
                {

                    var recyclableItem = new RecyclableItem
                    {
                        Id = viewModel.SelectedRecyclableTypeId,
                        TypeId = typeId,
                        Weight = viewModel.RecyclableItem.Weight,
                        ComputedRate = viewModel.RecyclableItem.ComputedRate,
                        ItemDescription = viewModel.RecyclableItem.ItemDescription
                    };


                    recyclableItemRepository.EditRecyclableItem(recyclableItem);

                    return RedirectToAction("List");
                }
                
            }

            viewModel.RecyclableTypes = recyclableTypeRepository.GetRecyclableTypes()
          .Select(t => new SelectListItem { Value = t.Rate.ToString(), Text = t.Type });

            return View(viewModel);

            

     
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
             
            RecyclableItem recyclableItem = recyclableItemRepository.GetRecyclableItem(id);

            if (recyclableItem == null)
            {
                 
                return NotFound();
            }

            return View(recyclableItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmation(int id)
        {

            recyclableItemRepository.DeleteRecyclableItem(id);

            return RedirectToAction("List");

        }

      
    }
}
