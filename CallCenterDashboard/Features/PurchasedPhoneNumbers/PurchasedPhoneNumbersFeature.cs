using CallingDashboard.Models;
using Fluxor;

namespace CallingDashboard.Features.PurchasedPhoneNumbers;

public class PurchasedPhoneNumbersFeature : Feature<PurchasedPhoneNumbersState>
{
    public override string GetName() => "PurchasedPhoneNumbers";

    protected override PurchasedPhoneNumbersState GetInitialState() => new(false, false, Array.Empty<PhoneNumberConfiguration>());
}