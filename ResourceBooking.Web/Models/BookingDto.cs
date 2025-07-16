
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ResourceBooking.Web.Models
{
    public class BookingDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Resource ID is required")]
        public int ResourceId { get; set; }

        [ValidateNever] // skip validation on save
        public ResourceDto Resource { get; set; }

        [Required(ErrorMessage = "Start time is required")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End time is required")]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "Booked by is required")]
        [StringLength(50, ErrorMessage = "Booked by cannot exceed 50 characters")]
        public string BookedBy { get; set; } = string.Empty;

        [Required(ErrorMessage = "Purpose is required")]
        [StringLength(200, ErrorMessage = "Purpose cannot exceed 200 characters")]
        public string Purpose { get; set; } = string.Empty;

        public static ValidationResult? ValidateBooking(BookingDto booking, ValidationContext context)
        {
            if (booking.EndTime <= booking.StartTime)
            {
                return new ValidationResult("End time must be after start time.");
            }
            if (booking.StartTime < DateTime.Now)
            {
                return new ValidationResult("Start time cannot be in the past.");
            }
            return ValidationResult.Success;
        }
    }
}
