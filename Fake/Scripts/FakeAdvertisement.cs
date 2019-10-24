using System;
using System.Text;

namespace Creobit.Advertising
{
    public sealed class FakeAdvertisement : IFakeAdvertisement
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

        void IAdvertisement.Show(Action onComplete, Action onFailure)
        {
            if (IsReady)
            {
                IsReady = false;

                onComplete();
            }
            else
            {
                Configuration.RaiseExceptionDetected(new InvalidOperationException($"Advertisement with \"{Id}\" is not ready!"));

                onFailure();
            }
        }

        #endregion
        #region IFakeAdvertisement

        string IFakeAdvertisement.Tag => Tag;

        #endregion
        #region FakeAdvertisement

        public readonly FakeConfiguration Configuration;
        public readonly string Id;
        public readonly string Tag;

        public FakeAdvertisement(FakeConfiguration configuration, string id, string tag)
        {
            Configuration = configuration;
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
