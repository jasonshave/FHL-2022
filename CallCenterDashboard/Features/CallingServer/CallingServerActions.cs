using Azure.Communication;
using CallCenterDashboard.Models;
using JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.V2022_11_1_preview.Events;

namespace CallCenterDashboard.Features.CallingServer;

public record CallingServerInitializeAction;
public record CallingServerNotifyIncomingCallAction(IncomingCall incomingCall, string? contextId);
public record CallingServerAnswerAction(string incomingCallContext);
public record CallingServerHangUpAction(CallData CallData);
public record CallingServerTerminateAction(CallData CallData);
public record CallingServerAddParticipantAction(CallData CallData, CommunicationIdentifier UserToAdd);
public record CallingServerRemoveParticipantAction(CallData CallData, CommunicationIdentifier UserToRemove);
public record CallingServerTransferCallAction(CallData CallData, CommunicationIdentifier UserToTransferTo);
public record CallingServerNotifyAction(NotificationData NotificationData);