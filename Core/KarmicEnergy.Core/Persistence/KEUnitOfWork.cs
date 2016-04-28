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
        private ISiteRepository _SiteRepository;
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
        private IAlarmRepository _AlarmRepository;

        private IContactRepository _ContactRepository;
        private ICountryRepository _CountryRepository;
        private ISeverityRepository _SeverityRepository;

        private IUnitRepository _UnitRepository;
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

        public ICustomerUserRepository CustomerUserRepository
        {
            get { return _CustomerUserRepository ?? (_CustomerUserRepository = new CustomerUserRepository(_context)); }
        }

        public ISiteRepository SiteRepository
        {
            get { return _SiteRepository ?? (_SiteRepository = new SiteRepository(_context)); }
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

        public IAlarmRepository AlarmRepository
        {
            get { return _AlarmRepository ?? (_AlarmRepository = new AlarmRepository(_context)); }
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
    }
}
