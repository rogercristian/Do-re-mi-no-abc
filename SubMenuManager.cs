using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore;
using UnityEngine.SocialPlatforms;

public class SubMenuManager : MonoBehaviour
{
    static bool trasition = false;
    public static int levelToUnlock = 0;

    public static int maxSubLevels = 0;
    public int _maxSubLevels = 0;
 //   [SerializeField] RectTransform playButton = null;
    [SerializeField] Button[] levelButtons = null;
   // RectTransform playButtonHolder = null;
    RectTransform levelButtonsHolder = null;
    [SerializeField] Star starPrefab = null;
    public static string _mainLevel = null;

   // public string mainLevel = null;

    Camera mainCamera;
    // Start is called before the first frame update
    private void Awake()
    {
       // playButtonHolder = playButton.parent.GetComponent<RectTransform>();
        levelButtonsHolder = levelButtons[0].transform.parent.GetComponent<RectTransform>();
    }

    private void Start()
    {
        maxSubLevels = _maxSubLevels;
       // PlayerPrefs.DeleteKey(_mainLevel);
       // _mainLevel = mainLevel;
       
        mainCamera = Camera.main;
       
        if (GameManager.FirstTime) 
        {                
            for (int i = 0; i < levelButtons.Length; i++)
            {
                 levelButtons[i].interactable = PlayerPrefs.GetInt("Level" + i.ToString()) == 1;                  
              
            }
           
        }
        else
        {
            // levelButtonsHolder.gameObject.SetActive(true);           
            for (int i = 0; i < levelButtons.Length; i++)
            {
                levelButtons[i].interactable = PlayerPrefs.GetInt("Level" + i.ToString()) == 1;             
            }
           
            CheckToUnlockLevel();
        }
    }

    void CheckToUnlockLevel()
    {
       
        if (levelToUnlock < maxSubLevels)
        {
            if (levelToUnlock != 0 && !levelButtons[levelToUnlock].interactable)
            {
                StartCoroutine(UnlockLevel());
            }
        }
       
        else
        {
            //int nStart = 20;
            //float angle = 0;

            //for (int i = 0; i < nStart; i++)
            //{
            //    float x = Mathf.Sin(angle * Mathf.Deg2Rad);
            //    float y = Mathf.Cos(angle * Mathf.Deg2Rad);

            //    Instantiate(starPrefab, mainCamera.ScreenPointToRay(levelButtons[2].transform.position).GetPoint(0), Quaternion.identity).SetDir(new Vector2(x, y).normalized, false);

            //    angle += 360 / nStart;
            //}
           // victory.gameObject.SetActive(true);

            levelToUnlock = 0;
        }

    }

    IEnumerator UnlockLevel()
    {
        yield return new WaitForSeconds(.5f);

        RipplePostProcessor.StartRipple(levelButtons[levelToUnlock].transform.position);

        yield return null;

        //int nStart = 20;
        //float angle = 0;

        //for (int i = 0; i < nStart; i++)
        //{
        //    float x = Mathf.Sin(angle * Mathf.Deg2Rad);
        //    float y = Mathf.Cos(angle * Mathf.Deg2Rad);

        //    Instantiate(starPrefab, mainCamera.ScreenPointToRay(levelButtons[levelToUnlock].transform.position).GetPoint(0), Quaternion.identity).SetDir(new Vector2(x, y).normalized, false);

        //    angle += 360 / nStart;
        //}

        levelButtons[levelToUnlock].interactable = true;      

        PlayerPrefs.SetInt("Level" + levelToUnlock.ToString(), 1);       
        PlayerPrefs.Save();       
    }

    IEnumerator Victory()
    {
        yield return null;
    }

    public void GoToLevelSelection()
    {
        if (trasition) return;
        StartCoroutine(DelayToLoad());
        levelButtonsHolder.gameObject.SetActive(true);
      //  for (int i = 0; i < levelButtons.Length; i++) StartCoroutine(ButtonAnimation(levelButtons[i].GetComponent<RectTransform>(), 1f + Random.Range(0f, .5f)));
    }

    //IEnumerator ButtonAnimation(RectTransform button, float delay, bool show = true)
    //{
    //    float final = button.anchoredPosition.y;
    //    if (show) button.anchoredPosition = Vector2.zero;

    //    if (delay > 0) yield return new WaitForSeconds(delay);

    //    float duration = show ? 1f : .5f;
    //    float time = 0;
    //    while (time <= duration)
    //    {
    //       // button.anchoredPosition = Vector2.up * final * (show ? GameManager.EaseOutBounce(0f, 1f, time / duration) : GameManager.EaseInBounce(1f, 0f, time / duration));
    //        time += Time.deltaTime;
    //        yield return null;
    //    }
    //}

    IEnumerator DelayToLoad()
    {
        trasition = true;
        if (GameManager.FirstTime) yield return new WaitForSeconds(.5f);

        //float duration = 1f;
        //float time = 0;
        //while (time <= duration)
        //{
        //    playButtonHolder.anchorMin  = Vector2.up * GameManager.EaseOutBounce(0f, 1f, time / duration);
        //    playButtonHolder.anchorMax  =  Vector2.one;
        //    time += Time.deltaTime;
        //    yield return null;
        //}

        //playButtonHolder.anchorMax =  Vector2.one + Vector2.up;
        //playButtonHolder.anchorMin =  Vector2.zero + Vector2.up;

        trasition = false;
    }

    //public void GotoLevel(int level)
    //{
    //   // if (trasition) return;
    //    StartCoroutine(TransitionToLevel(level));
    //    for (int i = 0; i < levelButtons.Length; i++) StartCoroutine(ButtonAnimation(levelButtons[i].GetComponent<RectTransform>(), Random.Range(0f, .5f), false));
    //}

    //IEnumerator TransitionToLevel(int level)
    //{
    //   // trasition = true;

    //    yield return new WaitForSeconds(1f);

    //  //  trasition = false;
    //   // SceneManager.LoadScene(level);
    //}
}
