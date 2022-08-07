using Azure.Communication.PhoneNumbers;

namespace CallCenterDashboard.Features.AutoAnswer;

public record AutoAnswerInitializeAction;

public record AutoAnswerSetAction(bool Enabled, PurchasedPhoneNumber PhoneNumber);