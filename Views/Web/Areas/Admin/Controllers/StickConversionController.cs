using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Admin.ViewModels.StickConversion;
using KarmicEnergy.Web.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class StickConversionController : BaseController
    {
        #region Index
        [Authorize(Roles = "SuperAdmin, Admin, Operator")]
        public ActionResult Index()
        {
            var stickConversions = KEUnitOfWork.StickConversionRepository.GetAllActive().ToList();
            var viewModels = ListViewModel.Map(stickConversions);
            return View(viewModels);
        }
        #endregion Index

        #region Create
        [Authorize(Roles = "SuperAdmin, Admin, Operator")]
        public ActionResult Create()
        {
            LoadStatuses();
            LoadUnits();
            return View();
        }

        //
        // POST: /StickConversion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin, Admin, Operator")]
        public ActionResult Create(CreateViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    LoadStatuses();
                    LoadUnits();
                    return View(viewModel);
                }

                StickConversion stickConversion = new StickConversion() { Name = viewModel.Name, Status = viewModel.Status, FromUnitId = viewModel.FromUnitId, ToUnitId = viewModel.ToUnitId };
                KEUnitOfWork.StickConversionRepository.Add(stickConversion);
                KEUnitOfWork.Complete();

                return RedirectToAction("Index", "StickConversion", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            LoadStatuses();
            LoadUnits();
            return View(viewModel);
        }
        #endregion Create

        #region Edit
        [Authorize(Roles = "SuperAdmin, Admin, Operator")]
        public ActionResult Edit(Int32 id)
        {
            StickConversion stickConversion = KEUnitOfWork.StickConversionRepository.Get(id);
            var viewModel = EditViewModel.Map(stickConversion);
            LoadStatuses();
            LoadUnits();
            return View(viewModel);
        }

        //
        // POST: /StickConversion/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin, Admin, Operator")]
        public ActionResult Edit(EditViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    LoadStatuses();
                    LoadUnits();
                    return View(viewModel);
                }

                StickConversion stickConversion = KEUnitOfWork.StickConversionRepository.Get(viewModel.Id);
                stickConversion.Name = viewModel.Name;
                stickConversion.Status = viewModel.Status;
                stickConversion.FromUnitId = viewModel.FromUnitId;
                stickConversion.ToUnitId = viewModel.ToUnitId;

                KEUnitOfWork.StickConversionRepository.Update(stickConversion);
                KEUnitOfWork.Complete();

                return RedirectToAction("Index", "StickConversion", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            LoadStatuses();
            LoadUnits();
            return View(viewModel);
        }
        #endregion Edit

        #region Delete
        //
        // GET: /Customer/Delete
        [HttpGet]
        [Authorize(Roles = "SuperAdmin, Admin, Operator")]
        public ActionResult Delete(Int32 id)
        {
            try
            {
                var stickConversion = KEUnitOfWork.StickConversionRepository.Get(id);

                if (stickConversion == null)
                {
                    AddErrors("Stick Conversion does not exist");
                    return View("Index");
                }

                stickConversion.DeletedDate = DateTime.UtcNow;
                KEUnitOfWork.StickConversionRepository.Update(stickConversion);
                KEUnitOfWork.Complete();

                return RedirectToAction("Index", "StickConversion", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View();
        }
        #endregion Delete        

        #region Values

        [Authorize(Roles = "SuperAdmin, Admin, Operator")]
        public ActionResult Value()
        {
            return View();
        }

        //
        // POST: /StickConversion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin, Admin, Operator")]
        public ActionResult Value(CreateValueViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    LoadStatuses();
                    LoadUnits();
                    return View(viewModel);
                }

                StickConversionValue stickConversionValue = new StickConversionValue();
                KEUnitOfWork.StickConversionValueRepository.Add(stickConversionValue);
                KEUnitOfWork.Complete();

                return RedirectToAction("Index", "ListValue", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            LoadStatuses();
            LoadUnits();
            return View(viewModel);
        }

        #endregion Values
    }
}