using UnityEngine;

namespace CompanyName.Services.SL
{
  public interface IInitializableService
  {
    void Init();
  }
  public interface IInitializableWithContextService
  {
    void Init(MonoBehaviour context);
  }

  public abstract class ServiceInstallerBase : MonoBehaviour
  {
    [SerializeField] private bool _destroyAfterRegistration = true;

    public abstract void Register();

    public virtual void Init()
    {
      OnInit();
    }

    protected virtual void OnInit()
    {
      if (_destroyAfterRegistration)
      {
        Destroy(this);
      }
    }
  }

  public abstract class GlobalMonoInitializableServiceInstaller<TImplementation> : GlobalMonoServiceInstaller<TImplementation> where TImplementation : Component, IInitializableService
  {
    protected override void OnInit()
    {
      _service.Init();
      base.OnInit();
    }
  }

  public abstract class GlobalMonoServiceInstaller<TImplementation> : ServiceInstallerBase where TImplementation : Component
  {
    [SerializeField] protected TImplementation _service;

    public override void Register()
    {
      _service.RegisterAsGlobalService();
    }

    protected override void OnInit()
    {
      DontDestroyOnLoad(_service.gameObject);
      base.OnInit();
    }
  }

  public abstract class GlobalMonoInitializableServiceInstaller<TInterface, TImplementation> : GlobalMonoServiceInstaller<TInterface, TImplementation>
    where TImplementation : Component, TInterface, IInitializableService
  {
    protected override void OnInit()
    {
      _service.Init();
      base.OnInit();
    }
  }

  public abstract class GlobalMonoServiceInstaller<TInterface, TImplementation> : GlobalMonoServiceInstaller<TImplementation>
    where TImplementation : Component, TInterface
  {
    public override void Register()
    {
      _service.RegisterAsGlobalService<TInterface, TImplementation>();
    }
  }

  public abstract class GlobalInitializableServiceInstaller<TImplementation> : GlobalServiceInstaller<TImplementation> where TImplementation : class, IInitializableService
  {
    protected override void OnInit()
    {
      _service.Init();
      base.OnInit();
    }
  }

  public abstract class GlobalServiceInstaller<TImplementation> : ServiceInstallerBase where TImplementation : class
  {
    protected TImplementation _service;
    public override void Register()
    {
      _service = CreateService();
      DoRegister();
    }

    protected virtual void DoRegister()
    {
      _service.RegisterAsGlobalService();
    }

    protected abstract TImplementation CreateService();
  }

  public abstract class GlobalInitializableServiceInstaller<TInterface, TImplementation> : GlobalServiceInstaller<TInterface, TImplementation>
    where TImplementation : class, TInterface, IInitializableService
  {
    protected override void OnInit()
    {
      _service.Init();
      base.OnInit();
    }
  }

  public abstract class GlobalServiceInstaller<TInterface, TImplementation> : GlobalServiceInstaller<TImplementation>
  where TImplementation : class, TInterface
  {
    protected override void DoRegister()
    {
      _service.RegisterAsGlobalService<TInterface, TImplementation>();
    }
  }
}