using System.ComponentModel;

namespace KarmicEnergy.Core.Entities
{
    public enum StatusEnum
    {
        [Description("Active")]
        Active = 'A',

        [Description("Inactive")]
        Inactive = 'I'
    }
}
