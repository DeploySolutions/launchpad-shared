using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.BackgroundProcess
{
    //
    // Summary:
    //     This interface is used to mark classes that can be run as LaunchPad background job arguments
    //
    // Type parameters:
    //   TArgs:
    public partial interface ICanBeLaunchPadBackgroundJobArgs
    {
        [Required]
        public string Id { get; set; }

        public string Name { get; set; }

        public string DescriptionShort { get; set; }
        
        public string DescriptionFull { get; set; }

        public string Chron { get; set; }

    }
}
