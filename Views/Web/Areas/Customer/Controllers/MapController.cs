using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Services.Interface;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Map;
using KarmicEnergy.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace KarmicEnergy.Web.Areas.Customer.Controllers
{
    [Authorize]
    public class MapController : BaseController
    {
        #region Fields
        private readonly ISiteService _siteService;
        private readonly IPondService _pondService;
        private readonly ITankService _tankService;
        private readonly ISensorService _sensorService;
        private readonly ISensorItemService _sensorItemService;
        private readonly ISensorItemEventService _sensorItemEventService;
        private readonly IAlarmService _alarmService;
        #endregion Fields

        #region Constructor

        public MapController(ISiteService siteService, IPondService pondService, ITankService tankService
            , ISensorService sensorService, ISensorItemService sensorItemService, ISensorItemEventService sensorItemEventService
            , IAlarmService alarmService)
        {
            this._siteService = siteService;
            this._pondService = pondService;
            this._tankService = tankService;
            this._sensorService = sensorService;
            this._sensorItemService = sensorItemService;
            this._sensorItemEventService = sensorItemEventService;
            this._alarmService = alarmService;
        }

        #endregion Constructor

        #region Index

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult Index()
        {
            IndexViewModel viewModel = new IndexViewModel();

            IEnumerable<Site> sites;
            if (User.IsInRole("General Manager") || User.IsInRole("Customer"))
            {
                if (IsSite)
                {
                    viewModel.SiteId = SiteId;
                    var site = _siteService.Get(SiteId);
                    viewModel.Map(site);
                }
                else
                {
                    sites = this._siteService.GetsByCustomer(CustomerId);
                    viewModel.Sites = SiteViewModel.Map(sites);
                }
            }
            else if(!User.IsInRole("General Manager") && !User.IsInRole("Customer"))
            {
                sites = this._siteService.GetsSiteByUser(Guid.Parse(UserId));
                viewModel.Sites = SiteViewModel.Map(sites);
            }            

            AddLog("Navigated to Map View", LogTypeEnum.Info);
            return View(viewModel);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult SiteSelected(Guid siteId)
        {
            IndexViewModel viewModel = new IndexViewModel();
            var site = this._siteService.Get(siteId);
            viewModel.Map(site);

            var sites = this._siteService.GetsByCustomer(CustomerId);
            viewModel.Sites = SiteViewModel.Map(sites);
            viewModel.SiteId = siteId;

            return View("Index", viewModel);
        }

        #endregion Index       

        #region Json

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetSite(Guid siteId)
        {
            var site = _siteService.Get(siteId);
            ViewModels.Map.SiteViewModel viewModel = ViewModels.Map.SiteViewModel.Map(site);

            var siteAlarms = this._alarmService.GetsBySiteWithTrigger(viewModel.Id);
            viewModel.Alarms = AlarmViewModel.Map(siteAlarms);

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetSiteWithInfo(Guid siteId)
        {
            var sensor = this._siteService.Get(siteId);
            ViewModels.Map.SiteViewModel viewModel = ViewModels.Map.SiteViewModel.Map(sensor);

            // Alarm
            var siteAlarms = this._alarmService.GetsBySiteWithTrigger(siteId);
            viewModel.Alarms = AlarmViewModel.Map(siteAlarms);

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetsPond(Guid siteId)
        {
            var ponds = this._pondService.GetsBySite(siteId);
            IList<PondViewModel> viewModels = PondViewModel.Map(ponds);

            if (viewModels.Any())
            {
                foreach (var pondViewModel in viewModels)
                {
                    var pondAlarms = this._alarmService.GetsByPondWithTrigger(pondViewModel.Id);
                    pondViewModel.Alarms = AlarmViewModel.Map(pondAlarms);
                }
            }

            return Json(viewModels, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetPondWithInfo(Guid pondId)
        {
            var pond = this._pondService.Get(pondId);
            PondViewModel viewModel = PondViewModel.Map(pond);

            if (this._sensorService.HasSensorTank(viewModel.Id) &&
                this._sensorItemService.HasTankSensorItem(viewModel.Id, ItemEnum.WaterVolume))
            {
                var waterVolumesLastEvent = this._sensorItemEventService.GetLastEventByTankAndItem(viewModel.Id, ItemEnum.WaterVolume);

                if (waterVolumesLastEvent != null)
                {
                    viewModel.WaterVolumeLastId = waterVolumesLastEvent.Id;
                    viewModel.WaterVolumeLastValue = Decimal.Parse(waterVolumesLastEvent.Value);
                    viewModel.WaterVolumeLastDate = waterVolumesLastEvent.EventDate;
                    viewModel.WaterVolumeLastUnit = waterVolumesLastEvent.SensorItem.Unit.Symbol;
                    viewModel.WaterVolumeCapacity = Decimal.Parse(pond.Convert(waterVolumesLastEvent.SensorItem.Unit.Id));
                    viewModel.WaterVolumeCapacityUnit = waterVolumesLastEvent.SensorItem.Unit.Symbol;
                }
            }

            // Alarm
            var pondAlarms = this._alarmService.GetsByPondWithTrigger(pondId);
            viewModel.Alarms = AlarmViewModel.Map(pondAlarms);

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetsTank(Guid siteId)
        {
            var tanks = this._tankService.GetsBySite(siteId);
            var viewModels = TankViewModel.Map(tanks);

            if (viewModels.Any())
            {
                foreach (var tankViewModel in viewModels)
                {
                    var tankAlarms = this._alarmService.GetsByTankWithTrigger(tankViewModel.Id);
                    tankViewModel.Alarms = AlarmViewModel.Map(tankAlarms);
                }
            }

            return Json(viewModels, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetTankWithInfo(Guid tankId)
        {
            var tank = this._tankService.Get(tankId);
            TankViewModel viewModel = TankViewModel.Map(tank);

            if (this._sensorService.HasSensorTank(viewModel.Id))
            {
                if (this._sensorItemService.HasTankSensorItem(viewModel.Id, ItemEnum.WaterVolume))
                {
                    var waterVolumesLastEvent = this._sensorItemEventService.GetLastEventByTankAndItem(viewModel.Id, ItemEnum.WaterVolume);

                    if (waterVolumesLastEvent != null)
                    {
                        viewModel.WaterVolumeLastId = waterVolumesLastEvent.Id;
                        viewModel.WaterVolumeLastValue = Decimal.Parse(waterVolumesLastEvent.Value);
                        viewModel.WaterVolumeLastDate = waterVolumesLastEvent.EventDate;
                        viewModel.WaterVolumeLastUnit = waterVolumesLastEvent.SensorItem.Unit.Symbol;
                        viewModel.WaterVolumeCapacity = Decimal.Parse(tank.Convert(waterVolumesLastEvent.SensorItem.Unit.Id));
                        viewModel.WaterVolumeCapacityUnit = waterVolumesLastEvent.SensorItem.Unit.Symbol;
                    }
                }

                if (this._sensorItemService.HasTankSensorItem(viewModel.Id, ItemEnum.WaterTemperature))
                {
                    var waterTemperatureLastEvent = this._sensorItemEventService.GetLastEventByTankAndItem(viewModel.Id, ItemEnum.WaterTemperature);

                    if (waterTemperatureLastEvent != null)
                    {
                        viewModel.WaterTemperatureLastEventId = waterTemperatureLastEvent.Id;
                        viewModel.WaterTemperatureLastEventValue = Decimal.Parse(waterTemperatureLastEvent.Value);
                        viewModel.WaterTemperatureLastEventDate = waterTemperatureLastEvent.EventDate;
                        viewModel.WaterTemperatureLastEventUnit = waterTemperatureLastEvent.SensorItem.Unit.Symbol;
                    }
                }
            }

            // Alarm
            var tankAlarms = this._alarmService.GetsByTankWithTrigger(tankId);
            viewModel.Alarms = AlarmViewModel.Map(tankAlarms);

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetsSensor(Guid siteId)
        {
            var sensors = this._sensorService.GetsBySite(siteId);
            IList<SensorViewModel> viewModels = SensorViewModel.Map(sensors);

            if (viewModels.Any())
            {
                foreach (var viewModel in viewModels)
                {
                    var sensorAlarms = this._alarmService.GetsBySensor(viewModel.Id);
                    viewModel.Alarms = AlarmViewModel.Map(sensorAlarms);
                }
            }

            return Json(viewModels, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Customer, General Manager, Supervisor, Operator")]
        public ActionResult GetSensorWithInfo(Guid sensorId)
        {
            var sensor = this._sensorService.Get(sensorId);
            SensorViewModel viewModel = SensorViewModel.Map(sensor);

            if (this._sensorService.HasSensorTank(viewModel.Id))
            {
                if (this._sensorItemService.HasSiteSensorItem(viewModel.Id, ItemEnum.AmbientTemperature))
                {
                    var ambientTemperatureLastEvent = this._sensorItemEventService.GetLastEventByTankAndItem(viewModel.Id, ItemEnum.AmbientTemperature);

                    if (ambientTemperatureLastEvent != null)
                    {
                        viewModel.AmbientTemperatureLastEventId = ambientTemperatureLastEvent.Id;
                        viewModel.AmbientTemperatureLastEventValue = Decimal.Parse(ambientTemperatureLastEvent.Value);
                        viewModel.AmbientTemperatureLastEventDate = ambientTemperatureLastEvent.EventDate;
                        viewModel.AmbientTemperatureLastEventUnit = ambientTemperatureLastEvent.SensorItem.Unit.Symbol;
                    }
                }

                if (this._sensorItemService.HasSiteSensorItem(viewModel.Id, ItemEnum.Voltage))
                {
                    var voltageLastEvent = this._sensorItemEventService.GetLastEventByTankAndItem(viewModel.Id, ItemEnum.Voltage);

                    if (voltageLastEvent != null)
                    {
                        viewModel.VoltageLastEventId = voltageLastEvent.Id;
                        viewModel.VoltageLastEventValue = Decimal.Parse(voltageLastEvent.Value);
                        viewModel.VoltageLastEventDate = voltageLastEvent.EventDate;
                        viewModel.VoltageLastEventUnit = voltageLastEvent.SensorItem.Unit.Symbol;
                    }
                }
            }

            // Alarm
            var tankAlarms = this._alarmService.GetsByTankWithTrigger(sensorId);
            viewModel.Alarms = AlarmViewModel.Map(tankAlarms);

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
        #endregion Json
    }
}