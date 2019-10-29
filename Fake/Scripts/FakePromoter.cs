using System;
using System.Collections.Generic;

namespace Creobit.Advertising
{
    public sealed class FakePromoter : IPromoter, IFakePromoter
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
        #region IFakePromoter

        FakePromoterConfiguration IFakePromoter.Configuration => Configuration;

        #endregion
        #region FakePromoter

        public readonly FakePromoterConfiguration Configuration;

        private IList<FakeAdvertisement> _advertisements;

        public FakePromoter(FakePromoterConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IList<FakeAdvertisement> Advertisements
        {
            get => _advertisements ?? Array.Empty<FakeAdvertisement>();
            set => _advertisements = value;
        }

        internal void RaiseExceptionDetected(Exception exception)
        {
            ExceptionDetected?.Invoke(exception);
        }

        private void UpdateAdvertisements()
        {
            Advertisements = CreateAdvertisements();

            List<FakeAdvertisement> CreateAdvertisements()
            {
                var advertisements = new List<FakeAdvertisement>();

                foreach (var (AdvertisementId, Tag) in Configuration.AdvertisementMap)
                {
                    var advertisement = new FakeAdvertisement(this, AdvertisementId, Tag);

                    advertisements.Add(advertisement);
                }

                return advertisements;
            }
        }

        #endregion
    }
}
