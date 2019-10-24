using System;
using System.Collections.Generic;

namespace Creobit.Advertising
{
    public sealed class FakeConfiguration
    {
        #region FakeConfiguration

        private IEnumerable<(string AdvertisementId, string Tag)> _advertisementMap;

        public event Action<Exception> ExceptionDetected;

        public IEnumerable<(string AdvertisementId, string Tag)> AdvertisementMap
        {
            get => _advertisementMap ?? Array.Empty<(string AdvertisementId, string Tag)>();
            set => _advertisementMap = value;
        }

        internal void RaiseExceptionDetected(Exception exception)
        {
            ExceptionDetected?.Invoke(exception);
        }

        #endregion
    }
}
