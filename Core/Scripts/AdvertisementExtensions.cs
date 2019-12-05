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
            var invokeResult = default(bool?);

            self.Prepare(
                () => invokeResult = true,
                () => invokeResult = false);

            while (!invokeResult.HasValue)
            {
                await Task.Delay(MillisecondsDelay);
            }
        }

        #endregion
    }
}
