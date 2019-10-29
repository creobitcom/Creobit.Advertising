namespace Creobit.Advertising
{
    public interface IFakeAdvertisement
    {
        #region IFakeAdvertisement

        IFakePromoter Promoter
        {
            get;
        }

        string Tag
        {
            get;
        }

        #endregion
    }
}
