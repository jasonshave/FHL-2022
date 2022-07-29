using Fluxor;

namespace CallCenterDashboard.Features.CallDetails
{
    public static class CallDetailsReducers
    {
        [ReducerMethod]
        public static CallDetailsState SetCallDetail(CallDetailsState state, CallDetailSetDataAction action) =>
            state with
            {
                PreviousId = action.Id,
                CallConnectionProperties = action.CallConnectionProperties,
                Participants = action.Participants
            };
    }

}
