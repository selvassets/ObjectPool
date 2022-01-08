namespace ObjectPool
{
    public interface IPoolObjectCreator<T>
    {
        T Create();
    }
}