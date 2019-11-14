using System;
using System.Text;

namespace Creobit.Advertising
{
    public sealed class FakeAdvertisement : IAdvertisement, IFakeAdvertisement
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
        #region IFakeAdvertisement

        IFakePromoter IFakeAdvertisement.Promoter => Promoter;

        string IFakeAdvertisement.Tag => Tag;

        #endregion
        #region FakeAdvertisement

        public readonly FakePromoter Promoter;
        public readonly string Id;
        public readonly string Tag;

        public FakeAdvertisement(FakePromoter promoter, string id, string tag)
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
