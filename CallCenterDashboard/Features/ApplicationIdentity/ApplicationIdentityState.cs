using Azure.Communication;

namespace CallingDashboard.Features.ApplicationIdentity;

public record ApplicationIdentityState(bool Initialized, bool Loading, CommunicationUserIdentifier? Id, string? ApplicationName);