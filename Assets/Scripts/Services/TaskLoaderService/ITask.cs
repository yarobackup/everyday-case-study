using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CompanyName.TaskLoaderService
{
  public interface ITask
  {
    IEnumerator Execute(IProgress<float> progress);
  }
}

namespace CompanyName.TaskLoaderService
{
  public class DelayTask : ITask
  {
    private float _delay;

    public DelayTask(float v)
    {
      _delay = v;
    }

    public IEnumerator Execute(IProgress<float> progress)
    {
      var wait = new WaitForSeconds(1f);
      for (int i = 0; i < _delay; i++)
      {
        progress.Report(i * 1f / _delay);
        yield return wait;
      }
    }
  }
}
namespace CompanyName.TaskLoaderService
{
  public class LoadSceneTask : ITask
  {
    private string _sceneName;
    public LoadSceneTask(string v)
    {
      _sceneName = v;
    }

    public IEnumerator Execute(IProgress<float> progress)
    {
      var delay = new WaitForSeconds(0.1f);
      var op = SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
      while (!op.isDone)
      {
        progress.Report(op.progress);
        yield return delay;
      }

      SceneManager.SetActiveScene(SceneManager.GetSceneByName(_sceneName));
    }
  }
}

namespace CompanyName.TaskLoaderService
{
  public class UnloadSceneTask : ITask
  {

    private string _sceneName;


    public UnloadSceneTask(string v)
    {
      _sceneName = v;
    }

    public IEnumerator Execute(IProgress<float> progress)
    {
      progress.Report(0f);
      yield return SceneManager.UnloadSceneAsync(_sceneName);
      progress.Report(1f);
    }
  }
}
