using System.ComponentModel.DataAnnotations;

namespace CallingDashboard.Models;

public class ApplicationIdentityFormData
{
    [Required(ErrorMessage = "The application name is required.")]
    public string Name { get; set; }
}