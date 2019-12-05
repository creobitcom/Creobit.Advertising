namespace Creobit.Advertising
{
    public interface IAdvertisementDummy
    {
        IPromoterDummy Promoter
        {
            get;
        }

        string Tag
        {
            get;
        }
    }
}
