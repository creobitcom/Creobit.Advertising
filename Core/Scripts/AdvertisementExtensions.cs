using System;
using System.Threading.Tasks;

namespace Creobit.Advertising
{
    public static class AdvertisementExtensions
    {
        #region AdvertisementExtensions

        private const int MillisecondsDelay = 10;

        public static async Task PrepareAsync(this IAdvertisement self)
        {
            var promoter = self.Promoter;
            var invokeException = default(Exception);
            var invokeResult = default(bool?);

            promoter.ExceptionDetected += OnExceptionDetected;
            self.Prepare(
                () => invokeResult = true,
                () => invokeResult = false);

            while (!invokeResult.HasValue)
            {
                await Task.Delay(MillisecondsDelay);
            }

            promoter.ExceptionDetected -= OnExceptionDetected;

            if (!invokeResult.Value)
            {
                throw invokeException ?? new InvalidOperationException();
            }

            void OnExceptionDetected(Exception exception)
            {
                invokeException = exception;
            }
        }

        public static async Task ShowAsync(this IAdvertisement self)
        {
            var promoter = self.Promoter;
            var outputException = default(Exception);
            var outputShowResult = default(ShowResult?);

            promoter.ExceptionDetected += OnExceptionDetected;
            self.Show(
                () => outputShowResult = ShowResult.Complete,
                () => outputShowResult = ShowResult.Skip,
                () => outputShowResult = ShowResult.Failure);

            while (!outputShowResult.HasValue)
            {
                await Task.Delay(MillisecondsDelay);
            }

            promoter.ExceptionDetected -= OnExceptionDetected;

            switch (outputShowResult.Value)
            {
                case ShowResult.Complete:
                    break;
                case ShowResult.Skip:
                    throw new SkipException();
                case ShowResult.Failure:
                    throw outputException ?? new InvalidOperationException();
            }

            void OnExceptionDetected(Exception exception)
            {
                outputException = exception;
            }
        }

        private enum ShowResult
        {
            Complete,
            Skip,
            Failure
        }

        #endregion
    }
}
