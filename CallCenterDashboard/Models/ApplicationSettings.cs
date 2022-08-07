using Azure.Communication;
using System.ComponentModel.DataAnnotations;

namespace CallingDashboard.Models;

public class ApplicationSettings
{
    public string? ResourceId { get; set; }

    [Required(ErrorMessage = "The application name must be set.")]
    public string? ApplicationName { get; set; }

    public bool AutoAnswer { get; set; }

    public HashSet<PhoneNumberConfiguration> PhoneNumbers { get; } = new();

    public CommunicationUserIdentifier? ApplicationIdentity { get; set; }
}