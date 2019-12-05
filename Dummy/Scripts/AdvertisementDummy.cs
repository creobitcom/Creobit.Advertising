using System;
using System.Text;

namespace Creobit.Advertising
{
    public sealed class AdvertisementDummy : IAdvertisement
    {
        #region Object

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"{{ ");
            stringBuilder.Append($"{nameof(Id)}:{Id} ");
            stringBuilder.Append($"{nameof(IsReady)}:{IsReady} ");
            stringBuilder.Append($"{nameof(Tag)}:{Tag} ");
            stringBuilder.Append($"}}");

            return stringBuilder.ToString();
        }

        #endregion
        #region IAdvertisement

        string IAdvertisement.Id => Id;

        bool IAdvertisement.IsReady => IsReady;

        void IAdvertisement.Prepare(Action onComplete, Action onFailure)
        {
            IsReady = true;

            onComplete();
        }

        void IAdvertisement.Show(Action onComplete, Action onSkip, Action onFailure)
        {
            if (IsReady)
            {
                IsReady = false;

                onComplete();
            }
            else
            {
                onFailure();
            }
        }

        #endregion
        #region AdvertisementDummy

        public readonly string Id;
        public readonly string Tag;

        public AdvertisementDummy(string id, string tag)
        {
            Id = id;
            Tag = tag;
        }

        private bool IsReady
        {
            get;
            set;
        }

        #endregion
    }
}
