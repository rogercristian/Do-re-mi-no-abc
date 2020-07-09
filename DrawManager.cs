using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    static DrawManager DM { get; set; } = null;

    public static bool Drawing { get; private set; } = false;

    static TrailRenderer currentTrail = null;
    static SpriteRenderer currentDot = null;
    static List<TrailRenderer> trailList = new List<TrailRenderer>();
    static List<SpriteRenderer> dots = new List<SpriteRenderer>();
    static int order = 0;

    [SerializeField] TrailRenderer trailPrefab = null;
    [SerializeField] SpriteRenderer dotPrefab = null;

    private void Awake()
    {
        DM = this;
    }

    public static void BeginDraw(Vector3 startPosition)
    {
        if (LevelManager.Transitioning) return;
        //LevelManager.WayPointsHolder.position = startPosition;
        currentTrail = Instantiate(DM.trailPrefab, Player.P.transform);
        SpriteRenderer circle = Instantiate(DM.dotPrefab, Player.P.transform.position, Quaternion.identity);
        currentDot = Instantiate(DM.dotPrefab, Player.P.transform);
        currentDot.sortingOrder = circle.sortingOrder = currentTrail.sortingOrder = order;
        currentDot.color = circle.color = currentTrail.endColor = currentTrail.startColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        currentDot.transform.localScale = circle.transform.localScale = Vector3.one * currentTrail.startWidth;
        dots.Add(circle);
        order++;
        Drawing = true;
    }

    public static void EndDraw()
    {
        currentDot.transform.parent = currentTrail.transform.parent = null;
        trailList.Add(currentTrail);
        dots.Add(currentDot);
        currentDot = null;
        currentTrail = null;
        Drawing = false;
    }

    public static void ResetDraw()
    {
        
        for (int i = 0; i < trailList.Count; i++) if(trailList[i] != null) Destroy(trailList[i].gameObject);

        for (int i = 0; i < dots.Count; i++) if (dots[i] != null) Destroy(dots[i].gameObject);

        trailList.Clear();
        dots.Clear();
        order = 0;
    }



}
