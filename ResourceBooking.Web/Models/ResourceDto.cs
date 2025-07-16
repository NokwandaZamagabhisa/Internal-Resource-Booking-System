using System.ComponentModel.DataAnnotations;

namespace ResourceBooking.Web.Models
{
    public class ResourceDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Location is required")]
        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters")]
        public string Location { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be a positive number")]
        public int Capacity { get; set; }

        [Display(Name = "Available")] public bool IsAvailable { get; set; } = true;
    }
}
