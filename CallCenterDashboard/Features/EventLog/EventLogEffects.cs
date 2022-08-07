using CallCenterDashboard.Interfaces;
using CallCenterDashboard.Models;
using Fluxor;

namespace CallCenterDashboard.Features.EventLog;

public class EventLogEffects
{
    private readonly IRepository<EventLogData> _eventLogRepository;

    public EventLogEffects(IRepository<EventLogData> eventLogRepository)
    {
        _eventLogRepository = eventLogRepository;
    }

    [EffectMethod]
    public Task OnInitialize(EventLogInitializeAction action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new EventLogSetDataAction(_eventLogRepository.List()));
        return Task.CompletedTask;
    }

    [EffectMethod]
    public Task OnAdd(EventLogAddAction action, IDispatcher dispatcher)
    {
        _eventLogRepository.Add(Guid.NewGuid().ToString(), action.EventLogData);
        return Task.CompletedTask;
    }
}