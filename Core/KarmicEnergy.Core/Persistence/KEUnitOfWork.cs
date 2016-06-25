using KarmicEnergy.Core.Repositories;
using Munizoft.Core.Persistence;
using System;

namespace KarmicEnergy.Core.Persistence
{
    public class KEUnitOfWork : KEUnitOfWorkBase<KEContext>, IDisposable
    {
        #region Fields             
        private ILogRepository _LogRepository;

        private IAddressRepository _AddressRepository;

        private ICustomerRepository _CustomerRepository;
        private ICustomerUserRepository _CustomerUserRepository;
        private ICustomerUserSettingRepository _CustomerUserSettingRepository;
        private ICustomerUserSiteRepository _CustomerUserSiteRepository;

        private IUserRepository _UserRepository;

        private ISiteRepository _SiteRepository;

        private IPondRepository _PondRepository;

        private ITankRepository _TankRepository;
        private ITankModelRepository _TankModelRepository;

        private ISensorRepository _SensorRepository;
        private ISensorItemRepository _SensorItemRepository;
        private ISensorTypeRepository _SensorTypeRepository;
        private ISensorGroupRepository _SensorGroupRepository;
        private IGroupRepository _GroupRepository;

        private ISensorItemEventRepository _SensorItemEventRepository;

        private IItemRepository _ItemRepository;

        private ITriggerRepository _TriggerRepository;
        private IOperatorRepository _OperatorRepository;
        private IOperatorTypeRepository _OperatorTypeRepository;

        private IAlarmRepository _AlarmRepository;
        private IAlarmHistoryRepository _AlarmHistoryRepository;

        private IContactRepository _ContactRepository;
        private ICountryRepository _CountryRepository;
        private ISeverityRepository _SeverityRepository;

        private IUnitRepository _UnitRepository;

        private IStickConversionRepository _StickConversionRepository;
        private IStickConversionValueRepository _StickConversionValueRepository;

        private INotificationRepository _NotificationRepository;
        #endregion Fields

        #region Constructor

        public KEUnitOfWork()
            : base(new KEContext())
        {

        }

        public KEUnitOfWork(String connectionString)
            : base(new KEContext(connectionString))
        {

        }

        #endregion Constructor

        //#region Disposable

        //protected override void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            _context.Dispose();
        //        }
        //    }
        //    this.disposed = true;
        //}

        //public override void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //~KEUnitOfWork()
        //{
        //    Dispose(false);
        //}
        //#endregion Disposable

        public static KEUnitOfWork Create()
        {
            return new KEUnitOfWork();
        }

        public ILogRepository LogRepository
        {
            get { return _LogRepository ?? (_LogRepository = new LogRepository(_context)); }
        }

        public IUnitRepository UnitRepository
        {
            get { return _UnitRepository ?? (_UnitRepository = new UnitRepository(_context)); }
        }

        public IAddressRepository AddressRepository
        {
            get { return _AddressRepository ?? (_AddressRepository = new AddressRepository(_context)); }
        }

        public ICustomerRepository CustomerRepository
        {
            get { return _CustomerRepository ?? (_CustomerRepository = new CustomerRepository(_context)); }
        }

        public IUserRepository UserRepository
        {
            get { return _UserRepository ?? (_UserRepository = new UserRepository(_context)); }
        }

        public ICustomerUserRepository CustomerUserRepository
        {
            get { return _CustomerUserRepository ?? (_CustomerUserRepository = new CustomerUserRepository(_context)); }
        }

        public ICustomerUserSettingRepository CustomerUserSettingRepository
        {
            get { return _CustomerUserSettingRepository ?? (_CustomerUserSettingRepository = new CustomerUserSettingRepository(_context)); }
        }

        public ICustomerUserSiteRepository CustomerUserSiteRepository
        {
            get { return _CustomerUserSiteRepository ?? (_CustomerUserSiteRepository = new CustomerUserSiteRepository(_context)); }
        }

        public ISiteRepository SiteRepository
        {
            get { return _SiteRepository ?? (_SiteRepository = new SiteRepository(_context)); }
        }

        public IPondRepository PondRepository
        {
            get { return _PondRepository ?? (_PondRepository = new PondRepository(_context)); }
        }

        public ITankRepository TankRepository
        {
            get { return _TankRepository ?? (_TankRepository = new TankRepository(_context)); }
        }

        public ITankModelRepository TankModelRepository
        {
            get { return _TankModelRepository ?? (_TankModelRepository = new TankModelRepository(_context)); }
        }

        public ISensorRepository SensorRepository
        {
            get { return _SensorRepository ?? (_SensorRepository = new SensorRepository(_context)); }
        }

        public ISensorItemRepository SensorItemRepository
        {
            get { return _SensorItemRepository ?? (_SensorItemRepository = new SensorItemRepository(_context)); }
        }

        public IItemRepository ItemRepository
        {
            get { return _ItemRepository ?? (_ItemRepository = new ItemRepository(_context)); }
        }

        public ISensorTypeRepository SensorTypeRepository
        {
            get { return _SensorTypeRepository ?? (_SensorTypeRepository = new SensorTypeRepository(_context)); }
        }

        public ISensorGroupRepository SensorGroupRepository
        {
            get { return _SensorGroupRepository ?? (_SensorGroupRepository = new SensorGroupRepository(_context)); }
        }

        public IGroupRepository GroupRepository
        {
            get { return _GroupRepository ?? (_GroupRepository = new GroupRepository(_context)); }
        }

        public ITriggerRepository TriggerRepository
        {
            get { return _TriggerRepository ?? (_TriggerRepository = new TriggerRepository(_context)); }
        }

        public IOperatorRepository OperatorRepository
        {
            get { return _OperatorRepository ?? (_OperatorRepository = new OperatorRepository(_context)); }
        }

        public IOperatorTypeRepository OperatorTypeRepository
        {
            get { return _OperatorTypeRepository ?? (_OperatorTypeRepository = new OperatorTypeRepository(_context)); }
        }

        public IAlarmRepository AlarmRepository
        {
            get { return _AlarmRepository ?? (_AlarmRepository = new AlarmRepository(_context)); }
        }

        public IAlarmHistoryRepository AlarmHistoryRepository
        {
            get { return _AlarmHistoryRepository ?? (_AlarmHistoryRepository = new AlarmHistoryRepository(_context)); }
        }

        public ISensorItemEventRepository SensorItemEventRepository
        {
            get { return _SensorItemEventRepository ?? (_SensorItemEventRepository = new SensorItemEventRepository(_context)); }
        }

        public ISeverityRepository SeverityRepository
        {
            get { return _SeverityRepository ?? (_SeverityRepository = new SeverityRepository(_context)); }
        }

        public ICountryRepository CountryRepository
        {
            get { return _CountryRepository ?? (_CountryRepository = new CountryRepository(_context)); }
        }

        public IContactRepository ContactRepository
        {
            get { return _ContactRepository ?? (_ContactRepository = new ContactRepository(_context)); }
        }

        public IStickConversionRepository StickConversionRepository
        {
            get { return _StickConversionRepository ?? (_StickConversionRepository = new StickConversionRepository(_context)); }
        }

        public IStickConversionValueRepository StickConversionValueRepository
        {
            get { return _StickConversionValueRepository ?? (_StickConversionValueRepository = new StickConversionValueRepository(_context)); }
        }

        public INotificationRepository NotificationRepository
        {
            get { return _NotificationRepository ?? (_NotificationRepository = new NotificationRepository(_context)); }
        }
    }
}
