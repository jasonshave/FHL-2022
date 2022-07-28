using Azure.Communication.CallingServer;
using Fluxor;
using System.Collections.Concurrent;

namespace CallCenterDashboard.Features.CallDetails
{
    public static class CallDetailsReducers
    {
        [ReducerMethod]
        public static CallDetailsState HandleInitializeCallDetail(CallDetailsState state, InitializeCallDetailWithDataAction action)
        {
            return state with
            {
                IsInitialized = true,
                CallConnectionProperties = action.CallConnectionProperties,
                Participants = action.Participants
            };
        }
    }

}
