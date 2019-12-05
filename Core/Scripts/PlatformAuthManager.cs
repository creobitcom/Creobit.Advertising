using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class PlatformAuthManager : IPlatformAuthManager
{
    #region IPlatformAuthManager

    public IEnumerable<string> AuthenticatedPlatforms
    {
        get
        {
            var result = default(IEnumerable<string>);

            if (IsInitialized)
            {
                result = _platforms.
                    Where(platform => platform.IsInitialized).
                    Select(platform => platform.Id);
            }
            return result;
        }
    }

    public bool IsInitialized => _waitingInitializations == 0;

    public void Initialize(Action onInitialize)
    {
        _waitingInitializations = _platforms.Count();

        foreach (var platform in _platforms)
        {
            platform.Initialize(
                () => OnInitializedResult(platform.Id, true), 
                () => OnInitializedResult(platform.Id, false));
        }

        void OnInitializedResult(string id, bool result)
        {
            _waitingInitializations--;
            Debug.Log($"Инициализация {id}; Результат {result}");

            if (_waitingInitializations == 0)
            {
                onInitialize?.Invoke();
            }
        }
    }

    #endregion

    public PlatformAuthManager(IEnumerable<IPlatformAuth> platforms)
    {
        _platforms = platforms;
    }

    private IEnumerable<IPlatformAuth> _platforms;
    private int _waitingInitializations = -1;
}
