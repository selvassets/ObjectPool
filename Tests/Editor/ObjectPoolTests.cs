using NUnit.Framework;
using UnityEngine;

namespace ObjectPool
{
    public class ObjectPoolTests
    {
        [Test]
        public void GetAndReturnClassObject()
        {
            var pool = new ObjectPool<TestClassObject>(new DefaultObjectCreator<TestClassObject>());
            
            var obj = pool.GetObject();
            Assert.IsTrue(obj != null && obj is TestClassObject);

            pool.ReturnObject(ref obj);
            Assert.IsTrue(obj == null);
            Assert.AreEqual(pool.Count, 1);
        }

        [Test]
        public void GetAndReturnMonoObject()
        {
            var pool = new ObjectMonoPool<TestMonoObject>(new DefaultObjectMonoCreator<TestMonoObject>());

            var go = new GameObject();
            var mono = go.AddComponent<TestMonoObject>();

            var obj = pool.GetObject(mono);

            Assert.IsTrue(obj != null && obj is TestMonoObject);

            pool.ReturnObject(ref obj);
            Assert.IsTrue(obj == null);
            Assert.AreEqual(pool.Count, 1);
        }

        public class TestClassObject : IPoolable
        {
            public int Index { get; set; }

            public TestClassObject()
            {
                this.Index = -1;
            }

            void IPoolable.ResetState()
            {
                this.Index = -1;
            }
        }

        public class TestMonoObject : MonoBehaviour,  IPoolableMono
        {
            public int Index { get; set; }

            public TestMonoObject()
            {
                this.Index = -1;
            }

            void IPoolableMono.Deactivate()
            {
            }

            void IPoolableMono.Activate()
            {
            }
        }
    }
}