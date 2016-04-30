using System;
using System.Collections.Generic;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.FastTracker
{
    public class TankWithWaterVolume
    {
        #region Constructor
        public TankWithWaterVolume()
        {
            WaterVolumeInfos = new List<WaterVolumeViewModel>();
        }
        #endregion Constructor

        #region Property

        public Guid SiteId { get; set; }

        public Guid TankId { get; set; }

        public String TankName { get; set; }

        public Int32 TankModelId { get; set; }
        public String TankModelImage { get; set; }

        public String UrlImageTankModel
        {
            get
            {
                var baseurl = "/images/tankmodels/";

                if (!WaterVolumePerc.HasValue)
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "NoData"));

                Int32 volume = 0;

                if (Int32.TryParse(WaterVolumePerc.ToString(), out volume))
                {
                    var rnd = Math.Round(WaterVolumePerc.Value);
                }

                if (WaterVolumePerc == 0)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "0"));
                }
                else if (WaterVolumePerc > 0 && WaterVolumePerc < 20)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "10"));
                }
                else if (WaterVolumePerc >= 20 && WaterVolumePerc < 30)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "20"));
                }
                else if (WaterVolumePerc >= 30 && WaterVolumePerc < 40)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "30"));
                }
                else if (WaterVolumePerc >= 40 && WaterVolumePerc < 50)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "40"));
                }
                else if (WaterVolumePerc >= 50 && WaterVolumePerc < 60)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "50"));
                }
                else if (WaterVolumePerc >= 60 && WaterVolumePerc < 70)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelImage.Replace("{info}", "60"));
                }
                else if (WaterVolumePerc >= 70 && WaterVolumePerc < 80)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "70"));
                }
                else if (WaterVolumePerc >= 80 && WaterVolumePerc < 90)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "80"));
                }
                else if (WaterVolumePerc >= 90 && WaterVolumePerc < 100)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "90"));
                }
                else if (WaterVolumePerc == 100)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "100"));
                }

                return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "NoData"));
            }
            private set { }
        }

        public Guid SensorItemId { get; set; }

        public Decimal WaterVolumeCapacity { get; set; }
        public Decimal? WaterVolume { get; set; }
        public Decimal? WaterVolumePerc
        {
            get
            {
                if (WaterVolumeCapacity != 0 && WaterVolume.HasValue)
                {
                    return (WaterVolume / WaterVolumeCapacity);
                }
                else if (WaterVolumeCapacity == 0)
                {
                    return 0;
                }

                return null;
            }
        }

        public DateTime? EventDate { get; set; }

        public List<WaterVolumeViewModel> WaterVolumeInfos { get; set; }

        #endregion Property        
    }
}