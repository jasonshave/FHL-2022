using Azure.Communication.CallingServer;
using System.Collections.Concurrent;

namespace CallCenterDashboard.Features.CallDetails
{
    public record CallDetailsState(ConcurrentDictionary<string, bool?> IsUpToDateList, ConcurrentDictionary<string, CallConnectionProperties> CallConnectionPropertiesList, ConcurrentDictionary<string, IEnumerable<CallParticipant>> ParticipantsList);
}
