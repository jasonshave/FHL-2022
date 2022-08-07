using Azure.Communication;

namespace CallCenterDashboard.Features.ApplicationIdentity;

public record ApplicationIdentityState(bool Initialized, bool Loading, CommunicationUserIdentifier? Id, string? ApplicationName);