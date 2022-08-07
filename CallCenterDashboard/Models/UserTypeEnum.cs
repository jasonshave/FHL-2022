using System.Runtime.Serialization;

namespace CallingDashboard.Models;

public enum UserTypeEnum
{
    [EnumMember(Value = "ACS User")]
    AcsUser,
    [EnumMember(Value = "Phone Number")]
    PhoneNumber,
    [EnumMember(Value = "Teams User")]
    TeamsUser
}