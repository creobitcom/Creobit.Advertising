using System;
using System.Collections.Generic;

namespace Creobit.Advertising
{
    public sealed class UnityConfiguration
    {
        #region UnityConfiguration

        public readonly bool DebugMode;
        public readonly string GameId;
        public readonly bool TestMode;

        private IEnumerable<(string AdvertisementId, string PlacementId)> _advertisementMap;

        public event Action<Exception> ExceptionDetected;

        public UnityConfiguration(string gameId, bool debugMode, bool testMode)
        {
            GameId = gameId;
            DebugMode = debugMode;
            TestMode = testMode;
        }

        public IEnumerable<(string AdvertisementId, string PlacementId)> AdvertisementMap
        {
            get => _advertisementMap ?? Array.Empty<(string AdvertisementId, string PlacementId)>();
            set => _advertisementMap = value;
        }

        public int? Timeout
        {
            get;
            set;
        }

        internal void RaiseExceptionDetected(Exception exception)
        {
            ExceptionDetected?.Invoke(exception);
        }

        #endregion
    }
}
