using Azure.Communication.PhoneNumbers;

namespace CallingDashboard.Features.AutoAnswer;

public record AutoAnswerInitializeAction;

public record AutoAnswerSetAction(bool Enabled, PurchasedPhoneNumber PhoneNumber);