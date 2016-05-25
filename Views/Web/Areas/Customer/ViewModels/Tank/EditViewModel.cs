using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Tank
{
    public class EditViewModel
    {
        #region Constructor
        public EditViewModel()
        {
            TankModel = new TankModelViewModel();
        }
        #endregion Constructor

        #region Property
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        [Required]
        public String Name { get; set; }

        [Display(Name = "Reference")]
        [Required]
        [MaxLength(8)]
        public String Reference { get; set; }

        [Display(Name = "Description")]
        public String Description { get; set; }

        [Display(Name = "Status")]
        [Required]
        public String Status { get; set; }

        [Display(Name = "Site")]
        [Required]
        public Guid SiteId { get; set; }

        [Display(Name = "Tank Model")]
        [Required]
        public Int32 TankModelId { get; set; }

        [Display(Name = "Stick Conversion")]
        public Int32? StickConversionId { get; set; }

        [Display(Name = "Latitude")]
        public String Latitude { get; set; }

        [Display(Name = "Longitude")]
        public String Longitude { get; set; }

        [Display(Name = "Width")]
        public Decimal? Width { get; set; }

        [Display(Name = "Height")]
        public Decimal? Height { get; set; }

        [Display(Name = "Length")]
        public Decimal? Length { get; set; }

        [Display(Name = "Face Length")]
        public Decimal? FaceLength { get; set; }

        [Display(Name = "Bottom Width")]
        public Decimal? BottomWidth { get; set; }

        [Display(Name = "Dimension 1")]
        public Decimal? Dimension1 { get; set; }

        [Display(Name = "Dimension 2")]
        public Decimal? Dimension2 { get; set; }

        [Display(Name = "Dimension 3")]
        public Decimal? Dimension3 { get; set; }

        [Display(Name = "Minimum Distance")]
        public Decimal? MinimumDistance { get; set; }

        [Display(Name = "Maximum Distance")]
        public Decimal? MaximumDistance { get; set; }

        [Display(Name = "Water Volume Capacity")]
        public Decimal? WaterVolumeCapacity { get; set; }

        #endregion Property

        #region TankModeViewModel

        public TankModelViewModel TankModel { get; set; }

        #endregion TankModeViewModel

        #region Map

        public void Map(Core.Entities.Tank entity)
        {
            Mapper.CreateMap<Core.Entities.Tank, EditViewModel>().ForMember(x => x.TankModel, opt => opt.Ignore());
            Mapper.Map<Core.Entities.Tank, EditViewModel>(entity, this);
        }

        public void MapVMToEntity(Core.Entities.Tank entity)
        {
            Mapper.CreateMap<EditViewModel, Core.Entities.Tank>().ForMember(x => x.TankModel, opt => opt.Ignore());
            Mapper.Map<EditViewModel, Core.Entities.Tank>(this, entity);

            entity.Height = this.TankModel.Height;
            entity.Width = this.TankModel.Width;
            entity.Length = this.TankModel.Length;
            entity.FaceLength = this.TankModel.FaceLength;
            entity.BottomWidth = this.TankModel.BottomWidth;
            entity.Dimension1 = this.TankModel.Dimension1;
            entity.Dimension2 = this.TankModel.Dimension2;
            entity.Dimension3 = this.TankModel.Dimension3;
            //entity.MinimumDistance = this.TankModel.MinimumDistance;
            //entity.MaximumDistance = this.TankModel.MaximumDistance;
            entity.WaterVolumeCapacity = this.TankModel.WaterVolumeCapacity.HasValue ? this.TankModel.WaterVolumeCapacity.Value : 0;
        }

        public TankModelViewModel Map(Core.Entities.TankModel entity)
        {
            Mapper.CreateMap<Core.Entities.TankModel, TankModelViewModel>();
            var viewModel = Mapper.Map<Core.Entities.TankModel, TankModelViewModel>(entity, this.TankModel);

            this.TankModel.Height = this.Height;
            this.TankModel.Width = this.Width;
            this.TankModel.Length = this.Length;
            this.TankModel.FaceLength = this.FaceLength;
            this.TankModel.BottomWidth = this.BottomWidth;
            this.TankModel.Dimension1 = this.Dimension1;
            this.TankModel.Dimension2 = this.Dimension2;
            this.TankModel.Dimension3 = this.Dimension3;
            this.TankModel.MinimumDistance = this.MinimumDistance;
            this.TankModel.MaximumDistance = this.MaximumDistance;
            this.TankModel.WaterVolumeCapacity = this.WaterVolumeCapacity;

            return viewModel;
        }

        #endregion Map
    }
}