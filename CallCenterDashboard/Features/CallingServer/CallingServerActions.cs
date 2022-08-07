using Azure.Communication;
using CallCenterDashboard.Models;

namespace CallCenterDashboard.Features.CallingServer;

public record CallingServerInitializeAction;
public record CallingServerAnswerAction(UnansweredCall UnansweredCall);
public record CallingServerRejectAction(UnansweredCall UnansweredCall);
public record CallingServerHangUpAction(CallData CallData);
public record CallingServerTerminateAction(CallData CallData);
public record CallingServerAddParticipantAction(CallData CallData, CommunicationIdentifier UserToAdd);
public record CallingServerRemoveParticipantAction(CallData CallData, CommunicationIdentifier UserToRemove);
public record CallingServerTransferCallAction(CallData CallData, CommunicationIdentifier UserToTransferTo);
public record CallingServerNotifyAction(NotificationData NotificationData);
public record CallingServerPlayAudioAction(CallData CallData, Uri AudioLocation, bool Loop);
public record CallingServerCreateCallAction(CommunicationIdentifier Source, IEnumerable<CommunicationIdentifier> Targets, PhoneNumberIdentifier? AlternateCallerId);