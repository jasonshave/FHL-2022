using Azure.Communication.CallingServer;
using Fluxor;
using System.Collections.Concurrent;

namespace CallCenterDashboard.Features.CallDetails
{
    public static class CallDetailsReducers
    {
        [ReducerMethod]
        public static CallDetailsState SetCallDetail(CallDetailsState state, CallDetailSetDataAction action)
        {
            return state with
            {
                PreviousId = action.Id,
                CallConnectionProperties = action.CallConnectionProperties,
                Participants = action.Participants
            };
        }
    }

}
