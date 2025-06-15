using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CompanyNameLoaderService
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(sceneName, mode);
        }

        public void LoadSceneAsync(string sceneName, float minimumLoadTime = 0f, Action onSceneLoaded = null)
        {
            StartCoroutine(LoadSceneAsyncCoroutine(sceneName, minimumLoadTime, onSceneLoaded));
        }

        public IEnumerator LoadSceneAsyncCoroutine(string sceneName, float minimumLoadTime, Action onSceneLoaded)
        {
            float startTime = Time.time;
            
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            asyncOperation.allowSceneActivation = false;

            // Wait until the scene is fully loaded
            while (asyncOperation.progress < 0.9f)
            {
                yield return null;
            }

            // Calculate remaining time to wait
            float elapsedTime = Time.time - startTime;
            float remainingTime = Mathf.Max(0, minimumLoadTime - elapsedTime);
            
            // Wait remaining time if needed
            if (remainingTime > 0)
            {
                yield return new WaitForSeconds(remainingTime);
            }

            // Activate the scene
            asyncOperation.allowSceneActivation = true;

            // Wait until the scene is fully activated
            while (!asyncOperation.isDone)
            {
                yield return null;
            }
            
            // Call completion callback if provided
            onSceneLoaded?.Invoke();
        }
    }
} 