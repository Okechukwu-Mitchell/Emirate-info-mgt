using System.ComponentModel;

namespace Emirate.EmirateEnums
{
    public enum PaymentStatus
    {
        [Description("Pending")]
        Pending = 1,
        [Description("Approved")]
        Approved = 2,
        [Description("Declined")]
        Declined = 3,
        
    }

    public enum DocumentStatus
    {

        //[Description("Pending")]
        //Pending = 1,
        //[Description("Completed")]
        //Approved = 2,
        //[Description("Declined")]
        //Declined = 3,

    }
}

