using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDraw : MonoBehaviour
{
    [SerializeField] SineWave sineWave;
    [SerializeField] CosWave cosWave;
    [SerializeField] LineRenderer circleLine;
    [SerializeField] LineRenderer radiusLine;
    [SerializeField] GameObject dot;

    [SerializeField] int steps;
    [SerializeField] float radius;

    float time;
    bool rotate;

    Action<float> uiAction;
    Func<float, float> waveFunc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        DrawCircle();

        if (Input.GetKeyDown(KeyCode.K))
            StartCoroutine(IncreaseRaius());

        if (Input.GetKeyDown(KeyCode.R))
        {
            dot.transform.position = Vector3.right;
            dot.transform.DOScale(Vector2.one * 0.1f, 0.25f).OnComplete(() => dot.transform.DOScale(Vector2.one * 0.05f, 0.25f).OnComplete(() => DrawWaveLine()));
        }

        if (Input.GetKeyDown(KeyCode.F))
            DrawCosLine();

        if(Input.GetKeyDown(KeyCode.Z))
        {
            waveFunc = Mathf.Sin;
            uiAction = UIAnimations.instance.SetSineTxt;
            rotate = true;
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            waveFunc = Mathf.Cos;
            uiAction = UIAnimations.instance.SetCosTxt;
            rotate = true;
            time = 0;
        }

        if (rotate)
            RotateRadiusLineOverTime(uiAction, waveFunc);
    }

    public void HideFullCircle()
    {
        StartCoroutine(ReduceRadiusAndDot());
    }

    IEnumerator ReduceRadiusAndDot()
    {
        yield return new WaitForSeconds(1.75f);
        float xPos = radiusLine.GetPosition(1).x;
        dot.transform.DOScale(Vector2.one * 0.08f, 0.25f).OnComplete(() => dot.transform.DOScale(Vector2.zero, 0.25f));
        DOTween.To(() => xPos, x => xPos = x, 0f, 0.5f).onUpdate = (() => radiusLine.SetPosition(1,new Vector2(xPos,0)));
        yield return new WaitForSeconds(0.5f);
        DOTween.To(() => radius, x => radius = x, 0f, 0.75f);
    }  

    void DrawWaveLine()
    {
        StartCoroutine(RotateRadiusLine(sineWave.waveInfo, Mathf.Sin));
        StartCoroutine(sineWave.waveInfo.DrawUnitWave(Mathf.Sin));
    }

    void DrawCosLine()
    {
        StartCoroutine(RotateRadiusLine(cosWave.waveInfo, Mathf.Cos));
        StartCoroutine(cosWave.waveInfo.DrawUnitWave(Mathf.Cos));
    }

    IEnumerator IncreaseRaius()
    {
        UIAnimations.instance.AnimateUnitCircleTxt();

        yield return new WaitForSeconds(2f);

        DOTween.To(() => radius, x => radius = x, 1f, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => StartCoroutine(DrawRadiusLine()));     
    }
    void DrawCircle()
    { 
        circleLine.positionCount = steps;

        float angleSteps = (Mathf.PI * 2) / steps;
        float angle = angleSteps;

        for (int i = 0; i < steps; i++)
        {
            float x = Mathf.Cos(angle);
            float y = Mathf.Sin(angle);

            Vector2 pos = new Vector2(x, y) * radius;

            circleLine.SetPosition(i, pos);

            angle += angleSteps;
        }

    }

    IEnumerator DrawRadiusLine()
    {
        float t = 0;
        float duration = 0.3f;

        while(t < 1)
        {
            t += Time.deltaTime / duration;

            Vector3 pos = Vector3.Lerp(Vector3.zero, Vector3.right, t);

            radiusLine.SetPosition(1, pos);

            yield return null;
        }

        yield return new WaitForSeconds(0.25f);

        UIAnimations.instance.AnimateRadiusTxt();
    }

    bool firstRotate;
    IEnumerator RotateRadiusLine(WaveInfo info,Func<float,float> waveFunc)
    {
        float angleSteps = (Mathf.PI * 2) / info.steps;
        int dividedSteps = info.steps / 4;
        float angle = angleSteps;

        //StartCoroutine(sineWave.DrawSinWave());

        float rotateTime = 0;
        int startIndex = 0;
        int endIndex = dividedSteps;
        while (rotateTime < 8)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                RotateAngle(info, waveFunc, ref angle, angleSteps, UIAnimations.instance.SetSineTxt);
                yield return new WaitForSeconds(info.drawTime);
            }
            rotateTime++;
            startIndex = endIndex;
            endIndex += dividedSteps;
            if (rotateTime == 4 && firstRotate == false)
            {
                yield return new WaitForSeconds(4f);
                firstRotate = true;
            }

            if (rotateTime == 1)
            {
                if (firstRotate == false)
                   yield return new WaitForSeconds(8f);   
                else
                    yield return new WaitForSeconds(3f);

            }
            else if(rotateTime < 4)
            {
                yield return new WaitForSeconds(4f);
            }
            else
                yield return new WaitForSeconds(5f);

        }
        firstRotate = true;
        //StartCoroutine(RotateRadiusLineOverTimeBack());
    }

    bool cosRotation;
    void RotateRadiusLineOverTime(Action<float> updateUI,Func<float,float> waveFunc)
    {
         time += Time.deltaTime * 2;
        float endAngle = (Mathf.PI * 2) * 2;

        if(time > endAngle)
        {
            time = endAngle;
            rotate = false;
        }
        MoveDotAndLine(time);
        float angle = (time * Mathf.Rad2Deg) + 720;
        if (cosRotation == false)
           UIAnimations.instance.SetAngleTxt(angle);
        else
            UIAnimations.instance.SetAngleRadTxt(time + (4*Mathf.PI));

        if (time >= endAngle)
        {
            cosRotation = true;
        }

        updateUI(waveFunc(time));
    }

    void RotateAngle(WaveInfo info, Func<float, float> waveFunc, ref float angle,float angleSteps,Action<float> updateUI)
    {
        if (cosRotation == false)
            UIAnimations.instance.SetAngleTxt(angle * Mathf.Rad2Deg);
        else
            UIAnimations.instance.SetAngleRadTxt(angle);

        Vector3 pos = MoveDotAndLine(angle);
        float whatWave = waveFunc(angle);
        updateUI(whatWave);
        info.SetLineFromCircle(pos, new Vector2(angle, whatWave));
        angle += angleSteps;
    }

    Vector3 MoveDotAndLine(float angle)
    {
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);

        Vector2 pos = new Vector2(x, y);

        radiusLine.SetPosition(1, pos);
        dot.transform.position = pos;

        return pos;
    }
}
