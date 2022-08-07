using System.ComponentModel.DataAnnotations;

namespace CallingDashboard.Models;

public class ParticipantManagementFormData
{
    [Required(ErrorMessage = "The identity is required.")]
    public string Id { get; set; }
}