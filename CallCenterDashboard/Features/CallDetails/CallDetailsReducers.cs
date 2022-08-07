using Fluxor;

namespace CallingDashboard.Features.CallDetails
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

        [ReducerMethod]
        public static CallDetailsState InitializeCallDetailState(CallDetailsState state, CallDetailStateSetInitializationAction action) =>
            state with
            {
                Initialized = true
            };

        [ReducerMethod]
        public static CallDetailsState SetParticipants(CallDetailsState state, ParticipantSetDataAction action)
        {
            if (state.PreviousId == action.Id)
            {
                return state with
                {
                    Participants = action.Participants
                };
            }

            return state;
        }

        [ReducerMethod]
        public static CallDetailsState ClearCallDetail(CallDetailsState state, CallDetailClearAction action)
        {
            if (state.PreviousId == action.Id)
            {
                return state with
                {
                    Participants = null,
                    CallConnectionProperties = null
                };
            }
            return state;
        }
    }

}
