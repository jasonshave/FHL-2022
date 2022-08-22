using CallingDashboard.Features.CallingServer;
using CallingDashboard.Features.UnansweredCalls;
using CallingDashboard.Interfaces;
using CallingDashboard.Models;
using Fluxor;
using JasonShave.Azure.Communication.Service.EventHandler.CallAutomation;

namespace CallingDashboard.Features.AutoAnswer;

public class AutoAnswerEffects
{
    private readonly ICallAutomationEventSubscriber _callAutomationEventSubscriber;

    public AutoAnswerEffects(
        ICallAutomationEventSubscriber callAutomationEventSubscriber)
    {
        _callAutomationEventSubscriber = callAutomationEventSubscriber;
    }

    [EffectMethod]
    public async Task OnSet(AutoAnswerSetAction action, IDispatcher dispatcher)
    {
        _callAutomationEventSubscriber.OnIncomingCall += (e, c) =>
        {
            switch (action.Enabled)
            {
                case true:
                    {
                        if (action.PhoneNumber.Id == e.To.RawId)
                        {
                            // IncomingCall matches the auto-answer
                            dispatcher.Dispatch(new CallingServerAnswerAction(new UnansweredCall(e, c, DateTimeOffset.UtcNow)));
                        }
                        break;
                    }
                case false:
                    {
                        dispatcher.Dispatch(new UnansweredCallsAddAction(new UnansweredCall(e, c, DateTimeOffset.UtcNow)));
                        break;
                    }
            }

            return ValueTask.CompletedTask;
        };
    }
}