using System;
using System.Collections.Concurrent;
using UnityEngine;

namespace ObjectPool
{
    public class ObjectMonoPool<T> where T : MonoBehaviour, IPoolableMono
    {
        private readonly ConcurrentBag<T> _container = new ConcurrentBag<T>();

        private readonly IPoolObjectMonoCreator<T> _objectCreator;

        public int Count { get { return _container.Count; } }

        public ObjectMonoPool(IPoolObjectMonoCreator<T> creator)
        {
            if (creator == null)
            {
                throw new ArgumentNullException("Creator can't be null.");
            }

            _objectCreator = creator;
        }

        public T GetObject(T prefab)
        {
            T obj = null;

            if (!_container.TryTake(out obj))
            {
                obj = _objectCreator.Create(prefab);
            }

            obj.Activate();

            obj.Active = true;

            return obj;
        }

        public void ReturnObject(ref T obj)
        {
            obj.Deactivate();

            obj.Active = false;

            _container.Add(obj);
            obj = null;
        }
    }
}