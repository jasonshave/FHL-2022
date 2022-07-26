﻿using CallingDashboard.Models;

namespace CallingDashboard.Features.ActiveCalls;

public record ActiveCallsState(bool Initialized, bool Loading, IEnumerable<CallData> CallData);