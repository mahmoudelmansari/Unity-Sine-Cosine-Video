using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveInfo : MonoBehaviour
{
    [field: SerializeField] public LineRenderer wave { get; set; }
    [field: SerializeField] public LineRenderer lineFromCircle { get; set; }
    [field: SerializeField] public LineRenderer waveLengthLine { get; set; }
    [field: SerializeField] public GameObject dot { get; set; }

    float angleSteps;

    [field: SerializeField] public float waveStartOffset { get; set; }
    [field: SerializeField] public float wavePeak { get; set; }
    [field: SerializeField] public float waveWide { get; set; }

    [field: SerializeField] public int steps { get; private set; }
    [field: SerializeField] public float drawTime { get; private set; }

    [field: SerializeField] public float offset { get; private set; }
    [SerializeField] float offsetTarget; 

    [SerializeField] float waveLength;
    public bool animate;
    public bool animateUnitWave;
    float time = 0;

    bool clearOnce;
     public bool startDrawing;

    [SerializeField] Color waveColor;

    [SerializeField] Effects effect;
    // Start is called before the first frame update
    void Start()
    {
        angleSteps = ((Mathf.PI * 2) * waveLength) / steps;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            StartCoroutine(ReduceWave());
    }

    [SerializeField] float targetPeak;
    [SerializeField] float targetLowPeak;
    public void IncreaseAmplitude()
    {
        DOTween.To(() => wavePeak, x => wavePeak = x, targetPeak, 1f).onUpdate = (() => UIAnimations.instance.MultiplySineTxt(wavePeak));
        targetPeak = 1;
    }

    public void DecreaseAmplitude()
    {
        DOTween.To(() => wavePeak, x => wavePeak = x, targetLowPeak, 1f).onUpdate = (() => UIAnimations.instance.MultiplySineTxt(wavePeak));
    }

    [SerializeField] float targetWide;
    [SerializeField] float targetLowWide;
    public void IncreaseWaveLength()
    {
        DOTween.To(() => waveWide, x => waveWide = x, targetWide, 2f).onUpdate = (() => UIAnimations.instance.MultiplyTetaTxt(waveWide,wavePeak));
    }

    public void DecreaseWaveLength()
    {
        DOTween.To(() => waveWide, x => waveWide = x, targetLowWide, 2f).onUpdate = (() => UIAnimations.instance.MultiplyTetaTxt(waveWide,wavePeak));
        targetWide = 1;
    }

    public void OffsetWave()
    {
        DOTween.To(() => offset, x => offset = x, offsetTarget, 1f).onUpdate = (() => UIAnimations.instance.AddOffset(offset,waveWide,wavePeak));
        offsetTarget = 0;
    }

    public void ResetWave()
    {
        IncreaseAmplitude();
        IncreaseWaveLength();
        OffsetWave();
    }

    [SerializeField] Transform waveOffset;
    public IEnumerator DrawWave(Func<float, float> waveFunc)
    {
        yield return new WaitForSeconds(0.5f);

        //dot.SetActive(true);
        float angle = 2* Mathf.PI;

        for (int i = 0; i < 50; i++)
        {
            wave.positionCount++;
            float y = waveFunc(angle * 2f) * 0.5f;

            Vector2 pos = new Vector2(angle, y) - (Vector2.right * waveStartOffset);
            //dot.transform.position = pos;
            wave.SetPosition(i, pos + (Vector2)waveOffset.position);
            angle += angleSteps;
            yield return new WaitForSeconds(drawTime);
        }
        startDrawing = true;

        //animate = true;
    }

    public void UpdateWave(Func<float, float> waveFunc)
    {
        float angle = 2 * Mathf.PI;
        time += Time.deltaTime * waveWide;
        for (int i = 0; i < 50; i++)
        {
            float y = waveFunc(time + angle * 2f) * 0.5f *wavePeak;

            Vector2 pos = new Vector2(angle, y) - (Vector2.right * waveStartOffset);
            //dot.transform.position = pos;
            wave.SetPosition(i, pos + (Vector2)waveOffset.position);
            angle += angleSteps;
        }
        // ----------------------------------------------

        //angleSteps = ((Mathf.PI * 2) * 2f) / wave.positionCount;
        /*
        wave.positionCount = 50;
        angleSteps = ((Mathf.PI * 2)) / wave.positionCount;
        float angle = 2 * Mathf.PI;
        for (int i = 0; i < wave.positionCount; i++)
        {
            float y = waveFunc(angle * 2f) * 0.5f;
            Vector2 pos = new Vector2(angle, y) + (Vector2)waveOffset.position;
            dot.transform.position = pos;
            wave.SetPosition(i, pos );
            angle += angleSteps;
        }
        */
    }
    static bool firstUnit;
    public IEnumerator DrawUnitWave(Func<float, float> waveFunc)
    {
        angleSteps = (Mathf.PI * 2f) / steps;
        int dividedSteps = steps / 4;
        float angle = angleSteps;
        dot.SetActive(true);
        dot.transform.DOScale(Vector3.one * 0.05f, 0.25f).SetEase(Ease.OutBounce);
        wave.positionCount = 0;

        int rotateTimes = 0;
        int startIndex = 0;
        int endIndex = dividedSteps;

        while (rotateTimes < 8)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                DividedUnitWave(waveFunc, i, ref angle);
                yield return new WaitForSeconds(drawTime);
            }
            effect.SetDrawinfToTrue(new Vector2(angle, waveFunc(angle)));
            if (firstUnit && rotateTimes == 0)
                UIAnimations.instance.ShowAmplitudeTxt(new Vector2(Mathf.Cos(angle) + 1, Mathf.Sin(angle) + 0.3f), waveColor);

            rotateTimes++;
            startIndex = endIndex;
            endIndex += dividedSteps;
            UIAnimations.instance.SpawnDegValueTxt(dot.transform.position + Vector3.up * 0.3f * (Mathf.Abs(dot.transform.position.y)/ dot.transform.position.y));
            
            if (rotateTimes == 4 && firstUnit == false)
            {
                yield return new WaitForSeconds(4f);

                CameraMovement.instance.ZoomAndMoveRight();
                UIAnimations.instance.MoveTextsLeft();
                firstUnit = true;
            }

            if(rotateTimes == 1)
            {
                if (firstUnit == false)
                {
                    yield return new WaitForSeconds(5f);
                    UIAnimations.instance.ShowAmplitudeTxt(dot.transform.position + Vector3.up * 0.3f, waveColor);
                }


                yield return new WaitForSeconds(3f);
                UIAnimations.instance.HideAmplitudeTxt();
            }
            else if (rotateTimes < 4)
              yield return new WaitForSeconds(4f);
            else
              yield return new WaitForSeconds(5f);

        }
        firstUnit = true;
        UIAnimations.instance.ResetValueIndex();
        StartCoroutine(RemoveLineFromCircle());
        effect.ChangeColor(new Color32(49,187,250,255));
    }
    void DividedUnitWave(Func<float, float> waveFunc, int i, ref float angle)
    {
        wave.positionCount++;
        float y = waveFunc(angle);
        Vector2 pos = new Vector2(angle, y);
        dot.transform.position = pos;
        wave.SetPosition(i, pos);
        angle += angleSteps;
    }

    IEnumerator ContinueUnitWave(Func<float, float> waveFunc)
    {
        CameraMovement.instance.ZoomAndMoveRight();
        UIAnimations.instance.MoveTextsLeft();
        float angle = angleSteps + (Mathf.PI * 2f);
        for (int i = steps; i < steps *2; i++)
        {
            DividedUnitWave(waveFunc, i,ref angle);
            yield return new WaitForSeconds(drawTime);

        }
        CameraMovement.instance.ZoomInAndMoveCenter();
        UIAnimations.instance.MoveTextsToCenter();

    }

    void MoveWavePoints(Func<float, float> waveFunc, ref float angle, int index)
    {
        float y = waveFunc((angle + time) * waveWide) * wavePeak;

        Vector2 pos = new Vector2(angle, y) - (Vector2.right * waveStartOffset);
        //dot.transform.position = pos;
        wave.SetPosition(index, pos + new Vector2(5,5));
        angle += angleSteps;
    }

    public void AnimateWave(Func<float,float> waveFunc)
    {
        if (wave.positionCount < steps)
            return;

        float angle = angleSteps;
        time += Time.deltaTime * 2;

        for (int i = 0; i < wave.positionCount; i++)
        {
            MoveWavePoints(waveFunc, ref angle, i);
        }

        if (clearOnce == false) 
        {
            StartCoroutine(ClearWave());
            clearOnce = true;
        }
    }
    public void AnimateWaveByAngle(Func<float, float> waveFunc)
    {
        waveStartOffset = 0;
        float angle = angleSteps;
       
        time += Time.deltaTime * 2;
        float endAngle = (Mathf.PI * 2) * 2;

        for (int i = 0; i < wave.positionCount; i++)
        {
            MoveWavePoints(waveFunc, ref angle, i);
            if (time > endAngle)
            {
                time = endAngle;
                animateUnitWave = false;
                startDrawing = true;
            }
        }      
    }

    public void ResetTimer() => time = 0;

    public IEnumerator ClearWave()
    {
        yield return new WaitForSeconds(3f);

        Stack<Vector3> points = new Stack<Vector3>();

        for (int i = wave.positionCount - 1; i >= 0; i--)
        {
            points.Push(wave.GetPosition(i));
        }

        animate = false;
        for (int i = 0; i < wave.positionCount/2; i++)
        {
            points.Pop();
            wave.SetPositions(points.ToArray());
            points.Pop();
            wave.SetPositions(points.ToArray());
            yield return null;
        }

        wave.positionCount = 0;
        dot.transform.DOScale(Vector3.one * 0.1f, 0.25f).OnComplete(() => dot.transform.DOScale(Vector3.zero, 0.25f));


    }

    public IEnumerator ReduceWave()
    {
        yield return new WaitForSeconds(1f);
        startDrawing = false;
        int posCount = wave.positionCount;
        for (int i = 0; i < posCount; i++)
        {
            wave.positionCount--;
            //if(wave.positionCount > 0)
            //  dot.transform.position = wave.GetPosition(wave.positionCount);
            yield return new WaitForSeconds(0.02f);
        }

        dot.transform.DOScale(Vector2.one * 0.07f, 0.25f).OnComplete(() => dot.transform.DOScale(Vector2.zero, 0.25f));
    }

    public void SetLineFromCircle(Vector2 startPos, Vector2 endPos)
    {
        lineFromCircle.positionCount = 2;
        lineFromCircle.SetPosition(0, startPos);
        lineFromCircle.SetPosition(1, endPos);
    }

    public IEnumerator RemoveLineFromCircle()
    {
        float t = 0;
        float duration = 0.5f;
        
        while( t < 1 )
        {
            t += Time.deltaTime / duration;
            Vector2 pos = Vector2.Lerp(Vector2.zero, lineFromCircle.GetPosition(1), t);
            lineFromCircle.SetPosition(0, pos);
            yield return null;
        }
    }

    public IEnumerator DrawWaveLength()
    {
        waveLengthLine.positionCount += 2;
        float t = 0;
        float duration = 0.5f;
        float startAngle = Mathf.PI * 0.5f;
        float y = Mathf.Sin(startAngle);
        Vector2 startPos = new Vector2(startAngle, y) - Vector2.up * 0.5f;

        float endAngle = Mathf.PI * 2 + Mathf.PI * 0.5f;
        float y2 = Mathf.Sin(endAngle);
        Vector2 endPos = new Vector2(endAngle, y2) - Vector2.up * 0.5f;

        waveLengthLine.SetPosition(0, startPos);
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            Vector2 pos = Vector2.Lerp(startPos, endPos, t);
            waveLengthLine.SetPosition(1, pos);
            yield return null;
        }

        UIAnimations.instance.ShowWaveLength();

        yield return new WaitForSeconds(1.5f);

        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            Vector2 pos = Vector2.Lerp(startPos, endPos, t);
            waveLengthLine.SetPosition(0, pos);
            yield return null;
        }
    }
}
