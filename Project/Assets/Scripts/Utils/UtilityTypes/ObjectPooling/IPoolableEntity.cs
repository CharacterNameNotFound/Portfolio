namespace Utils.UtilityTypes.ObjectPooling
{
    public interface IPoolableEntity
    {
        // Called when objects send to pool
        public void OnPooled();
        // We do not want to have interface injected from Zenject, so better not to use interface here
        public void Dispose();
    }
}