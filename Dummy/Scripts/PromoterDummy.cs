using System;
using System.Collections.Generic;

namespace Creobit.Advertising
{
    public sealed class PromoterDummy : IPromoter
    {
        #region IPromoter

        public event Action<Exception> ExceptionDetected;

        IEnumerable<IAdvertisement> IPromoter.Advertisements => Advertisements;

        void IPromoter.Initialize(Action onComplete, Action onFailure)
        {
            UpdateAdvertisements();

            onComplete();
        }

        #endregion
        #region PromoterDummy

        public readonly PromoterConfigurationDummy Configuration;

        private IList<AdvertisementDummy> _advertisements;

        public PromoterDummy(PromoterConfigurationDummy configuration)
        {
            Configuration = configuration;
        }

        private IList<AdvertisementDummy> Advertisements
        {
            get => _advertisements ?? Array.Empty<AdvertisementDummy>();
            set => _advertisements = value;
        }

        internal void RaiseExceptionDetected(Exception exception)
        {
            ExceptionDetected?.Invoke(exception);
        }

        private void UpdateAdvertisements()
        {
            Advertisements = CreateAdvertisements();

            List<AdvertisementDummy> CreateAdvertisements()
            {
                var advertisements = new List<AdvertisementDummy>();

                foreach (var (AdvertisementId, Tag) in Configuration.AdvertisementMap)
                {
                    var advertisement = new AdvertisementDummy(AdvertisementId, Tag);

                    advertisements.Add(advertisement);
                }

                return advertisements;
            }
        }

        #endregion
    }
}
