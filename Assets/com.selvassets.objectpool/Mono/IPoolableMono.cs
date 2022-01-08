namespace ObjectPool
{
    public interface IPoolableMono
    {
        void Deactivate();
        void Activate();
    }
}