using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    [SerializeField] LineRenderer lineEffect;
    [SerializeField] int steps;
    [SerializeField] float radius;
    [SerializeField] float width;
    Vector2 offset;
    float startRadius;
    float startWidth;

    [SerializeField] float TargetRadius;
    [SerializeField] float animeTime;

    bool startDrawing;
    // Start is called before the first frame update
    void Start()
    {
        width = lineEffect.startWidth;
        startWidth = width;
        startRadius = radius;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            startDrawing = true;
        }

        if (startDrawing)
            DrawCircle();

      
    }

    public void SetDrawinfToTrue(Vector2 offset)
    {
        startDrawing = true;
        this.offset = offset;
    }

    public void ChangeColor(Color color)
    {
        lineEffect.startColor = color;
        lineEffect.endColor = color;
    }

    bool once;
    public void DrawCircle()
    {
        lineEffect.positionCount = steps;

        float angleSteps = (Mathf.PI * 2) / steps;
        float angle = angleSteps;

        for (int i = 0; i < steps; i++)
        {
            float x = Mathf.Cos(angle);
            float y = Mathf.Sin(angle);

            Vector2 pos = new Vector2(x, y) * radius + offset;

            lineEffect.SetPosition(i, pos);

            angle += angleSteps;
        }
        if(once == false)
        {
            startDrawing = true;
            ChangeWidhAndRadius();
            once = true;
        }

    }

    private void ResetEffect()
    {
        startDrawing = false;
        once = false;
        radius = startRadius;
        width = startWidth;
    }

    void ChangeWidth(float w)
    {
        lineEffect.startWidth = w;
        lineEffect.endWidth = w;
    }
    void ChangeWidhAndRadius()
    {
        DOTween.To(() => width, x => width = x, 0, animeTime).onUpdate = () => ChangeWidth(width);
        DOTween.To(() => radius, x => radius = x, TargetRadius, animeTime).OnComplete(() => ResetEffect());
    }
}
