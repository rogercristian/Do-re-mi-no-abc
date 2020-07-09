using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressLoading : MonoBehaviour
{
    public Text loadText;
 
    public void BeginGame(string sceneName)
    {       
        StartCoroutine(LoadAsynchronously(sceneName));
    }
    IEnumerator LoadAsynchronously(string sceneName)
    {     
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadText.text = progress * 100 + "%" + "/ 100%";
           
          //  Debug.Log(operation.progress);
            yield return null;
        }
    }
}
