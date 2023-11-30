﻿using System.Text.Json;

namespace UserManagement.Api.ExceptionHandler;

internal sealed record ErrorDetails(int StatusCode, string Message)
{
    public override string ToString() => JsonSerializer.Serialize(this);
}
