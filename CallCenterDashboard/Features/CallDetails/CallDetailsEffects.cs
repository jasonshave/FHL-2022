using Azure.Communication.CallingServer;
using Fluxor;
using JasonShave.Azure.Communication.Service.EventHandler.CallingServer;

namespace CallCenterDashboard.Features.CallDetails
{
    public class CallDetailsEffects
    {
        private readonly ICallingServerEventSubscriber _callingServerEventSubscriber;
        private readonly CallingServerClient _callingServerClient;
        private readonly IState<CallDetailsState> _state;

        public CallDetailsEffects(
            ICallingServerEventSubscriber callingServerEventSubscriber,
            CallingServerClient callingServerClient,
            IState<CallDetailsState> state)
        {
            _callingServerEventSubscriber = callingServerEventSubscriber;
            _callingServerClient = callingServerClient;
            _state = state;
        }

        [EffectMethod(typeof(CallDetailStateInializeAction))]
        public async Task OnStateInitialzie(IDispatcher dispatcher)
        {

            _callingServerEventSubscriber.OnParticipantsUpdated += (@event, contextId) =>
            {
                dispatcher.Dispatch(new ParticipantUpdateAction(@event.CallConnectionId));

                return ValueTask.CompletedTask;
            };

            _callingServerEventSubscriber.OnCallDisconnected += (@event, contextId) =>
            {
                dispatcher.Dispatch(new CallDetailClearAction(@event.CallConnectionId));

                return ValueTask.CompletedTask;
            };
            dispatcher.Dispatch(new CallDetailStateSetInitializationAction());
        }
        [EffectMethod]
        public async Task OnInitialize(CallDetailInitializeAction action, IDispatcher dispatcher)
        {
            var callConnection = _callingServerClient.GetCallConnection(action.Id);
            try
            {
                CallConnectionProperties callConnectionProperties = callConnection.GetProperties();
                var participants = (await callConnection.GetParticipantsAsync()).Value;
                dispatcher.Dispatch(new CallDetailSetDataAction(
                    action.Id,
                    callConnectionProperties,
                    participants
                 ));
            }
            catch (Exception e)
            {
                dispatcher.Dispatch(new CallDetailSetDataAction(
                     action.Id,
                     null,
                     null
                  ));
            }
        }

        [EffectMethod]
        public async Task OnParticipantsUpdate(ParticipantUpdateAction action, IDispatcher dispatcher)
        {
            if (_state.Value.PreviousId == action.Id)
            {
                var callConnection = _callingServerClient.GetCallConnection(action.Id);
                try
                {
                    var participants = (await callConnection.GetParticipantsAsync()).Value;
                    dispatcher.Dispatch(new ParticipantSetDataAction(
                        action.Id,
                        participants
                     ));
                }
                catch (Exception e)
                {

                }
            }
        }
    }

}
