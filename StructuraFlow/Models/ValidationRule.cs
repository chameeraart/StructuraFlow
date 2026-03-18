using System.ComponentModel.DataAnnotations;

namespace StructuraFlow.Models
{

    public class ValidationRule
    {
        public int Id { get; set; }

        [Display(Name = "Check Duplicate Columns")]
        public bool CheckDuplicateColumns { get; set; }

        [Display(Name = "Check Duplicate Beams")]
        public bool CheckDuplicateBeams { get; set; }

        [Display(Name = "Check Duplicate Slabs")]
        public bool CheckDuplicateSlabs { get; set; }

        [Display(Name = "Check Negative Values")]
        public bool CheckNegativeValues { get; set; }

        [Display(Name = "Check Missing Fields")]
        public bool CheckMissingFields { get; set; }

        [Display(Name = "Check Beam References")]
        public bool CheckBeamReferences { get; set; }

        [Display(Name = "Check Beam Start/End Same")]
        public bool CheckBeamStartEndSame { get; set; }

        // Numeric fields with validation
        [Required(ErrorMessage = "Min Column Height is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Must be greater than 0")]
        public double? MinColumnHeight { get; set; }

        [Required(ErrorMessage = "Min Column Width is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Must be greater than 0")]
        public double? MinColumnWidth { get; set; }

        [Required(ErrorMessage = "Min Beam Length is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Must be greater than 0")]
        public double? MinBeamLength { get; set; }

        [Required(ErrorMessage = "Min Slab Thickness is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Must be greater than 0")]
        public double? MinSlabThickness { get; set; }
    }

}
