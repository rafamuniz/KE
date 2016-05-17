using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KarmicEnergy.Core.Persistence;

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
