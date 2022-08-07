﻿using CallCenterDashboard.Models;

namespace CallCenterDashboard.Features.PurchasedPhoneNumbers;

public record PurchasedPhoneNumbersInitializeAction;

public record PurchasedPhoneNumbersSetLoadedAction;

public record PurchasedPhoneNumbersSetDataAction(IEnumerable<PhoneNumberConfiguration> PhoneNumbers);

public record PurchasedPhoneNumbersSetAnswerModeAction(PhoneNumberConfiguration PhoneNumberConfiguration);