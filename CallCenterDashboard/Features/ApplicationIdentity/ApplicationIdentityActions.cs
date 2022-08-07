using Azure.Communication;

namespace CallingDashboard.Features.ApplicationIdentity;

public record ApplicationIdentityInitializeAction;

public record ApplicationIdentityLoadingAction(bool Loading);

public record ApplicationIdentitySetNameAction(string ApplicationName);

public record ApplicationIdentitySetIdentityAction(CommunicationUserIdentifier identity);
