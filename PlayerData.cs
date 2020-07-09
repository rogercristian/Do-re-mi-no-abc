using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerData
{
    public float score;
    public string sceneName;
    public int sceneIndex;
    public bool IsClear;

    public PlayerData (LevelManager leveldata)
    {
        score = leveldata.scorelevel;
        sceneName = leveldata.sceneName;
      //  leveldata.CheckLevelClear();
       // sceneIndex = player.sceneIndex;
    }
}
