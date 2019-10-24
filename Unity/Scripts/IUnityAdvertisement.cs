#if CREOBIT_ADVERTISING_UNITY && (UNITY_ANDROID || UNITY_IOS)
namespace Creobit.Advertising
{
    public interface IUnityAdvertisement : IAdvertisement
    {
        #region IUnityAdvertisement

        string PlacementId
        {
            get;
        }

        #endregion
    }
}
#endif
