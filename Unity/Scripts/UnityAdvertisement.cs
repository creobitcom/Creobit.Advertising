#if DISABLED
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Advertisements;

namespace Creobit.Advertising
{
    internal sealed class UnityAdvertisement : IAdvertisement, IUnityAdvertisement
    {
        #region Object

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"{{ ");
            stringBuilder.Append($"{nameof(Id)}:{Id} ");
            stringBuilder.Append($"{nameof(IsReady)}:{IsReady} ");
            stringBuilder.Append($"{nameof(PlacementId)}:{PlacementId} ");
            stringBuilder.Append($"}}");

            return stringBuilder.ToString();
        }

        #endregion
        #region IAdvertisement

        bool IAdvertisement.IsReady => IsReady;

        async void IAdvertisement.Prepare(Action onComplete, Action onFailure)
        {
            var configuration = Promoter.Configuration;
            var cancellationTokenSource = new CancellationTokenSource();

            cancellationTokenSource.CancelAfter(configuration.Timeout ?? 10000);

            try
            {
                var token = cancellationTokenSource.Token;

                while (!IsReady)
                {
                    await Task.Delay(10);

                    token.ThrowIfCancellationRequested();
                }

                onComplete();
            }
            catch (TaskCanceledException)
            {
                Promoter.RaiseExceptionDetected(new TimeoutException());

                onFailure();
            }
            catch (Exception exception)
            {
                Promoter.RaiseExceptionDetected(exception);

                onFailure();
            }
            finally
            {
                cancellationTokenSource.Dispose();
            }
        }

        void IAdvertisement.Show(Action onComplete, Action onSkip, Action onFailure)
        {
            if (!IsReady)
            {
                Promoter.RaiseExceptionDetected(new InvalidOperationException($"Advertisement with \"{Id}\" is not ready!"));

                onFailure();

                return;
            }

            var showOptions = new ShowOptions()
            {
                resultCallback = OnResult
            };

            if (string.IsNullOrWhiteSpace(PlacementId))
            {
                Advertisement.Show(showOptions);
            }
            else
            {
                Advertisement.Show(PlacementId, showOptions);
            }

            void OnResult(ShowResult result)
            {
                switch (result)
                {
                    case ShowResult.Failed:
                        Promoter.RaiseExceptionDetected(new OperationCanceledException($"{nameof(result)}: {result}"));
                        onFailure();
                        break;
                    case ShowResult.Skipped:
                        Promoter.RaiseExceptionDetected(new SkipException());
                        onSkip();
                        break;
                    case ShowResult.Finished:
                        onComplete();
                        break;
                }
            }
        }

        #endregion
        #region IUnityAdvertisement

        string IUnityAdvertisement.PlacementId => PlacementId;

        IUnityPromoter IUnityAdvertisement.Promoter => Promoter;

        #endregion
        #region UnityAdvertisement

        public readonly UnityPromoter Promoter;
        public readonly string Id;
        public readonly string PlacementId;

        public UnityAdvertisement(UnityPromoter promoter, string id, string placementId)
        {
            Promoter = promoter;
            Id = id;
            PlacementId = placementId;
        }

        private bool IsReady => string.IsNullOrWhiteSpace(PlacementId)
            ? Advertisement.IsReady()
            : Advertisement.IsReady(PlacementId);

        #endregion
    }
}
#endif
