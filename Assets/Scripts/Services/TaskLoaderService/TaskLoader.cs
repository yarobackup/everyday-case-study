using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CompanyName.TaskLoaderService
{
  public class TaskLoader : MonoBehaviour, ITaskLoader
  {
    public event Action<float> OnProgressChanged;
    public event Action OnLoadingCompleted;

    private List<(ITask task, int progressDelta)> _tasks = new List<(ITask task, int progressDelta)>();
    private List<(ITask task, int progressDelta)> _processingTasks = new List<(ITask task, int progressDelta)>();
    private float _progress;
    private float _totalProgress;
    private List<Action<float>> _progressHandlers = new List<Action<float>>();

    public void RegisterTask(ITask task, int progressDelta)
    {
      _tasks.Add((task, progressDelta));
    }

    public void StartLoading()
    {
      _processingTasks.AddRange(_tasks);
      _tasks.Clear();
      _progress = 0f;
      _totalProgress = _processingTasks.Sum(t => t.progressDelta);
      SetProgress(0);
      StartCoroutine(ProcessTasks());
    }

    private IEnumerator ProcessTasks()
    {
      while (_processingTasks.Count > 0)
      {
        var task = _processingTasks[0];
        _processingTasks.RemoveAt(0);
        var progress = new LoadingProgress();
        var progressHandler = (Action<float>)((p) => SetProgress(task.progressDelta * p));
        _progressHandlers.Add(progressHandler);
        progress.Current += progressHandler;
        yield return task.task.Execute(progress);
        progress.Current -= progressHandler;
        _progressHandlers.Remove(progressHandler);
        _progress += task.progressDelta;
      }
      OnLoadingCompleted?.Invoke();
    }

    private void SetProgress(float delta)
    {
      OnProgressChanged?.Invoke((_progress + delta) / _totalProgress);
    }

    private void OnDestroy()
    {
      // Clean up any remaining progress handlers
      foreach (var handler in _progressHandlers)
      {
        if (handler != null)
        {
          OnProgressChanged -= handler;
        }
      }
      _progressHandlers.Clear();
    }
  }

  public class LoadingProgress : IProgress<float>
  {
    public event Action<float> Current;

    const float ratio = 1f;

    public void Report(float value)
    {
      Current?.Invoke(value / ratio);
    }
  }
}
