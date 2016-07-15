using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Sensor;
using KarmicEnergy.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
    [Authorize]
    public class SiteController : BaseController
    {
        #region Index
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Index()
        {
            List<Site> entities = LoadSites();
            var viewModels = ViewModels.Site.ListViewModel.Map(entities);
            AddLog("Navigated to Site View", LogTypeEnum.Info);
            return View(viewModels);
        }

        #endregion Index

        #region Create

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Create()
        {
            LoadStatuses();
            ViewModels.Site.CreateViewModel viewModel = new ViewModels.Site.CreateViewModel();
            AddLog("Navigated to Creat Site View", LogTypeEnum.Info);
            return View(viewModel);
        }

        //
        // POST: /User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Create(ViewModels.Site.CreateViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    LoadStatuses();
                    return View(viewModel);
                }

                Site site = viewModel.Map();
                site.CustomerId = CustomerId;

                Core.Entities.Address address = viewModel.MapAddress();
                address.Id = Guid.NewGuid();
                site.Address = address;
                site.AddressId = address.Id;

                KEUnitOfWork.SiteRepository.Add(site);
                KEUnitOfWork.Complete();

                AddLog("Site Created", LogTypeEnum.Info);
                return RedirectToAction("Index", "Site", new { area = "Customer" });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            LoadStatuses();
            return View(viewModel);
        }
        #endregion Create

        #region Edit

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Edit(Guid id)
        {
            var site = KEUnitOfWork.SiteRepository.Get(id);

            if (site == null)
            {
                AddErrors("Site does not exist");
                return View("Index");
            }

            LoadStatuses();
            ViewModels.Site.EditViewModel viewModel = new ViewModels.Site.EditViewModel();
            viewModel.MapEntityToVM(site);
            viewModel.MapEntityToVM(site.Address);

            AddLog("Navigated to Edit Site View", LogTypeEnum.Info);
            return View(viewModel);
        }

        //
        // POST: /User/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Edit(ViewModels.Site.EditViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    LoadStatuses();
                    return View(viewModel);
                }

                var site = KEUnitOfWork.SiteRepository.Get(viewModel.Id);

                if (site == null)
                {
                    LoadStatuses();
                    AddErrors("Site does not exist");
                    return View("Index");
                }

                //viewModel.MapVMToEntity(site);
                //viewModel.MapVMToEntity(site.Address);

                Core.Entities.Address address = viewModel.Address.Map(site.Address);
                Core.Entities.Site s = viewModel.Map(site);
                site.Address = address;

                KEUnitOfWork.SiteRepository.Update(s);
                KEUnitOfWork.Complete();

                AddLog("Site Updated", LogTypeEnum.Info);
                return RedirectToAction("Index", "Site", new { area = "Customer" });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            LoadStatuses();
            return View(viewModel);
        }
        #endregion Edit

        #region Delete
        //
        // GET: /User/Delete
        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Delete(Guid id)
        {
            var site = KEUnitOfWork.SiteRepository.Get(id);

            if (site == null)
            {
                AddErrors("Site does not exist");
                return View("Index");
            }

            site.DeletedDate = DateTime.UtcNow;
            KEUnitOfWork.SiteRepository.Update(site);
            KEUnitOfWork.Complete();

            AddLog("Site Deleted", LogTypeEnum.Info);
            return RedirectToAction("Index", "Site");
        }
        #endregion Delete

        #region Sensor

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult SensorIndex()
        {
            ViewModels.Sensor.ListViewModel viewModel = new ViewModels.Sensor.ListViewModel();
            List<SensorViewModel> sensorViewModels = new List<SensorViewModel>();
            List<Sensor> sensors = null;
            Guid? siteId = null;

            if (Request.QueryString["SiteId"] != null)
            {
                Guid sId;
                if (Guid.TryParse(Request.QueryString["SiteId"], out sId))
                {
                    siteId = sId;
                }
            }

            viewModel.SiteId = !IsSite && siteId.HasValue ? siteId.Value : SiteId;

            if (viewModel.SiteId.HasValue)
            {
                sensors = KEUnitOfWork.SensorRepository.GetsByCustomerAndSite(CustomerId, viewModel.SiteId.Value).ToList();
                sensorViewModels = SensorViewModel.Map(sensors);
            }

            viewModel.Sensors = sensorViewModels;
            LoadSites();

            AddLog("Navigated to Sensor of Site View", LogTypeEnum.Info);
            return View(viewModel);
        }

        #region Create

        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult SensorCreate(Guid siteId)
        {
            ViewModels.Sensor.CreateViewModel viewModel = new ViewModels.Sensor.CreateViewModel()
            {
                SiteId = siteId
            };

            AddLog("Navigated to Create Sensor of Site View", LogTypeEnum.Info);
            return View(LoadCreateViewModel(viewModel));
        }

        private ViewModels.Sensor.CreateViewModel LoadCreateViewModel(ViewModels.Sensor.CreateViewModel viewModel)
        {
            if (viewModel == null)
                viewModel = new ViewModels.Sensor.CreateViewModel();

            if (!IsSite)
            {
                LoadSites();
            }
            else
            {
                viewModel.SiteId = SiteId;
            }

            LoadSensorTypes();
            LoadStatuses();

            return viewModel;
        }

        //
        // POST: /Sensor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult SensorCreate(ViewModels.Sensor.CreateViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(LoadCreateViewModel(viewModel));
                }

                // Validate Item check=true
                if (!IsValidateCreateItems(viewModel))
                {
                    return View(LoadCreateViewModel(viewModel));
                }

                Sensor sensor = new Sensor()
                {
                    Name = viewModel.Name,
                    SiteId = viewModel.SiteId,
                    SensorTypeId = viewModel.SensorTypeId,
                    Status = viewModel.Status,
                    Reference = viewModel.Reference
                };

                if (viewModel.Items.Any())
                {
                    foreach (var item in viewModel.Items)
                    {
                        if (item.IsSelected)
                        {
                            SensorItem sensorItem = new SensorItem()
                            {
                                ItemId = item.Id,
                                UnitId = item.UnitSelected.Value
                            };

                            sensor.SensorItems.Add(sensorItem);
                        }
                    }
                }

                KEUnitOfWork.SensorRepository.Add(sensor);
                KEUnitOfWork.Complete();

                AddLog("Sensor of Site Created", LogTypeEnum.Info);
                return RedirectToAction("SensorIndex", "Site", new { SiteId = sensor.SiteId });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View(LoadCreateViewModel(viewModel));
        }

        private Boolean IsValidateCreateItems(ViewModels.Sensor.CreateViewModel viewModel)
        {
            Boolean flag = true;

            if (viewModel.Items.Any())
            {
                foreach (var item in viewModel.Items)
                {
                    if (item.IsSelected && !item.UnitSelected.HasValue)
                    {
                        AddErrors(String.Format("Unit is required for {0}", item.Name));
                        flag = false;
                    }
                }
            }

            return flag;
        }
        #endregion Create

        #region Edit

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult SensorEdit(Guid id)
        {
            var sensor = KEUnitOfWork.SensorRepository.Get(id);

            if (sensor == null)
            {
                AddErrors("Sensor does not exist");
                return View("Index");
            }

            ViewModels.Sensor.EditViewModel viewModel = new ViewModels.Sensor.EditViewModel();
            viewModel = ViewModels.Sensor.EditViewModel.Map(sensor);

            LoadEditViewModel(viewModel);

            var items = LoadItems();
            viewModel.Items = ItemViewModel.Map(items);

            if (sensor.SensorItems.Any())
            {
                List<ItemViewModel> selectedItems = new List<ItemViewModel>();

                foreach (var item in sensor.SensorItems)
                {
                    foreach (var avalItem in viewModel.Items)
                    {
                        if (item.ItemId == avalItem.Id)
                        {
                            avalItem.IsSelected = true;
                            avalItem.UnitSelected = item.UnitId;
                        }
                    }
                }
            }

            AddLog("Navigated to Edit Sensor of Site View", LogTypeEnum.Info);
            return View(viewModel);
        }

        //
        // POST: /User/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult SensorEdit(ViewModels.Sensor.EditViewModel viewModel)
        {
            Sensor sensor = null;

            try
            {
                if (!ModelState.IsValid)
                {
                    LoadEditViewModel(viewModel);
                    AddErrors("Sensor does not exist");
                    return View(viewModel);
                }

                // Validate Item check=true
                if (!IsValidateEditItems(viewModel))
                {
                    LoadEditViewModel(viewModel);
                    return View(viewModel);
                }

                sensor = KEUnitOfWork.SensorRepository.Get(viewModel.Id);

                sensor.Name = viewModel.Name;
                sensor.SiteId = viewModel.SiteId;
                sensor.SensorTypeId = viewModel.SensorTypeId;
                sensor.Status = viewModel.Status;
                sensor.Reference = viewModel.Reference;

                if (viewModel.Items.Any())
                {
                    foreach (var item in viewModel.Items)
                    {
                        if (item.IsSelected)
                        {
                            var hasSensorItem = sensor.SensorItems.Where(x => x.ItemId == item.Id && x.DeletedDate == null).SingleOrDefault();

                            if (hasSensorItem == null) // ADD
                            {
                                SensorItem sensorItem = new SensorItem()
                                {
                                    ItemId = item.Id,
                                    UnitId = item.UnitSelected.Value
                                };

                                sensor.SensorItems.Add(sensorItem);
                            }
                            else if (hasSensorItem.UnitId != item.UnitSelected.Value) // UPDATE
                            {
                                sensor.SensorItems.Where(x => x.ItemId == item.Id && x.DeletedDate == null).SingleOrDefault().UnitId = item.UnitSelected.Value;
                            }
                        }
                        else // DELETE
                        {
                            var hasSensorItem = sensor.SensorItems.Where(x => x.ItemId == item.Id && x.DeletedDate == null).SingleOrDefault();

                            if (hasSensorItem != null)
                            {
                                hasSensorItem.DeletedDate = DateTime.UtcNow;
                            }
                        }
                    }
                }

                KEUnitOfWork.SensorRepository.Update(sensor);
                KEUnitOfWork.Complete();

                AddLog("Sensor of Site Updated", LogTypeEnum.Info);
                return RedirectToAction("SensorIndex", "Site", new { SiteId = sensor.SiteId });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            LoadEditViewModel(viewModel);

            return View(viewModel);
        }

        private void LoadEditViewModel(ViewModels.Sensor.EditViewModel viewModel)
        {
            if (!IsSite)
            {
                LoadSites();
            }
            else
            {
                viewModel.SiteId = SiteId;
            }

            LoadStatuses();
            LoadSensorTypes();
        }

        private Boolean IsValidateEditItems(ViewModels.Sensor.EditViewModel viewModel)
        {
            Boolean flag = true;

            if (viewModel.Items.Any())
            {
                foreach (var item in viewModel.Items)
                {
                    if (item.IsSelected && !item.UnitSelected.HasValue)
                    {
                        AddErrors(String.Format("Unit is required for {0}", item.Name));
                        flag = false;
                    }
                }
            }

            return flag;
        }

        #endregion Edit

        #region Delete
        //
        // GET: /Site/SensorDelete
        [HttpGet]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult SensorDelete(Guid id)
        {
            var sensor = KEUnitOfWork.SensorRepository.Get(id);

            if (sensor == null)
            {
                AddErrors("Sensor does not exist");
                return View("Index");
            }

            sensor.DeletedDate = DateTime.UtcNow;

            foreach (var item in sensor.SensorItems)
            {
                item.DeletedDate = DateTime.UtcNow;
            }

            KEUnitOfWork.SensorRepository.Update(sensor);
            KEUnitOfWork.Complete();

            AddLog("Sensor of Site Deleted", LogTypeEnum.Info);
            return RedirectToAction("SensorIndex", "Site", new { SiteId = sensor.SiteId });
        }

        #endregion Delete

        #endregion Sensor
    }
}
