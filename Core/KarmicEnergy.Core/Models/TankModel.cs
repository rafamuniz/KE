using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarmicEnergy.Core.Models
{
    public class TankModel
    {
        #region Property

        public Guid Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public Decimal WaterVolumeCapacity { get; set; }

        public String Status { get; set; } = "A";

        public String Latitude { get; set; }

        public String Longitude { get; set; }

        public Guid SiteId { get; set; }

        public Int32 TankModelId { get; set; }

        public Int32 NumAlarms { get; set; }

        public Decimal? WaterTemperature { get; set; }

        public Decimal? WeatherTemperature { get; set; }

        public DateTime LastMeasurementDate { get; set; }
        #endregion Property
    }
}
