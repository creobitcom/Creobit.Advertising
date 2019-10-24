#if CREOBIT_ADVERTISING_UNITY && (UNITY_ANDROID || UNITY_IOS)
using System;
using System.Collections.Generic;
using UnityEngine.Advertisements;

namespace Creobit.Advertising
{
    public sealed class UnityPromoter : IPromoter
    {
        #region IPromoter

        IEnumerable<IAdvertisement> IPromoter.Advertisements => Advertisements;

        void IPromoter.Initialize(Action onComplete, Action onFailure)
        {
            if (string.IsNullOrWhiteSpace(Configuration.GameId))
            {
                Configuration.RaiseExceptionDetected(new InvalidOperationException($"{nameof(Configuration.GameId)} is null or whitespace!"));

                onFailure();

                return;
            }

            Advertisement.debugMode = Configuration.DebugMode;
            Advertisement.Initialize(Configuration.GameId, Configuration.TestMode);
            UpdateAdvertisements();

            onComplete();
        }

        #endregion
        #region UnityPromoter

        public readonly UnityConfiguration Configuration;

        private IList<IAdvertisement> _advertisements;

        public UnityPromoter(UnityConfiguration configuration)
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

                foreach (var (AdvertisementId, PlacementId) in Configuration.AdvertisementMap)
                {
                    var advertisement = new UnityAdvertisement(Configuration, AdvertisementId, PlacementId);

                    advertisements.Add(advertisement);
                }

                return advertisements;
            }
        }

        #endregion
    }
}
#endif
