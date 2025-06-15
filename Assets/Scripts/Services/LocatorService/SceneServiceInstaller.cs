using UnityEngine;

namespace CompanyName.Services.SL
{
  public abstract class SceneServiceInstallerBase : ServiceInstallerBase { }

  public abstract class SceneMonoServiceInstaller<TImplementation> : SceneServiceInstallerBase where TImplementation : Component
  {
    [SerializeField] protected TImplementation _service;

    public override void Register()
    {
      this.RegisterAsSceneService(_service);
      OnInit();
    }
  }

  public abstract class SceneMonoServiceInstaller<TInterface, TImplementation> : SceneMonoServiceInstaller<TImplementation>
    where TImplementation : Component, TInterface
    where TInterface : class
  {
    public override void Register()
    {
      this.RegisterAsSceneService<TInterface>(_service);
      OnInit();
    }
  }

  public abstract class SceneServiceInstaller<TImplementation> : SceneServiceInstallerBase where TImplementation : class
  {
    public override void Register()
    {
      var service = CreateService();
      this.RegisterAsSceneService(service);
      OnInit();
    }
    protected abstract TImplementation CreateService();
  }

  public abstract class SceneServiceInstaller<TInterface, TImplementation> : SceneServiceInstaller<TImplementation>
  where TImplementation : class, TInterface
  where TInterface : class
  {
    public override void Register()
    {
      var service = CreateService();
      this.RegisterAsSceneService<TInterface>(service);
      OnInit();
    }
  }
}