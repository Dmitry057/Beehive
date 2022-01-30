using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [Header("SceneName")]
    [SerializeField] private string sceneName;
    [Header("Interface")]
    [SerializeField] private Image ProgressBar;
    [SerializeField] private Text ProgressText;

    private void Start()
    {
        StartCoroutine(AsyncLoad());
    }
    private IEnumerator AsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            ProgressBar.fillAmount = operation.progress;
            ProgressText.text = string.Format("{0:0}%", progress);
            yield return null;
        }
    }
}
