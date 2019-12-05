using System;
using System.Collections.Generic;

namespace Creobit.Advertising
{
    public sealed class PromoterConfigurationDummy
    {
        private IEnumerable<(string AdvertisementId, string Tag)> _advertisementMap;

        public IEnumerable<(string AdvertisementId, string Tag)> AdvertisementMap
        {
            get => _advertisementMap ?? Array.Empty<(string AdvertisementId, string Tag)>();
            set => _advertisementMap = value;
        }
    }
}
