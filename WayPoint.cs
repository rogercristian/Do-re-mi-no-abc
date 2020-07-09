using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{

    public float DetectionRadius => detectionRadius;
    [SerializeField] float detectionRadius = 1.5f;

    public bool Activated { get; private set; } = false;

    Color color = Color.white;
    SpriteRenderer spr;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        ResetWayPoint();
    }

    public void ActivateDetection()
    {
        spr.enabled = true;
        color = Color.yellow;
        lastDist = float.MaxValue;
        lastPoint = Vector3.zero;
        detectingPlayer = true;
        Activated = false;
    }

    public void ResetWayPoint()
    {
        spr.enabled = false;
        color = Color.red;
        Activated = false;
        detectingPlayer = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, .1f);
        if (!detectingPlayer) return;
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void Update()
    {
        if (detectingPlayer) CheckPlayerDistance();
    }

    bool detectingPlayer = false;
    float lastDist = float.MaxValue;
    Vector3 lastPoint = Vector3.zero;

    void CheckPlayerDistance()
    {
        if (DrawManager.Drawing)
        {
            float dist = Vector2.Distance(Player.P.transform.position, transform.position);
            if (dist <= detectionRadius)
            {
                color = Color.green;
                Activated = true;
                spr.enabled = false;
                detectingPlayer = false;
                LevelManager.LastWayPoint = transform.position;
                LevelManager.ActivateNextPoint();
            }
            else if (dist < lastDist)
            {
                lastDist = dist;
                lastPoint = Player.P.transform.position;
            }
            else if (dist > lastDist && Vector2.Distance(lastPoint, Player.P.transform.position) > detectionRadius / 2 && Vector2.Distance(Player.P.transform.position, LevelManager.LastWayPoint) > detectionRadius)
            {
                detectingPlayer = false;
                DrawManager.EndDraw();
                LevelManager.ResetLevel();
            }

        }
    }

}
