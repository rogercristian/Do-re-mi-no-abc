using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldController : MonoBehaviour
{

    //Criar gerenciador de fases que salve o progresso e permitar criar níveis infinitamente
    public string[] newLevels;                  // Cria Array com nome dos nivies principais
    public int levelSelected = 0;                   // Indica qual indice do Array acima será selecionado no menu
    private  string newLevelName = null;   // Noma da novo nivel principal que será salvo no PlayerPrefs    
    public GameManager gm;
    public GameObject[] subLevelPlanes;
     
    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    switch (levelSelected)
    //    {
    //        case 0:
    //            {

    //                //  Debug.Log("Nada foi selecionado");
    //                break;
    //            }
    //        case 1:
    //            {
    //                newLevelName = newLevels[levelSelected];

    //                subLevelPlanes[0].SetActive(true);
    //                PlayerPrefs.SetString(SubMenuManager._mainLevel, SubMenuManager._mainLevel);
                   
    //                Debug.Log(PlayerPrefs.GetString(SubMenuManager._mainLevel));

    //                break;

    //            }
    //        case 2:
    //            {
    //                newLevelName = newLevels[levelSelected];

    //                subLevelPlanes[1].SetActive(true);
    //                PlayerPrefs.SetString(SubMenuManager._mainLevel, SubMenuManager._mainLevel);
                    
    //                Debug.Log(PlayerPrefs.GetString(SubMenuManager._mainLevel));

    //                break;
    //            }
    //        case 3:
    //            {
    //                newLevelName = newLevels[levelSelected];

    //                subLevelPlanes[2].SetActive(true);

    //                PlayerPrefs.SetString(SubMenuManager._mainLevel, SubMenuManager._mainLevel);
                   
    //                Debug.Log(PlayerPrefs.GetString(SubMenuManager._mainLevel));


    //                break;
    //            }
    //        case 4:
    //            {
    //                newLevelName = newLevels[levelSelected];

    //                subLevelPlanes[3].SetActive(true);
    //                PlayerPrefs.SetString(SubMenuManager._mainLevel, SubMenuManager._mainLevel);
                    
    //                Debug.Log(PlayerPrefs.GetString(SubMenuManager._mainLevel));

    //                break;
    //            }

    //        default:
    //            Debug.LogError(levelSelected + "nao carregado");

    //            break;
    //    }
    //}
    public void OptionLevelSelected()
    {

        int option = levelSelected;
       

    }

    public void ButtonClicked()
    {
        switch (levelSelected)
        {
            case 0:
                {

                    //  Debug.Log("Nada foi selecionado");
                    break;
                }
            case 1:
                {
                    
                    newLevelName = newLevels[levelSelected];

                    subLevelPlanes[0].SetActive(true);

                    string mainLevel = SubMenuManager._mainLevel;

                    Debug.Log(PlayerPrefs.GetString(SubMenuManager._mainLevel));

                    break;

                }
            case 2:
                {
                    newLevelName = newLevels[levelSelected];

                    subLevelPlanes[1].SetActive(true);

                    string mainLevel = SubMenuManager._mainLevel;

                   

                    

                    break;
                }
            case 3:
                {
                   
                    newLevelName = newLevels[levelSelected];
                    string mainLevel = SubMenuManager._mainLevel;

                    subLevelPlanes[2].SetActive(true);


                    Debug.Log(PlayerPrefs.GetString(SubMenuManager._mainLevel));




                    break;
                }
            case 4:
                {
                    newLevelName = newLevels[levelSelected];
                    string mainLevel = SubMenuManager._mainLevel;

                    subLevelPlanes[3].SetActive(true);                  
                   

                   

                    break;
                }

            default:
                Debug.LogError(levelSelected + "nao carregado");

                break;
        }

        
    }
    public void CloseSubLevelsPanel()
    {
        for(int i  = 0; i < subLevelPlanes.Length; i++)
        {
            levelSelected = 0;
            subLevelPlanes[i].SetActive(false);
        }
    }
    
}
