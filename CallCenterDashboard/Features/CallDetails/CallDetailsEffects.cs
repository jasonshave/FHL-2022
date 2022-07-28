using Azure;
using Azure.Communication;
using Azure.Communication.CallingServer;
using CallCenterDashboard.Features.ActiveCalls;
using CallCenterDashboard.Interfaces;
using CallCenterDashboard.Models;
using Fluxor;
using JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.V2022_11_1_preview.Events;
using JasonShave.Azure.Communication.Service.EventHandler.CallingServer;
using MudBlazor;
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
        public async Task OnInitialize(InitializeCallDetailAction action, IDispatcher dispatcher)
        {
            var callConnection = _callingServerClient.GetCallConnection(action.Id);
            try
            {
                CallConnectionProperties callConnectionProperties = callConnection.GetProperties();
                var participants = (await callConnection.GetParticipantsAsync()).Value;
                    dispatcher.Dispatch(new InitializeCallDetailWithDataAction(
                    callConnectionProperties,
                    participants
                 ));

            }
            catch (Exception e)
            {
                dispatcher.Dispatch(new InitializeCallDetailWithDataAction(
                     null,
                     null
                  ));
            }
        }
    }

}
