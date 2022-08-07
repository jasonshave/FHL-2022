using Azure.Communication.PhoneNumbers;

namespace CallCenterDashboard.Features.AutoAnswer;

public record AutoAnswerState(bool Initialized, bool Enabled, PurchasedPhoneNumber? PhoneNumber);