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

                if (!WaterVolumePercentage.HasValue)
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "NoData"));

                Int32 volume = 0;

                if (Int32.TryParse(WaterVolumePercentage.ToString(), out volume))
                {
                    var rnd = Math.Round(WaterVolumePercentage.Value);
                }

                if (WaterVolumePercentage == 0)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "0"));
                }
                else if (WaterVolumePercentage > 0 && WaterVolumePercentage < 20)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "10"));
                }
                else if (WaterVolumePercentage >= 20 && WaterVolumePercentage < 30)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "20"));
                }
                else if (WaterVolumePercentage >= 30 && WaterVolumePercentage < 40)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "30"));
                }
                else if (WaterVolumePercentage >= 40 && WaterVolumePercentage < 50)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "40"));
                }
                else if (WaterVolumePercentage >= 50 && WaterVolumePercentage < 60)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "50"));
                }
                else if (WaterVolumePercentage >= 60 && WaterVolumePercentage < 70)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelImage.Replace("{info}", "60"));
                }
                else if (WaterVolumePercentage >= 70 && WaterVolumePercentage < 80)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "70"));
                }
                else if (WaterVolumePercentage >= 80 && WaterVolumePercentage < 90)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "80"));
                }
                else if (WaterVolumePercentage >= 90 && WaterVolumePercentage < 100)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "90"));
                }
                else if (WaterVolumePercentage == 100)
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
        public Decimal? WaterVolumePercentage
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