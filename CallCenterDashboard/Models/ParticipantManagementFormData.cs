using System.ComponentModel.DataAnnotations;

namespace CallCenterDashboard.Models;

public class ParticipantManagementFormData
{
    [Required(ErrorMessage = "The identity is required.")]
    public string Id { get; set; }
}