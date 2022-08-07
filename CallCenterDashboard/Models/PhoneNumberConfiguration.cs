using Azure.Communication.PhoneNumbers;

namespace CallingDashboard.Models;

public class PhoneNumberConfiguration
{
    public string Id { get; set; }
    public PurchasedPhoneNumber PhoneNumber { get; set; }
    public bool AutoAnswer { get; set; }
}