#if CREOBIT_ADVERTISING_UNITY && (UNITY_ANDROID || UNITY_IOS)
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Advertisements;

namespace Creobit.Advertising
{
    internal sealed class UnityAdvertisement : IUnityAdvertisement
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

        string IAdvertisement.Id => Id;

        bool IAdvertisement.IsReady => IsReady;

        async void IAdvertisement.Prepare(Action onComplete, Action onFailure)
        {
            var cancellationTokenSource = new CancellationTokenSource();

            cancellationTokenSource.CancelAfter(Configuration.Timeout ?? 10000);

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
                Configuration.RaiseExceptionDetected(new TimeoutException());

                onFailure();
            }
            catch (Exception exception)
            {
                Configuration.RaiseExceptionDetected(exception);

                onFailure();
            }
            finally
            {
                cancellationTokenSource.Dispose();
            }
        }

        void IAdvertisement.Show(Action onComplete, Action onFailure)
        {
            if (!IsReady)
            {
                Configuration.RaiseExceptionDetected(new InvalidOperationException($"Advertisement with \"{Id}\" is not ready!"));

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
                if (result == ShowResult.Finished)
                {
                    onComplete();
                }
                else
                {
                    Configuration.RaiseExceptionDetected(new OperationCanceledException($"{nameof(result)}: {result}"));

                    onFailure();
                }
            }
        }

        #endregion
        #region IUnityAdvertisement

        string IUnityAdvertisement.PlacementId => PlacementId;

        #endregion
        #region UnityAdvertisement

        public readonly UnityConfiguration Configuration;
        public readonly string Id;
        public readonly string PlacementId;

        public UnityAdvertisement(UnityConfiguration configuration, string id, string placementId)
        {
            Configuration = configuration;
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
