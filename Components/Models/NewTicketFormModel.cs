using System.ComponentModel.DataAnnotations;

namespace AidLog.Components.Models;

public class NewTicketFormModel
{
    [Required]
    [MinLength(8, ErrorMessage = "Title is too short")]
    [StringLength(64, ErrorMessage = "Identifier too long (64 character limit)")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [MinLength(10, ErrorMessage = "Description is too short")]
    [StringLength(255, ErrorMessage = "Description is too long (255 character limit)")]
    public string? Description { get; set; }

    public bool EnableNotifications { get; set; }
}