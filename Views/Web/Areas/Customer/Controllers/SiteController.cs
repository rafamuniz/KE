using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Site;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Site.Sensor;
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
            var viewModels = ListViewModel.Map(entities);
            AddLog("Navigated to Site View", LogTypeEnum.Info);
            return View(viewModels);
        }

        #endregion Index

        #region Create

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Create()
        {
            LoadStatuses();
            CreateViewModel viewModel = new CreateViewModel();
            return View(viewModel);
        }

        //
        // POST: /User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Create(CreateViewModel viewModel)
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
                site.Address = address;

                KEUnitOfWork.SiteRepository.Add(site);
                KEUnitOfWork.Complete();

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
            EditViewModel viewModel = new EditViewModel();
            viewModel.Map(site);
            viewModel.Map(site.Address);

            return View(viewModel);
        }

        //
        // POST: /User/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult Edit(EditViewModel viewModel)
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

                viewModel.MapVMToEntity(site);
                viewModel.MapVMToEntity(site.Address);

                KEUnitOfWork.SiteRepository.Update(site);
                KEUnitOfWork.Complete();

                return RedirectToAction("Index", "Site");
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

            return RedirectToAction("Index", "Site");
        }
        #endregion Delete

        #region Sensor

        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult SensorIndex()
        {
            SensorListViewModel viewModel = new SensorListViewModel();
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

            if (!IsSite && siteId.HasValue)
            {
                sensors = KEUnitOfWork.SensorRepository.GetsByCustomerAndSite(CustomerId, siteId.Value).ToList();
                sensorViewModels = SensorViewModel.Map(sensors);
                viewModel.SiteId = siteId.Value;
            }
            else
            {
                sensors = KEUnitOfWork.SensorRepository.GetsByCustomerAndSite(CustomerId, SiteId).ToList();
                sensorViewModels = SensorViewModel.Map(sensors);
                viewModel.SiteId = SiteId;
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
            SensorCreateViewModel viewModel = new SensorCreateViewModel()
            {
                SiteId = siteId
            };

            return View(LoadCreateViewModel(viewModel));
        }

        private SensorCreateViewModel LoadCreateViewModel(SensorCreateViewModel viewModel)
        {
            if (viewModel == null)
                viewModel = new SensorCreateViewModel();

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
        public ActionResult SensorCreate(SensorCreateViewModel viewModel)
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

                return RedirectToAction("SensorIndex", "Site", new { SiteId = sensor.SiteId });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            return View(LoadCreateViewModel(viewModel));
        }

        private Boolean IsValidateCreateItems(SensorCreateViewModel viewModel)
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

            SensorEditViewModel viewModel = new SensorEditViewModel();
            viewModel = SensorEditViewModel.Map(sensor);

            LoadEditViewModel(viewModel);

            var items = LoadItems();
            viewModel.Items = SensorItemViewModel.Map(items);

            if (sensor.SensorItems.Any())
            {
                List<SensorItemViewModel> selectedItems = new List<SensorItemViewModel>();

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

            return View(viewModel);
        }

        //
        // POST: /User/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, General Manager, Supervisor")]
        public ActionResult SensorEdit(SensorEditViewModel viewModel)
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

                return RedirectToAction("Index", "Sensor", new { TankId = sensor.TankId });
            }
            catch (Exception ex)
            {
                AddErrors(ex);
            }

            LoadEditViewModel(viewModel);

            return View(viewModel);
        }

        private void LoadEditViewModel(SensorEditViewModel viewModel)
        {
            LoadStatuses();
            LoadTanks(CustomerId);
            LoadSensorTypes();
        }

        private Boolean IsValidateEditItems(SensorEditViewModel viewModel)
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

            return RedirectToAction("SensorIndex");
        }

        #endregion Delete

        #endregion Sensor
    }
}
