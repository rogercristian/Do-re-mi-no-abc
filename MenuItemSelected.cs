using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuItemSelected : MonoBehaviour
{
    /*
     Este script controla o menu responsivo para as Fases principais
     */
    WorldController wc;
    public int option;

    public string sceneName = null;

   // public string MenuMainLevel = null;
    // public LevelManager leveldata;
    //public Player player;


    // public string levelName;
    // Start is called before the first frame update
    void Start()
    {

        wc = FindObjectOfType<WorldController>();
    }

    public void OptionLevelSelected()
    {
        GameManager.PlayButtonClick();
        wc.levelSelected = option;           
    }

    public void LoadThisScene()
    {        
        GameManager.PlayButtonClick();
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    
    IEnumerator LoadAsynchronously(string thisScene)
    {
        thisScene = sceneName;
        AsyncOperation operation = SceneManager.LoadSceneAsync(thisScene);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress * .9f);
            yield return null;
        }

    }

    public void DeletSave()
    {
        PlayerPrefs.DeleteAll();
        return;
    }

}
