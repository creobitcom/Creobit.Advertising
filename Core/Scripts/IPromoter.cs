using System;
using System.Collections.Generic;

namespace Creobit.Advertising
{
    public interface IPromoter
    {
        event Action<Exception> ExceptionDetected;

        IEnumerable<IAdvertisement> Advertisements
        {
            get;
        }
    }
}
