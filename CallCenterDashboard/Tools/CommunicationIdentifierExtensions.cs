using Azure.Communication;

namespace CallCenterDashboard.Tools;

public static class CommunicationIdentifierExtensions
{
    public static CommunicationIdentifier ToCommunicationIdentifier(this string input)
    {
        if (input.StartsWith("+", StringComparison.OrdinalIgnoreCase))
            return new PhoneNumberIdentifier(input);

        if (input.StartsWith("8:acs", StringComparison.OrdinalIgnoreCase))
            return new CommunicationUserIdentifier(input);

        if (input.StartsWith("8:", StringComparison.OrdinalIgnoreCase))
            return new MicrosoftTeamsUserIdentifier(input);

        return new UnknownIdentifier(input);
    }
}