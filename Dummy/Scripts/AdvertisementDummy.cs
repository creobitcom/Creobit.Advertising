using System;
using System.Text;

namespace Creobit.Advertising
{
    public sealed class AdvertisementDummy : IAdvertisement, IAdvertisementDummy
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

        IPromoter IAdvertisement.Promoter => Promoter;

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
                Promoter.RaiseExceptionDetected(new InvalidOperationException($"Advertisement with \"{Id}\" is not ready!"));

                onFailure();
            }
        }

        #endregion
        #region IAdvertisementDummy

        IPromoterDummy IAdvertisementDummy.Promoter => Promoter;

        string IAdvertisementDummy.Tag => Tag;

        #endregion
        #region AdvertisementDummy

        public readonly PromoterDummy Promoter;
        public readonly string Id;
        public readonly string Tag;

        public AdvertisementDummy(PromoterDummy promoter, string id, string tag)
        {
            Promoter = promoter;
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
