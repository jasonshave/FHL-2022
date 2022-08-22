using System.ComponentModel.DataAnnotations;

namespace CallingDashboard.Models;

public class MediaData
{
    public string Id { get; set; }

    [Required(ErrorMessage = "Enter a name for the media source.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "The file location is required.")]
    [Url]
    public string FileUri { get; set; }

    public bool Loop { get; set; } = new();
}