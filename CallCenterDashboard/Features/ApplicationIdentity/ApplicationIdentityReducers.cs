using Fluxor;

namespace CallCenterDashboard.Features.ApplicationIdentity;

public static class ApplicationIdentityReducers
{
    [ReducerMethod]
    public static ApplicationIdentityState OnInitialize(ApplicationIdentityState state, ApplicationIdentityInitializeAction action) =>
        state with
        {
            Initialized = true
        };

    [ReducerMethod]
    public static ApplicationIdentityState OnSetName(ApplicationIdentityState state, ApplicationIdentitySetNameAction action) =>
        state with
        {
            ApplicationName = action.ApplicationName
        };

    [ReducerMethod]
    public static ApplicationIdentityState OnSetIdentity(ApplicationIdentityState state,
        ApplicationIdentitySetIdentityAction action) =>
        state with
        {
            Id = action.identity
        };
}