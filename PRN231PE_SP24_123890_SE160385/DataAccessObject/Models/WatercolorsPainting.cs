using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static DataAccessObject.Models.Validation.Validation;

namespace DataAccessObject.Models
{
    public partial class WatercolorsPainting
    {
        [Key]
        [Required]
        
        public string PaintingId { get; set; } = null!;
        [Required]
        [WatercolorsPaintingNameAttribute(ErrorMessage = "Please enter a valid Name.")]
        public string PaintingName { get; set; } = null!;
        [Required]
        public string? PaintingDescription { get; set; }
        [Required]
        public string? PaintingAuthor { get; set; }
        [Required]
        [PriceAttribute(ErrorMessage = "Please enter a valid price.")]
        public decimal? Price { get; set; }
        [Required]
        [PublishyearAttribute(ErrorMessage = "Please enter a valid year.")]
        public int? PublishYear { get; set; }
        public DateTime? CreatedDate { get; set; }
        [Required]
        public string? StyleId { get; set; }

        public virtual Style? Style { get; set; }
    }
}
