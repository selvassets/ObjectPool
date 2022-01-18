namespace ObjectPool
{
    public interface IPoolableMono
    {
        bool Active { get; set; }
        void Deactivate();
        void Activate();
    }
}