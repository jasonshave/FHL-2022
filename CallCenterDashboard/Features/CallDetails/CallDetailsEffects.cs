using Azure.Communication.CallingServer;
using Fluxor;
using JasonShave.Azure.Communication.Service.EventHandler.CallAutomation;

namespace CallingDashboard.Features.CallDetails
{
    public class CallDetailsEffects
    {
        private readonly ICallAutomationEventSubscriber _callAutomationEventSubscriber;
        private readonly CallAutomationClient _callAutomationClient;
        private readonly IState<CallDetailsState> _state;

        public CallDetailsEffects(
            ICallAutomationEventSubscriber callAutomationEventSubscriber,
            CallAutomationClient callAutomationClient,
            IState<CallDetailsState> state)
        {
            _callAutomationEventSubscriber = callAutomationEventSubscriber;
            _callAutomationClient = callAutomationClient;
            _state = state;
        }

        [EffectMethod(typeof(CallDetailStateInializeAction))]
        public async Task OnStateInitialize(IDispatcher dispatcher)
        {

            _callAutomationEventSubscriber.OnParticipantsUpdated += (@event, contextId) =>
            {
                dispatcher.Dispatch(new ParticipantUpdateAction(@event.CallConnectionId));

                return ValueTask.CompletedTask;
            };

            _callAutomationEventSubscriber.OnCallDisconnected += (@event, contextId) =>
            {
                dispatcher.Dispatch(new CallDetailClearAction(@event.CallConnectionId));

                return ValueTask.CompletedTask;
            };
            dispatcher.Dispatch(new CallDetailStateSetInitializationAction());
        }
        [EffectMethod]
        public async Task OnInitialize(CallDetailInitializeAction action, IDispatcher dispatcher)
        {
            var callConnection = _callAutomationClient.GetCallConnection(action.Id);
            try
            {
                CallConnectionProperties callConnectionProperties = await callConnection.GetCallConnectionPropertiesAsync();
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
                var callConnection = _callAutomationClient.GetCallConnection(action.Id);
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
