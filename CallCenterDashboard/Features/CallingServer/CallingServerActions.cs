using Azure.Communication;
using CallCenterDashboard.Models;

namespace CallCenterDashboard.Features.CallingServer;

public record CallingServerHangUpAction(CallData CallData);
public record CallingServerTerminateAction(CallData CallData);
public record CallingServerAddParticipantAction(CallData CallData, CommunicationIdentifier UserToAdd);
public record CallingServerRemoveParticipantAction(CallData CallData, CommunicationIdentifier UserToRemove);

public record CallingServerNotifyAction(NotificationData NotificationData);