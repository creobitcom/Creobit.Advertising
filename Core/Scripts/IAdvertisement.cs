using System;

namespace Creobit.Advertising
{
    public interface IAdvertisement
    {
        #region IAdvertisement

        string Id
        {
            get;
        }

        bool IsReady
        {
            get;
        }

        IPromoter Promoter
        {
            get;
        }

        void Prepare(Action onComplete, Action onFailure);

        void Show(Action onComplete, Action onFailure);

        #endregion
    }
}
