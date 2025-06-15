using CompanyName.Game;
using UnityEngine;

namespace CompanyName.PoolService
{
  public abstract class Pool<T> : MonoBehaviour where T : PoolableComponent
  {
    [SerializeField] private T _pooledItemPrefab;
    [SerializeField] private int _poolSize;

    private  ObjectPool<T> _pool;

    public ObjectPool<T> Pooled
    {
      get
      {
        if (_pool == null)
        {
          _pool = CreatePool(_pooledItemPrefab, _poolSize);
        }
        return _pool;
      }
    }

    private ObjectPool<T> CreatePool<T>(T prefab, int size) where T : PoolableComponent
    {
      return new ObjectPool<T>(prefab, transform, size);
    }
  }
}