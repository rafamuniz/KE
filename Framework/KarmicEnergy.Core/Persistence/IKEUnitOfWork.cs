using KarmicEnergy.Core.Repositories;
using System;

namespace KarmicEnergy.Core.Persistence
{
    public interface IKEUnitOfWork : IDisposable
    {
        ISiteRepository SiteRepository { get; }
    }
}