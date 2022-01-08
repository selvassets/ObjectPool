using UnityEngine;

namespace ObjectPool
{
    public class DefaultObjectMonoCreator<T> : IPoolObjectMonoCreator<T> where T : MonoBehaviour, new()
    {
        T IPoolObjectMonoCreator<T>.Create(T prefab)
        {
            return Object.Instantiate(prefab);
        }
    }
}