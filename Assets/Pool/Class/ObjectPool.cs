using System;
using System.Collections.Concurrent;

namespace ObjectPool
{
    public class ObjectPool<T> where T : class, IPoolable
    {
        private readonly ConcurrentBag<T> _container = new ConcurrentBag<T>();

        private readonly IPoolObjectCreator<T> _objectCreator;

        public int Count { get { return _container.Count; } }

        public ObjectPool(IPoolObjectCreator<T> creator)
        {
            if (creator == null)
            {
                throw new ArgumentNullException("Creator can't be null.");
            }

            _objectCreator = creator;
        }

        public T GetObject()
        {
            T obj = null;

            if (_container.TryTake(out obj))
            {
                return obj;
            }
            return _objectCreator.Create();
        }

        public void ReturnObject(ref T obj)
        {
            obj.ResetState();
            _container.Add(obj);
            obj = null;
        }
    }
}