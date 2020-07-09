using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager LM { get; private set; } = null;

    public static Transform[] WayPointsHolder => LM.wayPointsHolder;
    public static Vector3 LastWayPoint { get; set; } = Vector3.zero;
    public static int Dificulty { get; private set; } = 0;
    public static bool Transitioning { get; private set; } = false;
    public static RectTransform StarIcon => LM.starIcon;
    public static GameObject Letter => LM.letter;
    public static Camera mainCamera { get; private set; } = null;

   

    static int nextPoint = 0;
    static int waypointGroup = 0;
    static WayPoint[][] wayPoints = null;

    [SerializeField] Transform[] wayPointsHolder = null;
    [SerializeField] RectTransform transitionScreen = null;
    [SerializeField] RectTransform starIcon = null;
    [SerializeField] Star starPrefab = null;
    [SerializeField] GameObject letter = null;
    public Image score = null;
    [SerializeField] Image delayedScore = null;
    [SerializeField] int level = 0;
   public string nextLevelName; // Carrega a cena seguinte

    public float scorelevel;
    public string sceneName;
    public bool IsClear = false;
    public bool IsLastLevel = false;

  //  public  int thisLevel=0;
    public static bool isStarted = true;
    
    private void Awake()
    {        
        LM = this;
        isStarted = true;
        sceneName = SceneManager.GetActiveScene().name;
     //   thisLevel = level;
        mainCamera = Camera.main;
     
        wayPoints = new WayPoint[wayPointsHolder.Length][];

        for (int j = 0; j < wayPoints.GetLength(0); j++)
        {
            wayPoints[j] = new WayPoint[wayPointsHolder[j].childCount];

            for (int i = 0; i < wayPointsHolder[j].childCount; i++)
            {
                wayPoints[j][i] = wayPointsHolder[j].GetChild(i).GetComponent<WayPoint>();
                if (i == 0) continue;
                Vector2 dir = wayPoints[j][i].transform.position - wayPoints[j][i - 1].transform.position;
                wayPoints[j][i].transform.rotation = Quaternion.FromToRotation(Vector2.up, dir);
            }
            if (wayPointsHolder[j].childCount > 1) wayPoints[j][0].transform.rotation = Quaternion.FromToRotation(Vector2.up, wayPoints[j][1].transform.position - wayPoints[j][0].transform.position);
        }

        Dificulty = 0;

     if (GameManager.FirstTime == false) {
         
            //LM.delayedScore.fillAmount = LM.score.fillAmount = PlayerPrefs.GetFloat("Score" + level.ToString());
            LM.delayedScore.fillAmount = LM.score.fillAmount = PlayerPrefs.GetFloat(sceneName);
          //  Debug.Log(PlayerPrefs.GetFloat(LevelManager.LM.sceneName, LevelManager.LM.score.fillAmount));
        }
       
     
    }

    private void Start()
    {    
        ResetWayPoints();
        StartCoroutine(TransitionOut());                 
    }
    
    public static void ResetLevel()
    {
       // GameManager.PlayLoseSound();

        Letter.SetActive(true);
        DrawManager.ResetDraw();        
        ResetWayPoints();
    }

    public static void CheckIfWonLevel()
    {
        for (int i = 0; i < wayPoints[waypointGroup].Length; i++)
        {

            if (!wayPoints[waypointGroup][i].Activated)
            {              
                ResetLevel();
               
                return;

            }
        }
       

        if (Vector2.Distance(Player.P.transform.position, wayPoints[waypointGroup][wayPoints[waypointGroup].Length - 1].transform.position) > wayPoints[waypointGroup][wayPoints[waypointGroup].Length - 1].DetectionRadius)
        {

            ResetLevel();
          
            return;
        }
     

        waypointGroup++;
      
        if (waypointGroup >= wayPoints.Length)
        {
            LM.StartCoroutine(LM.WonAnimation());
        }
        else
        {
            WayPointsHolder[waypointGroup].gameObject.SetActive(true);
            nextPoint = 0;
            ActivateNextPoint();
        }
    }

    public static void ActivateNextPoint()
    {
        if (nextPoint < wayPoints[waypointGroup].Length)
        {
            wayPoints[waypointGroup][nextPoint].ActivateDetection();
            nextPoint++;
        }
    }

    static void ResetWayPoints()
    {
        for (int j = 0; j < wayPoints.Length; j++)
        {
            for (int i = 0; i < wayPoints[j].Length; i++)
            {
                wayPoints[j][i].ResetWayPoint();
            }
        }

        for (int k = 1; k < WayPointsHolder.Length; k++)
        {
            WayPointsHolder[k].gameObject.SetActive(false);
        }

        /*  Toca som de derrota ao errar  */
        if (Letter && !Transitioning && isStarted)
        {
            isStarted = false;                                         
        } else if (Letter && !Transitioning && !isStarted)
        {
            GameManager.PlayLoseSound();
        }
        /*  Toca som de derrota ao errar  */

        WayPointsHolder[0].gameObject.SetActive(true);
        nextPoint = 0;
        waypointGroup = 0;
        LastWayPoint = Vector3.zero;
        ActivateNextPoint();
    }

    public void GotoMenu()
    {
        if (Transitioning) return;
        GameManager.FirstTime = false;  // Testa se é a 1a vez que carrego esta Cena            

       PlayerPrefs.SetInt("Level" + level.ToString(), 1); // para habilitar a fase nao concluida      

        PlayerPrefs.SetFloat(sceneName, score.fillAmount); // OK -> para salvar o score na fase nao concluida
        // PlayerPrefs.SetFloat("Score" + level.ToString(), score.fillAmount); // para salvar o score na fase nao concluida

        PlayerPrefs.Save();      

        StartCoroutine(TransitionIn());
    }

    IEnumerator WonAnimation()
    {
        Transitioning = true;

        LM.letter.SetActive(false);

        GameManager.PlayVictorySound();

        yield return new WaitForSeconds(.25f);


        RipplePostProcessor.StartRipple(new Vector3(Screen.width / 2, Screen.height / 2));

        yield return null;

        int nStart = 5;
        float angle = 0;

        for (int i = 0; i < nStart; i++)
        {
            float x = Mathf.Sin(angle * Mathf.Deg2Rad);
            float y = Mathf.Cos(angle * Mathf.Deg2Rad);

            Instantiate(starPrefab, Vector2.zero, Quaternion.identity).SetDir(new Vector2(x, y).normalized, true);

            angle += 360 / nStart;
        }

        yield return new WaitForSeconds(1f);

        LM.letter.SetActive(true);

        WayPointsHolder[0].gameObject.SetActive(true);
        ResetLevel();

        Transitioning = false;

        yield return new WaitForSeconds(1f);

        if (LM.delayedScore.fillAmount < 1f)
        {
            LM.StartCoroutine(DelayedScore());
        }
    }
    
    IEnumerator TransitionOut()
    {
        Transitioning = true;

        float duration = .5f;
        float time = 0;
        while (time <= duration)
        {
            transitionScreen.anchorMin = Vector2.up * GameManager.EaseOutBounce(0f, 1f, time / duration);
            transitionScreen.anchorMax = transitionScreen.anchorMin + Vector2.one;
            time += Time.deltaTime;
            yield return null;
        }

        transitionScreen.anchorMax = Vector2.one + Vector2.up;
        transitionScreen.anchorMin = Vector2.zero + Vector2.up;

        Transitioning = false;
    }

    IEnumerator TransitionIn()
    {
        Transitioning = true;

        RipplePostProcessor.StartRipple(Input.mousePosition);

        float duration = .25f;
        float time = 0;
        while (time <= duration)
        {
            transitionScreen.anchorMin = Vector2.up - Vector2.up * EaseInOutSine(0f, 1f, time / duration);
            transitionScreen.anchorMax = transitionScreen.anchorMin + Vector2.one;
            time += Time.deltaTime;
            yield return null;
        }

        transitionScreen.anchorMax = Vector2.one;
        transitionScreen.anchorMin = Vector2.zero;

        Transitioning = false;

       SceneManager.LoadScene("MainMenu");
    }

    public float EaseInOutSine(float start, float end, float value)
    {
        end -= start;
        return -end * 0.5f * (Mathf.Cos(Mathf.PI * value) - 1) + start;
    }

    static IEnumerator starIconRoutine = null;

    public static void BlinkStarIcon()
    {
        if (starIconRoutine != null) LM.StopCoroutine(starIconRoutine);
        starIconRoutine = BlinkingStarIcon();
        LM.StartCoroutine(starIconRoutine);
    }

    static IEnumerator BlinkingStarIcon()
    {
        RipplePostProcessor.StartRipple(mainCamera.ScreenPointToRay(StarIcon.transform.position).GetPoint(0));

        StarIcon.localScale = Vector2.one * 2f;

        float time = .25f;
        while (time > 0)
        {
            StarIcon.localScale = Vector2.Lerp(StarIcon.localScale, Vector2.one, 10 * Time.deltaTime);
            time -= Time.deltaTime;
            yield return null;
        }

        starIconRoutine = null;
    }

    public static void AddToScore()
    {
        if (LM.score.fillAmount < 1f)
        {
            LM.score.fillAmount = LM.score.fillAmount += GameManager.dificulty * GameManager.dificulty; // dificuldad do score
        }

        LM.scorelevel = LM.score.fillAmount;   
           
    }

    static IEnumerator DelayedScore()
    {
        float start = LM.delayedScore.fillAmount;

        float time = 0;
        float duration = .28f;
        while (time <= duration)
        {
            LM.delayedScore.fillAmount = Mathf.Lerp(start, LM.score.fillAmount, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        LM.delayedScore.fillAmount = LM.score.fillAmount;
        LM.scorelevel = LM.score.fillAmount;       

        if (LM.score.fillAmount >= 1f)
        {
            LM.scorelevel = LM.score.fillAmount;
          
            LM.IsClear=true;

            PlayerPrefs.SetInt("Level" + LM.level.ToString(), 1);         
            /*=========================================================================================================
             ============================ Aqui salva o score maximo====================================================*/

           // PlayerPrefs.SetFloat(LM.sceneName, LM.score.fillAmount); // OK-> para salvar o score na fase nao concluida

            /*=========================================================================================================
            ============================ Aqui salva o score maximo====================================================*/
            PlayerPrefs.Save();

          //  Debug.Log(PlayerPrefs.GetInt(SubMenuManager._mainLevel));

            SceneManager.LoadScene(LM.nextLevelName);
            
        }
    }
    //public void SavePlayer()
    //{

    //    SaveSystem.SavePlayer(this);
        
    //}

    //public void LoadPlayer()
    //{
    //    PlayerData data = SaveSystem.LoadPlayer();

    //    scorelevel = data.score;
    //    sceneName = data.sceneName;
    //    IsClear = data.IsClear;
    //    //  player.sceneIndex = data.sceneIndex;
    //}
    //public void CheckLevelClear()
    //{
    //    string thisScene = sceneName;
    //    if (LM.score.fillAmount >= 1f && thisScene != null)
    //    {
    //        IsClear = true;
    //        //GameManager.FirstTime = false;
    //    }
    //    else { IsClear = false; }
    //    //if (thisScene == sceneName && IsClear && !IsLastLevel)
    //    //{
    //    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //    //}
        
    //}

}
