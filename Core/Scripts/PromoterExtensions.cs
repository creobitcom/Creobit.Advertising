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
            var invokeResult = default(bool?);

            self.Initialize(
                () => invokeResult = true,
                () => invokeResult = false);

            while (!invokeResult.HasValue)
            {
                await Task.Delay(MillisecondsDelay);
            }

            if (!invokeResult.Value)
            {
                throw new InvalidOperationException();
            }
        }

        #endregion
    }
}
