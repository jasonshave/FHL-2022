using CallCenterDashboard.Models;

namespace CallCenterDashboard.Features.PurchasedPhoneNumbers;

public record PurchasedPhoneNumbersState(bool Initialized, bool Loading, IEnumerable<PhoneNumberConfiguration> PhoneNumberConfigurations);