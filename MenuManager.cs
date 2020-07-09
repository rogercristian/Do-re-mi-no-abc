using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    static bool trasition = false;
    public static int levelToUnlock = 0;

    [SerializeField] RectTransform victory;
    [SerializeField] RectTransform mainMenu = null;
   // [SerializeField] RectTransform playButton = null;
    //[SerializeField] Button[] levelButtons = null;
    [SerializeField] Star starPrefab = null;
  //  RectTransform levelButtonsHolder = null;
  //  RectTransform playButtonHolder = null;
    Camera mainCamera;

   
   
    private void Awake()
    {
     //   playButtonHolder = playButton.parent.GetComponent<RectTransform>();
        //levelButtonsHolder = levelButtons[0].transform.parent.GetComponent<RectTransform>();

    }

    private void Start()
    {
        mainCamera = Camera.main;
     
        //levelButtonsHolder.gameObject.SetActive(false);

        //if (GameManager.FirstTime) 
        //{ 
        //    StartCoroutine(ButtonAnimation(playButton, 0)); 
        //}
        //else
        //{
        //    mainMenu.gameObject.SetActive(false);
        //    playButtonHolder.gameObject.SetActive(false);
        //    levelButtonsHolder.gameObject.SetActive(true);
        //    for (int i = 0; i < levelButtons.Length; i++)
        //    {
        //        levelButtons[i].interactable = (PlayerPrefs.GetInt("Level" + i.ToString()) == 1);

        //        StartCoroutine(ButtonAnimation(levelButtons[i].GetComponent<RectTransform>(), Random.Range(0f, .25f)));
        //    }
        //    //CheckToUnlockLevel();
        //}
    }

    //void CheckToUnlockLevel()
    //{ 

    //    if(levelToUnlock < 5)
    //    {
    //        if (levelToUnlock != 0 && !levelButtons[levelToUnlock].interactable) StartCoroutine(UnlockLevel());
    //    }
    //    else
    //    {
    //        int nStart = 20;
    //        float angle = 0;

    //        for (int i = 0; i < nStart; i++)
    //        {
    //            float x = Mathf.Sin(angle * Mathf.Deg2Rad);
    //            float y = Mathf.Cos(angle * Mathf.Deg2Rad);

    //            Instantiate(starPrefab, mainCamera.ScreenPointToRay(levelButtons[2].transform.position).GetPoint(0), Quaternion.identity).SetDir(new Vector2(x, y).normalized, false);

    //            angle += 360 / nStart;
    //        }
    //        victory.gameObject.SetActive(true);

    //        levelToUnlock = 0;
    //    }
       
    //}

    //IEnumerator UnlockLevel()
    //{
    //    yield return new WaitForSeconds(1f);

    //    RipplePostProcessor.StartRipple(levelButtons[levelToUnlock].transform.position);

    //    yield return null;

    //    int nStart = 20;
    //    float angle = 0;

    //    for (int i = 0; i < nStart; i++)
    //    {
    //        float x = Mathf.Sin(angle * Mathf.Deg2Rad);
    //        float y = Mathf.Cos(angle * Mathf.Deg2Rad);

    //        Instantiate(starPrefab, mainCamera.ScreenPointToRay(levelButtons[levelToUnlock].transform.position).GetPoint(0), Quaternion.identity).SetDir(new Vector2(x, y).normalized, false);

    //        angle += 360 / nStart;
    //    }

    //    levelButtons[levelToUnlock].interactable = true;

    //    PlayerPrefs.SetInt("Level" + levelToUnlock.ToString(), 1);
    //    PlayerPrefs.Save();
    //}

    IEnumerator Victory()
    {


        yield return null;

    }

    //public void GoToLevelSelection()
    //{
    //    if (trasition) return;
    //    StartCoroutine(DelayToLoad());
    //    levelButtonsHolder.gameObject.SetActive(true);
    //    for (int i = 0; i < levelButtons.Length; i++)
    //    {
    //        StartCoroutine(ButtonAnimation(levelButtons[i].GetComponent<RectTransform>(), 1f + Random.Range(0f, .5f)));
    //    }
    //}

    IEnumerator ButtonAnimation(RectTransform button, float delay, bool show = true)
    {
        float final = button.anchoredPosition.y;
        if (show) button.anchoredPosition = Vector2.zero;

        if (delay > 0) yield return new WaitForSeconds(delay);

        float duration = show ? 1f : .5f;
        float time = 0;
        while (time <= duration)
        {
            button.anchoredPosition = Vector2.up * final * (show ? GameManager.EaseOutBounce(0f, 1f, time / duration) : GameManager.EaseInBounce(1f, 0f, time / duration));
            time += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator DelayToLoad()
    {
        trasition = true;
        if (GameManager.FirstTime) yield return new WaitForSeconds(.5f);

        //float duration = 1f;
        //float time = 0;
        //while (time <= duration)
        //{
        //    playButtonHolder.anchorMin = mainMenu.anchorMin = Vector2.up * GameManager.EaseOutBounce(0f, 1f, time / duration);
        //    playButtonHolder.anchorMax = mainMenu.anchorMax = mainMenu.anchorMin + Vector2.one;
        //    time += Time.deltaTime;
        //    yield return null;
        //}

        //playButtonHolder.anchorMax = mainMenu.anchorMax = Vector2.one + Vector2.up;
        //playButtonHolder.anchorMin = mainMenu.anchorMin = Vector2.zero + Vector2.up;

        trasition = false;
    }

    //public void GotoLevel(int level)
    //{
    //    //if (PlayerPrefs.GetInt(wc.newLevelName, gotoLevelNow) <= 0) {
    //        gotoLevelNow = level;
    //    //}
       

    //    if (trasition) return;
    //    StartCoroutine(TransitionToLevel(level));
       
    //    // for (int i = 0; i < levelButtons.Length; i++) StartCoroutine(ButtonAnimation(levelButtons[i].GetComponent<RectTransform>(), Random.Range(0f, .5f), false));
    //}

    //IEnumerator TransitionToLevel(int level)
    //{
    //    trasition = true;

    //    yield return new WaitForSeconds(1f);

    //    trasition = false;
    // //   level = PlayerPrefs.GetInt(wc.newLevelName, MenuManager.gotoLevelNow);
    //  //   SceneManager.LoadScene(level);
    //   // wc.AllSceneManager();
    //}

}
