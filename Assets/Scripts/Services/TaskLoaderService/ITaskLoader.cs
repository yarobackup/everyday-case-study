using System;

namespace CompanyName.TaskLoaderService
{
    public interface ITaskLoader
    {
        event Action<float> OnProgressChanged;
        event Action OnLoadingCompleted;
        void RegisterTask(ITask task, int progressDelta);
        void StartLoading();
    }
}
