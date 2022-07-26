using Azure.Communication;
using CallCenterDashboard.Models;

namespace CallCenterDashboard.Misc;

public static class UserTypeEnumExtensions
{
    public static CommunicationIdentifier Convert(this UserTypeEnum userType, string id)
    {
        switch (userType)
        {
            case UserTypeEnum.AcsUser:
                return new CommunicationUserIdentifier(id);
            case UserTypeEnum.PhoneNumber:
                return new PhoneNumberIdentifier(id);
            case UserTypeEnum.TeamsUser:
                return new MicrosoftTeamsUserIdentifier(id);
            default:
                throw new ArgumentOutOfRangeException(nameof(userType), userType, null);
        }
    }
}