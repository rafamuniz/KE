using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Admin.ViewModels.TankModel;
using KarmicEnergy.Web.Controllers;
using Munizoft.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class TankModelController : BaseController
    {
        #region Index
        [Authorize(Roles = "SuperAdmin, Admin, Operator")]
        public ActionResult Index()
        {
            var tankModels = KEUnitOfWork.TankModelRepository.GetAllActive().ToList();
            var viewModels = ListViewModel.Map(tankModels);
            return View(viewModels);
        }
        #endregion Index

        #region Create
        [Authorize(Roles = "SuperAdmin, Admin, Operator")]
        public ActionResult Create()
        {
            LoadStatuses();
            return View();
        }

        //
        // POST: /Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin, Admin, Operator")]
        public ActionResult Create(CreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                LoadStatuses();
                return View(viewModel);
            }
            else if (!viewModel.Image.HasFile())
            {
                AddErrors("Image", "The Image field is required.");
                LoadStatuses();
                return View(viewModel);
            }

            try
            {
                String extension = Path.GetExtension(viewModel.Image.FileName);
                String newFileName = String.Format("{0}{1}", Guid.NewGuid(), extension);
                String pathFilename = Server.MapPath(String.Format("~/images/tank_models/{0}", newFileName));
                viewModel.Image.SaveAs(pathFilename);

                Byte[] image = viewModel.Image.ToByte();
                TankModel tankModel = new TankModel() { Name = viewModel.Name, Status = viewModel.Status, ImageFilename = newFileName, Image = image };
                KEUnitOfWork.TankModelRepository.Add(tankModel);
                KEUnitOfWork.Complete();

                return RedirectToAction("Index", "TankModel", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View(viewModel);
        }
        #endregion Create

        #region Edit
        [Authorize(Roles = "SuperAdmin, Admin, Operator")]
        public ActionResult Edit(Int32 id)
        {
            TankModel tankModel = KEUnitOfWork.TankModelRepository.Get(id);
            var viewModel = EditViewModel.Map(tankModel);
            LoadStatuses();
            return View(viewModel);
        }

        //
        // POST: /Customer/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin, Admin, Operator")]
        public ActionResult Edit(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                LoadStatuses();
                return View(viewModel);
            }

            try
            {
                TankModel tankModel = KEUnitOfWork.TankModelRepository.Get(viewModel.Id);
                tankModel.Name = viewModel.Name;
                tankModel.Status = viewModel.Status;

                if (viewModel.Image.HasFile())
                {
                    // Move Old Image
                    String originalPathFilename = Server.MapPath(String.Format("{0}{1}", "~/images/tank_models/", tankModel.ImageFilename));
                    String destPathFilename = Server.MapPath(String.Format("{0}{1}_{2}", "~/images/tank_models/delete/", tankModel.Id, tankModel.ImageFilename));
                    System.IO.File.Move(originalPathFilename, destPathFilename);

                    String extension = Path.GetExtension(viewModel.Image.FileName);
                    String newFileName = String.Format("{0}{1}", Guid.NewGuid(), extension);
                    String pathFilename = Server.MapPath(String.Format("~/images/tank_models/{0}", newFileName));
                    viewModel.Image.SaveAs(pathFilename);
                    Byte[] image = viewModel.Image.ToByte();
                    tankModel.ImageFilename = newFileName;
                    tankModel.Image = image;
                }

                KEUnitOfWork.TankModelRepository.Update(tankModel);
                KEUnitOfWork.Complete();

                return RedirectToAction("Index", "TankModel", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

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
                var tankModel = KEUnitOfWork.TankModelRepository.Get(id);

                if (tankModel == null)
                {
                    AddErrors("Tank Model does not exist");
                    return View("Index");
                }

                String originalPathFilename = Server.MapPath(String.Format("{0}{1}", "~/images/tank_models/", tankModel.ImageFilename));
                String destPathFilename = Server.MapPath(String.Format("{0}{1}", "~/images/tank_models/delete/", tankModel.ImageFilename));
                System.IO.File.Move(originalPathFilename, destPathFilename);
                tankModel.DeletedDate = DateTime.UtcNow;

                //KEUnitOfWork.TankModelRepository.Remove(tankModel);
                KEUnitOfWork.TankModelRepository.Update(tankModel);
                KEUnitOfWork.Complete();

                return RedirectToAction("Index", "TankModel", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View();
        }
        #endregion Delete        
    }
}