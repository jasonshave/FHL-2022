using Azure.Communication.CallingServer;
using Fluxor;
using JasonShave.Azure.Communication.Service.EventHandler.CallingServer;

namespace CallCenterDashboard.Features.CallDetails
{
    public class CallDetailsEffects
    {
        private readonly ICallingServerEventSubscriber _callingServerEventSubscriber;
        private readonly CallingServerClient _callingServerClient;

        public CallDetailsEffects(
            ICallingServerEventSubscriber callingServerEventSubscriber,
            CallingServerClient callingServerClient)
        {
            _callingServerEventSubscriber = callingServerEventSubscriber;
            _callingServerClient = callingServerClient;
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
    }

}
