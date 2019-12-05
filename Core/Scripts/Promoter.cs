using System;
using System.Collections.Generic;
using System.Linq;

namespace Creobit.Advertising
{
    public sealed class Promoter : IPromoter
    {
        #region IPromoter

        public event Action<Exception> ExceptionDetected;

        IEnumerable<IAdvertisement> IPromoter.Advertisements => Advertisements.
            Where(advertisement => _platformAuthManager.AuthenticatedPlatforms.Contains(advertisement.PlatformId));

        #endregion
        #region PromoterDummy

        private IEnumerable<IAdvertisement> _advertisements;

        public Promoter(IEnumerable<IAdvertisement> usedAdvertisements, IPlatformAuthManager platformAuthManager)
        {
            Advertisements = usedAdvertisements;
            _platformAuthManager = platformAuthManager;
        }

        private IEnumerable<IAdvertisement> Advertisements
        {
            get => _advertisements ?? Array.Empty<IAdvertisement>();
            set => _advertisements = value;
        }

        internal void RaiseExceptionDetected(Exception exception)
        {
            ExceptionDetected?.Invoke(exception);
        }

        private IPlatformAuthManager _platformAuthManager;

        #endregion
    }
}
