using Azure.Communication.PhoneNumbers;

namespace CallCenterDashboard.Models;

public class PhoneNumberConfiguration
{
    public string Id { get; set; }
    public PurchasedPhoneNumber PhoneNumber { get; set; }
    public bool AutoAnswer { get; set; }
}