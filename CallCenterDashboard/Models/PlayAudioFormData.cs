using System.ComponentModel.DataAnnotations;

namespace CallCenterDashboard.Models;

public class PlayAudioFormData
{
    [Required(ErrorMessage = "Please specify a file location.")]
    public string FileLocation { get; set; } = string.Empty;

    public bool Loop { get; set; }
}