namespace ObjectPool
{
    public interface IPoolObjectMonoCreator<T>
    {
        T Create(T prefab);
    }
}