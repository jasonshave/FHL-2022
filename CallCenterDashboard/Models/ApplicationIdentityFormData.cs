using System.ComponentModel.DataAnnotations;

namespace CallCenterDashboard.Models;

public class ApplicationIdentityFormData
{
    [Required(ErrorMessage = "The application name is required.")]
    public string Name { get; set; }
}