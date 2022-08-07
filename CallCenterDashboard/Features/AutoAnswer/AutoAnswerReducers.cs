using Fluxor;

namespace CallCenterDashboard.Features.AutoAnswer;

public static class AutoAnswerReducers
{
    [ReducerMethod(typeof(AutoAnswerInitializeAction))]
    public static AutoAnswerState OnInitialize(AutoAnswerState state) =>
        state with
        {
            Initialized = true
        };

    [ReducerMethod]
    public static AutoAnswerState OnSet(AutoAnswerState state, AutoAnswerSetAction action) =>
        state with
        {
            Enabled = action.Enabled,
            PhoneNumber = action.PhoneNumber
        };
}