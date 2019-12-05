using System.Collections.Generic;

namespace Creobit.Advertising
{
    public interface IAdvertisementConfiguration
    {
        IEnumerable<IAdvertisement> Advertisements { get; }
        IEnumerable<IPlatformAuth> Platforms { get; }
    }
}
