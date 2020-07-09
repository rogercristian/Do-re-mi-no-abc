using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    static public Player P { get; private set; } = null;

    [SerializeField] Camera mainCamera = null;
    Plane plane = new Plane(Vector3.forward, 0);
    public  int sceneIndex;
    private void Awake()
    {
        P = this;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
      //  Debug.Log(sceneIndex + "cena do player");
    }

    private void Update()
    {

        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            SetPosition();
            DrawManager.BeginDraw(transform.position);
        }
        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0))
        {
            SetPosition();
        }
        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
        {
            if (!DrawManager.Drawing) return;
            DrawManager.EndDraw();
            StartCoroutine(CheckIfGameWon());
        }

    }

    void SetPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        plane.Raycast(ray, out float distanceToPlane);
        transform.position = ray.GetPoint(distanceToPlane);
    }

    IEnumerator CheckIfGameWon()
    {
        //LevelManager.Letter.SetActive(false);
        //yield return new WaitForSeconds(.5f);
        LevelManager.CheckIfWonLevel();
        yield return null;
    }

}
