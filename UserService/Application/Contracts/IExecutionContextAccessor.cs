﻿namespace UserService.Application.Contracts;

public interface IExecutionContextAccessor
{
    Guid UserId { get; }

    bool IsAvailable { get; }
}