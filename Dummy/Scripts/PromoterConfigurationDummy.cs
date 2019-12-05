using System;
using System.Collections.Generic;

namespace Creobit.Advertising
{
    public sealed class PromoterConfigurationDummy : IAdvertisementConfiguration
    {
        private IEnumerable<IAdvertisement> _advertisementMap;

        public IEnumerable<IAdvertisement> Advertisements
        {
            get => _advertisementMap ?? Array.Empty<IAdvertisement>();
            set => _advertisementMap = value;
        }

        public PromoterConfigurationDummy()
        {
            _advertisementMap = new AdvertisementDummy[]
            {
                new AdvertisementDummy("Example", "rewardedVideo")
            };
        }
    }
}
