using CallingDashboard.Models;

namespace CallingDashboard.Features.PurchasedPhoneNumbers;

public record PurchasedPhoneNumbersState(bool Initialized, bool Loading, IEnumerable<PhoneNumberConfiguration> PhoneNumberConfigurations);