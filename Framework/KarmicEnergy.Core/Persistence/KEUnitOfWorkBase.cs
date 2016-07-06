using Munizoft.Core.Persistence;

namespace KarmicEnergy.Core.Persistence
{
    public abstract class KEUnitOfWorkBase<Ctx> : UnitOfWork<Ctx>
        where Ctx : KEContext
    {
        public KEUnitOfWorkBase(Ctx context)
            : base(context)
        {

        }
    }
}