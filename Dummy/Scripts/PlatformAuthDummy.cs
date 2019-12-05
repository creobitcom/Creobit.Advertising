using System;

namespace Creobit.Advertising 
{
    public class PlatformAuthDummy : IPlatformAuth
    {
        #region IPlatformAuth

        public string Id => "Dummy";

        public bool IsInitialized { get; private set; }

        public void Initialize(Action onSuccess, Action onFailed)
        {
            IsInitialized = true;
            onSuccess?.Invoke();
        }

        #endregion

        public PlatformAuthDummy()
        {
            IsInitialized = false;
        }
    }
}
