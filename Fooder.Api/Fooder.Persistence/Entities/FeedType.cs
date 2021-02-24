using System.ComponentModel.DataAnnotations;

namespace Fooder.Persistence.Entities
{
    public enum FeedType
    {
        [Display(Name = "Granular by weight")] GranularByWeight,
        [Display(Name = "Granular packed")] GranularPacked,
        [Display(Name = "Wet packed")] WetPacked, }
}
