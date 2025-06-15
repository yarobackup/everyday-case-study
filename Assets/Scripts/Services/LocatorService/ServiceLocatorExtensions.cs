using UnityEngine;

namespace CompanyName.Services.SL
{
    public static class ServiceLocatorExtensions
    {
        public static void RegisterAsGlobalService<TInterface, TImplementation>(this TImplementation impl)
            where TImplementation : class, TInterface
        {
            ServiceLocator.Global.Register<TInterface>(impl);
        }

        public static void RegisterAsGlobalService<TImplementation>(this TImplementation impl)
            where TImplementation : class
        {
            ServiceLocator.Global.Register(impl);
        }

        public static MonoBehaviour RegisterAsSceneService<TImplementation>(this MonoBehaviour monoBehaviour, TImplementation impl)
            where TImplementation : class
        {
            ServiceLocator.ForSceneOf(monoBehaviour).Register(impl);
            return monoBehaviour;
        }
        public static MonoBehaviour RegisterAsObjectService<TImplementation>(this MonoBehaviour monoBehaviour, TImplementation impl)
            where TImplementation : class
        {
            ServiceLocator.For(monoBehaviour).Register(impl);
            return monoBehaviour;
        }

        public static MonoBehaviour GetService<TImplementation>(this MonoBehaviour monoBehaviour, out TImplementation impl)
            where TImplementation : class
        {
            ServiceLocator.ForSceneOf(monoBehaviour).Get(out impl);
            return monoBehaviour;
        }
        public static MonoBehaviour GetLocalService<TImplementation>(this MonoBehaviour monoBehaviour, out TImplementation impl)
            where TImplementation : class
        {
            ServiceLocator.For(monoBehaviour).Get(out impl);
            return monoBehaviour;
        }
    }
}