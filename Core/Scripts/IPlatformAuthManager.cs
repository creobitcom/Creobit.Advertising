using System;
using System.Collections.Generic;

public interface IPlatformAuthManager
{
    IEnumerable<string> AuthenticatedPlatforms { get; }
    bool IsInitialized { get; }

    void Initialize(Action onInitialize);
}
