using System;

namespace Creobit.Advertising
{
    public interface IAdvertisement
    {
        string Id
        {
            get;
        }

        string PlatformId
        {
            get;
        }

        string Tag
        {
            get;
        }

        bool IsReady
        {
            get;
        }

        void Prepare(Action onComplete, Action onFailure);

        void Show(Action onComplete, Action onSkip, Action onFailure);
    }
}
