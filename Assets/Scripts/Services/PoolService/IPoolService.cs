using UnityEngine;

namespace CompanyName.PoolService
{
    public interface IPool<T> where T : Component
    {
        T Get();
        
        void Return(T item);
        
        void Prewarm(int count);
        
        void Clear();
    }
}
