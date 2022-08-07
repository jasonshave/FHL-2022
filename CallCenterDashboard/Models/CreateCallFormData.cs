using Azure.Communication;

namespace CallCenterDashboard.Models;

public class CreateCallFormData
{
    public string Source { get; set; } = null!;

    public PhoneNumberIdentifier? AlternateCallerId { get; set; }

    public HashSet<CommunicationIdentifier> Targets { get; } = new ();
}