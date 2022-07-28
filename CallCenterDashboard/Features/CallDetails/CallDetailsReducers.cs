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
            var isUpdatedDictionary = new ConcurrentDictionary<string, bool?>(state.IsUpToDateList);
            isUpdatedDictionary.TryAdd(action.Id, true);

            var callConnectionPropertiesDictionary = new ConcurrentDictionary<string, CallConnectionProperties>(state.CallConnectionPropertiesList);
            callConnectionPropertiesDictionary.TryAdd(action.Id, action.CallConnectionProperties);

            var ParticipantsDictionary = new ConcurrentDictionary<string, IEnumerable<CallParticipant>>(state.ParticipantsList);
            ParticipantsDictionary.TryAdd(action.Id, action.Participants);
            return state with
            {
                IsUpToDateList = isUpdatedDictionary,
                CallConnectionPropertiesList = callConnectionPropertiesDictionary,
                ParticipantsList = ParticipantsDictionary
            };
        }

        [ReducerMethod]
        public static CallDetailsState HandleUpdateParticipants(CallDetailsState state, UpdateParticipantsWithDataAction action)
        {
            state.ParticipantsList.TryGetValue(action.Id, out var participants);
            state.IsUpToDateList.TryGetValue(action.Id, out var IsUpdatToDate);
            if (participants != null && IsUpdatToDate != null)
            { 
                var ParticipantDictionary = new ConcurrentDictionary<string, IEnumerable<CallParticipant>>(state.ParticipantsList);
                ParticipantDictionary.TryUpdate(action.Id, action.Data, participants);
                var IsUpToDateDictionary = new ConcurrentDictionary<string, bool?>(state.IsUpToDateList);
                IsUpToDateDictionary.TryUpdate(action.Id, true, IsUpdatToDate);
                return state with
                {
                    ParticipantsList = ParticipantDictionary,
                    IsUpToDateList = IsUpToDateDictionary
                };
            }
            return state;
        }

        [ReducerMethod]
        public static CallDetailsState HandleParticipantUpdated(CallDetailsState state, ParticipantsUpdatedAction action)
        {
            var newDictionary = new ConcurrentDictionary<string, bool?>(state.IsUpToDateList);
            newDictionary.TryUpdate(action.Id, false, true);
            return state with
            {
                IsUpToDateList = newDictionary
            };
        }

        [ReducerMethod]
        public static CallDetailsState HandleRemoveCallDetail(CallDetailsState state, RemoveCallDetailAction action)
        {
            var IsUpToDateDictionary = new ConcurrentDictionary<string, bool?>(state.IsUpToDateList);
            IsUpToDateDictionary.TryRemove(action.Id, out var isUpToDate);

            var  CallDetialDictionary = new ConcurrentDictionary<string, CallConnectionProperties>(state.CallConnectionPropertiesList);
            IsUpToDateDictionary.TryRemove(action.Id, out var callConnection);

            var ParticipantsDictionary = new ConcurrentDictionary<string, IEnumerable<CallParticipant>>(state.ParticipantsList);
            ParticipantsDictionary.TryRemove(action.Id, out var participants);
            return state with
            {
                IsUpToDateList = IsUpToDateDictionary,
                CallConnectionPropertiesList = CallDetialDictionary,
                ParticipantsList = ParticipantsDictionary
            };
        }
    }

}
