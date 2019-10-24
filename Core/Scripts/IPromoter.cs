﻿using System;
using System.Collections.Generic;

namespace Creobit.Advertising
{
    public interface IPromoter
    {
        #region IPromoter

        IEnumerable<IAdvertisement> Advertisements
        {
            get;
        }

        void Initialize(Action onComplete, Action onFailure);

        #endregion
    }
}
