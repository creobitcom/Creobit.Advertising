using System;
using System.Collections.Generic;

namespace Creobit.Advertising
{
    public sealed class PromoterDummy : IPromoter
    {
        #region IPromoter

        public event Action<Exception> ExceptionDetected;

        IEnumerable<IAdvertisement> IPromoter.Advertisements => Advertisements;

        #endregion
        #region PromoterDummy

        private IEnumerable<IAdvertisement> _advertisements;

        public PromoterDummy(IEnumerable<IAdvertisement> usedAdvertisements)
        {
            _advertisements = usedAdvertisements;
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

        #endregion
    }
}
