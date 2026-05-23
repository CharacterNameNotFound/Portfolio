namespace GameWideSystems.RNGManagement
{
    public interface IRNGManager
    {
        public IRNGProvider GetRandomProvider(RNGGroup rngGroup);
    }
}