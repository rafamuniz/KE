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
    }
}
