using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    [SerializeField] Vector2 direction;
    [SerializeField]  float speed;
    [SerializeField]  float amplitude;

    [SerializeField] bool increaseTime;
    static float speedStatic = 2;
    static float amplitudeStatic = 1;
    bool startMoving;
    bool startMovingCos;
    public float time;
    bool changeScale;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        time = 0;
        transform.localScale = Vector2.zero;
        ShowCircle();
    }

    // Update is called once per frame
    void Update()
    {
        if(startMoving)
           Move(Mathf.Sin);

        if (startMovingCos)
            Move(Mathf.Cos);

        if (Input.GetKeyDown(KeyCode.T))
            changeScale = true;

        if (Input.GetKeyDown(KeyCode.J))
        {
            speed = 1;
            amplitude = 1;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DOTween.To(() => speed, x => speed = x, 3f, 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            DOTween.To(() => amplitude, x => amplitude = x, 2f, 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(StopCircle());
        }

    }

    IEnumerator StopCircle()
    {
        increaseTime = false;
        yield return new WaitForSeconds(1f);

        transform.DOScale(Vector2.one * 0.15f, 0.5f).OnComplete(()=> transform.DOScale(Vector2.zero, 0.25f));
    }

    public void StartMove()
    {
        startMoving = true;
        //increaseTime = false;
    }

    public void StartMovingCos()
    {
        startMovingCos = true;
    }

    public void randomizeSpeed(float speed)
    {
        this.speed = speed;
    }

    public void randomizeAmplitude(float distance)
    {
        this.amplitude = distance;
    }

    public void randomizeDirection(Vector2 v)
    {
        direction = v;
    }

    public void StopMoving()
    {
        StartCoroutine(Stop());
    }

    IEnumerator Stop()
    {
        startMoving = false;

        yield return new WaitForSeconds(0.5f);

        transform.DOScale(Vector2.zero, 0.5f);
    }
     
    void Move(Func<float,float> waveFun)
    {
          if(increaseTime)
           time += Time.deltaTime;

        float sinOutput = waveFun(time * speed) * amplitude;
        transform.position = new Vector2(time * speed, sinOutput);

        if(changeScale)
        transform.localScale = Vector2.one * 0.1f * sinOutput;
    }

    public void ShowCircle()
    {
        if(increaseTime)
          StartCoroutine(ShowCircleCoroutine(2f));
        else
        {
            StartCoroutine(ShowCircleCoroutine(0));
        }
    }

    IEnumerator ShowCircleCoroutine(float timing)
    {
        transform.DOScale(Vector2.one * 0.1f, 0.5f).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(timing);

        //startMoving = true;
    }
}
