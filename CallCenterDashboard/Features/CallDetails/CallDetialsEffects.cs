using Azure;
using Azure.Communication;
using Azure.Communication.CallingServer;
using CallCenterDashboard.Features.ActiveCalls;
using CallCenterDashboard.Interfaces;
using CallCenterDashboard.Models;
using Fluxor;
using JasonShave.Azure.Communication.Service.EventHandler.CallingServer;
using MudBlazor;
namespace CallCenterDashboard.Features.CallDetails
{
    public class CallDetialsEffects
    {
        private readonly ICallingServerEventSubscriber _callingServerEventSubscriber;
        private readonly CallingServerClient _callingServerClient;
       
        public CallDetialsEffects(
            ICallingServerEventSubscriber callingServerEventSubscriber,
            CallingServerClient callingServerClient)
        {
            _callingServerEventSubscriber = callingServerEventSubscriber;
            _callingServerClient = callingServerClient;
        }

        [EffectMethod]
        public async Task OnInitialize(InitializeCallDetailAction action, IDispatcher dispatcher)
        {
            var callConnection = _callingServerClient.GetCallConnection(action.Id);
            CallConnectionProperties callConnectionProperties = await callConnection.GetPropertiesAsync();
            var participants = (await callConnection.GetParticipantsAsync()).Value;
            dispatcher.Dispatch(new InitializeCallDetailWithDataAction(
                action.Id,
                callConnectionProperties,
                participants
             ));
            _callingServerEventSubscriber.OnParticipantsUpdated += (@event, contextId) =>
            {
                dispatcher.Dispatch(new ParticipantsUpdatedAction(@event.CorrelationId));
                return ValueTask.CompletedTask;
            };
        }

        [EffectMethod]
        public async Task OnUpdate(UpdateParticipantsAction action, IDispatcher dispatcher)
        {
            var callConnection = _callingServerClient.GetCallConnection(action.Id);
            var participants = (await callConnection.GetParticipantsAsync()).Value;
            dispatcher.Dispatch(new UpdateParticipantsWithDataAction(action.Id, participants));
        }
    }

}
