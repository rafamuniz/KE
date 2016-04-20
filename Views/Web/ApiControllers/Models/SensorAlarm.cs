using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace KarmicEnergy.Web.ApiControllers.Models
{
    [DataContract]
    public class SensorAlarm
    {
        [DataMember]
        public Int64 SensorId { get; set; }

        [DataMember]
        public String Description { get; set; }

        [DataMember]
        public String Email { get; set; }

        [DataMember]
        public Int16 SeverityId { get; set; }

        [DataMember]
        public Int16 SensorTypeId { get; set; }

        [DataMember]
        public Int32 Limit { get; set; }
    }
}