#if CREOBIT_ADVERTISING_UNITY && (UNITY_ANDROID || UNITY_IOS)
using System;
using System.Collections.Generic;
using UnityEngine.Advertisements;

namespace Creobit.Advertising
{
    public sealed class UnityPromoter : IPromoter, IUnityPromoter
    {
        #region IPromoter

        public event Action<Exception> ExceptionDetected;

        IEnumerable<IAdvertisement> IPromoter.Advertisements => Advertisements;

        void IPromoter.Initialize(Action onComplete, Action onFailure)
        {
            if (string.IsNullOrWhiteSpace(Configuration.GameId))
            {
                RaiseExceptionDetected(new InvalidOperationException($"{nameof(Configuration.GameId)} is null or whitespace!"));

                onFailure();

                return;
            }

            Advertisement.debugMode = Configuration.DebugMode;
            Advertisement.Initialize(Configuration.GameId, Configuration.TestMode);
            UpdateAdvertisements();

            onComplete();
        }

        #endregion
        #region IUnityPromoter

        UnityPromoterConfiguration IUnityPromoter.Configuration => Configuration;

        #endregion
        #region UnityPromoter

        public readonly UnityPromoterConfiguration Configuration;

        private IList<UnityAdvertisement> _advertisements;

        public UnityPromoter(UnityPromoterConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IList<UnityAdvertisement> Advertisements
        {
            get => _advertisements ?? Array.Empty<UnityAdvertisement>();
            set => _advertisements = value;
        }

        internal void RaiseExceptionDetected(Exception exception)
        {
            ExceptionDetected?.Invoke(exception);
        }

        private void UpdateAdvertisements()
        {
            Advertisements = CreateAdvertisements();

            List<UnityAdvertisement> CreateAdvertisements()
            {
                var advertisements = new List<UnityAdvertisement>();

                foreach (var (AdvertisementId, PlacementId) in Configuration.AdvertisementMap)
                {
                    var advertisement = new UnityAdvertisement(this, AdvertisementId, PlacementId);

                    advertisements.Add(advertisement);
                }

                return advertisements;
            }
        }

        #endregion
    }
}
#endif
