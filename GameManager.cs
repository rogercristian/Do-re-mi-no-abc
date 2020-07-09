using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour
{
    public static GameManager GM { get; private set; } = null;
    public static bool FirstTime { get; set; } = true;
    public static AudioSource soundEffect;
    public static AudioClip victorySoundEffect;
    public static AudioClip loserSoundEffect;
    public static AudioClip buttonClickEffect;

    [Range(0.0f, 1.0f)]
    public float gameDificulty;

    public static float dificulty;

    public  AudioClip victoryEffect;
    public  AudioClip loserEffect;
    public AudioClip buttonClick;

    private void Awake()
    {    
      
        if (GM == null)
        {
            GM = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        dificulty = gameDificulty;
        soundEffect = GetComponent<AudioSource>();
        victorySoundEffect = victoryEffect;
        loserSoundEffect = loserEffect;
        buttonClickEffect = buttonClick;

        PlayerPrefs.SetInt("Level0", 1);
        PlayerPrefs.SetInt("Level1", 0);
        PlayerPrefs.SetInt("Level2", 0);
        PlayerPrefs.SetInt("Level3", 0);
        PlayerPrefs.SetInt("Level4", 0);
        PlayerPrefs.SetInt("Level5", 0);
        PlayerPrefs.SetInt("Level6", 0);
        PlayerPrefs.SetInt("Level7", 0);
        PlayerPrefs.SetInt("Level8", 0);
        PlayerPrefs.SetInt("Level9", 0);

        //PlayerPrefs.SetFloat("Score0", 0);
        //PlayerPrefs.SetFloat("Score1", 0);
        //PlayerPrefs.SetFloat("Score2", 0);
        //PlayerPrefs.SetFloat("Score3", 0);
        //PlayerPrefs.SetFloat("Score4", 0);     

        PlayerPrefs.Save();  

        DontDestroyOnLoad(gameObject);

       // PlayerPrefs.SetString(SubMenuManager._mainLevel, SubMenuManager._mainLevel);
        PlayerPrefs.SetFloat(LevelManager.LM.sceneName, LevelManager.LM.score.fillAmount);//Salva progresso de qualquer Cena
        PlayerPrefs.Save();
    }

    public static void PlayVictorySound()
    {
        soundEffect.clip = victorySoundEffect;
        soundEffect.Play();
    }
    public static void PlayLoseSound()
    {
        soundEffect.clip = loserSoundEffect;
        soundEffect.Play();
    }

    public static void PlayButtonClick()
    {
        soundEffect.clip = buttonClickEffect;
        soundEffect.Play();
    }

    public static float EaseOutBounce(float start, float end, float value)
    {
        value /= 1f;
        end -= start;
        if (value < (1 / 2.75f))
        {
            return end * (7.5625f * value * value) + start;
        }
        else if (value < (2 / 2.75f))
        {
            value -= (1.5f / 2.75f);
            return end * (7.5625f * (value) * value + .75f) + start;
        }
        else if (value < (2.5 / 2.75))
        {
            value -= (2.25f / 2.75f);
            return end * (7.5625f * (value) * value + .9375f) + start;
        }
        else
        {
            value -= (2.625f / 2.75f);
            return end * (7.5625f * (value) * value + .984375f) + start;
        }
    }

    public static float EaseInBounce(float start, float end, float value)
    {
        end -= start;
        float d = 1f;
        return end - EaseOutBounce(0, end, d - value) + start;
    }


}
