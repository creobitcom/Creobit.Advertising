using System;
using System.Threading.Tasks;

namespace Creobit.Advertising
{
    public static class PromoterExtensions
    {
        #region PromoterExtensions

        private const int MillisecondsDelay = 10;

        public static async Task InitializeAsync(this IPromoter self)
        {
            var invokeException = default(Exception);
            var invokeResult = default(bool?);

            self.ExceptionDetected += OnExceptionDetected;
            self.Initialize(
                () => invokeResult = true,
                () => invokeResult = false);

            while (!invokeResult.HasValue)
            {
                await Task.Delay(MillisecondsDelay);
            }

            self.ExceptionDetected -= OnExceptionDetected;

            if (!invokeResult.Value)
            {
                throw invokeException ?? new InvalidOperationException();
            }

            void OnExceptionDetected(Exception exception)
            {
                invokeException = exception;
            }
        }

        #endregion
    }
}
