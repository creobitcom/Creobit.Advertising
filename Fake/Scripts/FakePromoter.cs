using System;
using System.Collections.Generic;

namespace Creobit.Advertising
{
    public sealed class FakePromoter : IPromoter
    {
        #region IPromoter

        IEnumerable<IAdvertisement> IPromoter.Advertisements => Advertisements;

        void IPromoter.Initialize(Action onComplete, Action onFailure)
        {
            UpdateAdvertisements();

            onComplete();
        }

        #endregion
        #region FakePromoter

        public readonly FakeConfiguration Configuration;

        private IList<IAdvertisement> _advertisements;

        public FakePromoter(FakeConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IList<IAdvertisement> Advertisements
        {
            get => _advertisements ?? Array.Empty<IAdvertisement>();
            set => _advertisements = value;
        }

        private void UpdateAdvertisements()
        {
            Advertisements = CreateAdvertisements();

            List<IAdvertisement> CreateAdvertisements()
            {
                var advertisements = new List<IAdvertisement>();

                foreach (var (AdvertisementId, Tag) in Configuration.AdvertisementMap)
                {
                    var advertisement = new FakeAdvertisement(Configuration, AdvertisementId, Tag);

                    advertisements.Add(advertisement);
                }

                return advertisements;
            }
        }

        #endregion
    }
}
