using Fluxor;

namespace CallCenterDashboard.Features.PurchasedPhoneNumbers;

public static class PurchasedPhoneNumbersReducers
{
    [ReducerMethod(typeof(PurchasedPhoneNumbersInitializeAction))]
    public static PurchasedPhoneNumbersState OnInitialize(PurchasedPhoneNumbersState state) =>
        state with
        {
            Initialized = true,
            Loading = true
        };

    [ReducerMethod]
    public static PurchasedPhoneNumbersState OnSetLoaded(PurchasedPhoneNumbersState state, PurchasedPhoneNumbersSetLoadedAction action) =>
        state with
        {
            Loading = false
        };

    [ReducerMethod]
    public static PurchasedPhoneNumbersState OnSetData(PurchasedPhoneNumbersState state,
        PurchasedPhoneNumbersSetDataAction action) =>
        state with
        {
            PhoneNumberConfigurations = action.PhoneNumbers
        };
}