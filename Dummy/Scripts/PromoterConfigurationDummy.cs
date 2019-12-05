using System;
using System.Collections.Generic;

namespace Creobit.Advertising
{
    public sealed class PromoterConfigurationDummy : IAdvertisementConfiguration
    {
        #region IAdvertisementConfiguration

        public IEnumerable<IAdvertisement> Advertisements
        {
            get => _advertisementMap ?? Array.Empty<IAdvertisement>();
            private set => _advertisementMap = value;
        }

        public IEnumerable<IPlatformAuth> Platforms
        {
            get => _platformMap ?? Array.Empty<IPlatformAuth>();
            private set => _platformMap = value;
        }

        #endregion

        private IEnumerable<IAdvertisement> _advertisementMap;
        private IEnumerable<IPlatformAuth> _platformMap;


        public PromoterConfigurationDummy()
        {
            _advertisementMap = new AdvertisementDummy[]
            {
                new AdvertisementDummy("Example", "rewardedVideo")
            };

            _platformMap = new PlatformAuthDummy[]
            {
                new PlatformAuthDummy()
            };
        }
    }
}
