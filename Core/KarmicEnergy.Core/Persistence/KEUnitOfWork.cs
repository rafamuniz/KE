using KarmicEnergy.Core.Repositories;
using Munizoft.Core.Persistence;
using System;

namespace KarmicEnergy.Core.Persistence
{
    public class KEUnitOfWork : UnitOfWork<KEContext>, IDisposable
    {
        #region Fields             
        private ICustomerRepository _CustomerRepository;
        private ICustomerUserRepository _CustomerUserRepository;
        private ISiteRepository _SiteRepository;
        private ITankRepository _TankRepository;       
        private ITankModelRepository _TankModelRepository;
        private ISensorRepository _SensorRepository;
        private ISensorTypeRepository _SensorTypeRepository;

        private ISensorDataRepository _SensorDataRepository;

        private ISensorAlarmRepository _SensorAlarmRepository;

        private ICountryRepository _CountryRepository;
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

        public ISensorTypeRepository SensorTypeRepository
        {
            get { return _SensorTypeRepository ?? (_SensorTypeRepository = new SensorTypeRepository(_context)); }
        }

        public ISensorAlarmRepository SensorAlarmRepository
        {
            get { return _SensorAlarmRepository ?? (_SensorAlarmRepository = new SensorAlarmRepository(_context)); }
        }

        public ISensorDataRepository SensorDataRepository
        {
            get { return _SensorDataRepository ?? (_SensorDataRepository = new SensorDataRepository(_context)); }
        }

        public ICountryRepository CountryRepository
        {
            get { return _CountryRepository ?? (_CountryRepository = new CountryRepository(_context)); }
        }
    }
}
