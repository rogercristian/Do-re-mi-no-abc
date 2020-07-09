using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RipplePostProcessor : MonoBehaviour
{
    static RipplePostProcessor RP = null;
    static Material RippleMaterial => RP.rippleMaterial;


    static private float amount = 0f;

    [SerializeField] Material rippleMaterial = null;
    [SerializeField] float maxAmount = 50f;
    [SerializeField] [Range(0, 1)] float friction = .9f;
    [SerializeField] bool rippleOnClick = false;

    private void Awake()
    {
        RP = this;
    }

    void Update()
    {
        if (rippleOnClick && (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)))
        {
            StartRipple(Input.mousePosition);
        }

        RippleMaterial.SetFloat("_Amount", amount);
        amount *= friction;
    }

    public static void StartRipple(Vector3 pos)
    {
        amount = RP.maxAmount;
        RippleMaterial.SetFloat("_CenterX", pos.x);
        RippleMaterial.SetFloat("_CenterY", pos.y);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, RippleMaterial);
    }
}
