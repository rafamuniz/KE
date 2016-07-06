using System.Linq;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            KEUnitOfWork KEUnitOfWork = KEUnitOfWork.Create();

            var sensors = KEUnitOfWork.SensorRepository.GetAllActive().ToList();

            if (sensors.Any())
            {
                foreach (var sensor in sensors)
                {

                }
            }
        }
    }
}
