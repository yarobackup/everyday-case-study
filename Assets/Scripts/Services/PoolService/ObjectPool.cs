using System.Collections.Generic;
using UnityEngine;

namespace CompanyName.PoolService
{
    public class ObjectPool<T> : IPool<T> where T : PoolableComponent
    {
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly Queue<T> _availableObjects;
        private readonly HashSet<T> _allObjects;

        public ObjectPool(T prefab, Transform parent = null, int initialSize = 10)
        {
            _prefab = prefab ?? throw new System.ArgumentNullException(nameof(prefab));
            _parent = parent;
            _availableObjects = new Queue<T>();
            _allObjects = new HashSet<T>();

            if (initialSize < 0)
            {
                Debug.LogWarning("Initial pool size cannot be negative. Setting to 0.");
                initialSize = 0;
            }

            Prewarm(initialSize);
        }

        public T Get()
        {
            T item;

            if (_availableObjects.Count > 0)
            {
                item = _availableObjects.Dequeue();
            }
            else
            {
                item = CreateNewObject();
            }

            // Null check for destroyed objects
            if (item == null)
            {
                Debug.LogWarning($"Found null object in pool for {typeof(T).Name}. Cleaning up...");
                CleanupNullObjects();
                return Get();
            }

            item.OnBeforeGetFromPool();

            item.gameObject.SetActive(true);
            item.OnGetFromPool();

            return item;
        }

        public void Return(T item)
        {
            if (item == null)
            {
                Debug.LogWarning("Trying to return null object to pool!");
                return;
            }

            if (!_allObjects.Contains(item))
            {
                Debug.LogWarning($"Trying to return object that doesn't belong to this pool: {item.name}");
                return;
            }

            item.OnBeforeReturnToPool();

            // Deactivate and reset the object
            item.gameObject.SetActive(false);
            item.transform.SetParent(_parent);

            // Add back to available queue
            _availableObjects.Enqueue(item);
            item.OnReturnToPool();
        }

        public void Prewarm(int count)
        {
            if (count < 0)
            {
                Debug.LogWarning("Cannot prewarm with negative count");
                return;
            }

            for (int i = 0; i < count; i++)
            {
                T obj = CreateNewObject();
                obj.gameObject.SetActive(false);
                _availableObjects.Enqueue(obj);
            }

            Debug.Log($"Prewarmed pool with {count} objects of type {typeof(T).Name}");
        }

        public void Clear()
        {
            foreach (T obj in _allObjects)
            {
                if (obj != null)
                {
                    Object.Destroy(obj.gameObject);
                }
            }

            _availableObjects.Clear();
            _allObjects.Clear();
        }
        public void CleanupNullObjects()
        {
            // Clean up available queue
            var tempQueue = new Queue<T>();
            while (_availableObjects.Count > 0)
            {
                var obj = _availableObjects.Dequeue();
                if (obj != null)
                {
                    tempQueue.Enqueue(obj);
                }
            }

            // Restore cleaned queue
            while (tempQueue.Count > 0)
            {
                _availableObjects.Enqueue(tempQueue.Dequeue());
            }

            // Clean up all objects set
            _allObjects.RemoveWhere(obj => obj == null);

            Debug.Log($"Cleaned up null objects from pool. Current count: {_allObjects.Count}");
        }

        /// <summary>
        /// Creates a new object from the prefab.
        /// </summary>
        private T CreateNewObject()
        {
            T newObject = Object.Instantiate(_prefab, _parent);


#if UNITY_EDITOR
            newObject.name = $"{_prefab.name} (Pooled)";
#endif


            _allObjects.Add(newObject);
            newObject.OnCreate();
            return newObject;
        }
    }

    public class PoolableComponent : MonoBehaviour
    {
        public virtual void OnCreate() { }
        public virtual void OnBeforeGetFromPool() { }
        public virtual void OnGetFromPool() { }
        public virtual void OnBeforeReturnToPool() { }
        public virtual void OnReturnToPool() { }
    }
}