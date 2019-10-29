#if CREOBIT_ADVERTISING_UNITY && (UNITY_ANDROID || UNITY_IOS)
namespace Creobit.Advertising
{
    public interface IUnityPromoter
    {
        #region IUnityPromoter

        UnityPromoterConfiguration Configuration
        {
            get;
        }

        #endregion
    }
}
#endif
