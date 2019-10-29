﻿#if CREOBIT_ADVERTISING_UNITY && (UNITY_ANDROID || UNITY_IOS)
namespace Creobit.Advertising
{
    public interface IUnityAdvertisement
    {
        #region IUnityAdvertisement

        string PlacementId
        {
            get;
        }

        IUnityPromoter Promoter
        {
            get;
        }

        #endregion
    }
}
#endif
