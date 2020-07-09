using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{

    Vector3 dir = Vector2.zero;
    TrailRenderer trail;
    SpriteRenderer spr;
    bool onLevel;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        trail = transform.GetChild(0).GetComponent<TrailRenderer>();
    }

    public void SetDir(Vector3 dir, bool onLevel)
    {
        this.dir = dir;
        this.onLevel = onLevel;
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        Vector2 end = transform.position + dir * 2f;
        Vector2 start = transform.position;
        float duration = Random.Range(.4f, .6f);
        float time = 0;

        while (time <= duration)
        {
            transform.position = Vector2.Lerp(start, end, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        if (onLevel)
        {
            start = transform.position;
            end = LevelManager.mainCamera.ScreenPointToRay(LevelManager.StarIcon.transform.position).GetPoint(0);

            duration = Random.Range(.25f, .5f);
            time = 0;
            while (time <= duration)
            {
                transform.position = Vector2.Lerp(start, end, time / duration);
                transform.localScale = Vector2.Lerp(Vector2.one, Vector2.one * .2f, time / duration);
                trail.startWidth = transform.localScale.x / 2f;
                time += Time.deltaTime;
                yield return null;
            }

            LevelManager.BlinkStarIcon();
            LevelManager.AddToScore();
        }
        else
        {
            start = transform.position;
            Color color = spr.color;
            end = transform.position + dir * 2f;
            duration = Random.Range(.4f, .6f);
            time = 0;

            while (time <= duration)
            {
                transform.position = Vector2.Lerp(start, end, time / duration);
                color.a = Mathf.Lerp(1, 0, time / duration);
                spr.color = trail.startColor = color;
                time += Time.deltaTime;
                yield return null;
            }
        }

        Destroy(gameObject);

    }

}
