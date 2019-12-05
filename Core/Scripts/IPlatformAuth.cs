using System;

public interface IPlatformAuth
{
    string Id { get; }
    bool IsInitialized { get; }

    void Initialize(Action onSuccess, Action onFailed);
}
