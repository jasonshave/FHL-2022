using Azure.Communication.PhoneNumbers;

namespace CallingDashboard.Features.AutoAnswer;

public record AutoAnswerState(bool Initialized, bool Enabled, PurchasedPhoneNumber? PhoneNumber);