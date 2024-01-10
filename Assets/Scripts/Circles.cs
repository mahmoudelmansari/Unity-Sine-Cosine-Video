using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circles : MonoBehaviour
{
    [SerializeField] CircleMovement circlePrefab;
    [SerializeField] CircleMovement[] circles;
    [SerializeField] UnitLineDraw unitLine;
    [SerializeField] UIAnimationsSecondPhase uiAnime;

    int changeIndex;
    public float repeatTime;

    bool spawn = true;
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating(nameof(SpawnCircle),0f, repeatTime);
    }

    IEnumerator SpawnCircleWave()
    {
        circles[0].gameObject.SetActive(true);
        circles[0].StartMove();

        yield return new WaitForSeconds(repeatTime);

        while(spawn)
        {
            CircleMovement c = Instantiate(circlePrefab, new Vector2(-6, -1), Quaternion.identity);
            c.gameObject.SetActive(true);
            c.StartMove();
            yield return new WaitForSeconds(repeatTime);

        }
    }

    void SpawnCircle()
    {
        
       

        //CircleMovement c2 = Instantiate(circlePrefab, new Vector2(-6, 1), Quaternion.identity);
        //c2.gameObject.SetActive(true);
        //c2.StartMovingCos();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            ShowFirstCircle();

        if (Input.GetKeyDown(KeyCode.R))
            SetRandomSpeed();

        if (Input.GetKeyDown(KeyCode.I))
            ChangeAmplitude(1,"2","-2",2);

        if (Input.GetKeyDown(KeyCode.D))
            ChangeAmplitude(-1.5f,"0,5","-0,5",0.25f);

        if (Input.GetKeyDown(KeyCode.B))
            changeSpeed(6);

        if (Input.GetKeyDown(KeyCode.N))
            changeSpeed(2);

        if (Input.GetKeyDown(KeyCode.V))
            StartCoroutine(ShowOtherCircles());

        if (Input.GetKeyDown(KeyCode.K))
        {
            if(changeIndex == 0)
              ChangeSomeCircles();

            if (changeIndex == 1)
                ChangeSomeCirclesTwo();

            if (changeIndex == 2)
                ChangeSomeCirclesThree();
            if(changeIndex == 3)
            {
                StopAllCircles();
            }

            changeIndex++;
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            spawn = false;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DOTween.To(() => repeatTime, x => repeatTime = x, 0.15f, 0.15f);
        }

        if(Input.GetKeyDown(KeyCode.U))
        {
            StartCoroutine(SpawnCircleWave());
        }

    }

    void StopAllCircles()
    {
        foreach(CircleMovement c in circles)
        {
            c.StopMoving();
        }
        uiAnime.HideTimeTxt();
    }

    void ShowFirstCircle()
    {
        circles[0].gameObject.SetActive(true);
    }

    void ChangeAmplitude(float amplitude, string num1, string num2, float offset)
    {
        circles[0].randomizeAmplitude(amplitude + 1);
        unitLine.ChangeUnitLine(amplitude, num1,num2, offset);
        uiAnime.UpdateAmplitudeTxt(Mathf.Abs(amplitude + 1));
    }


    void changeSpeed(float speed)
    {
        circles[0].randomizeSpeed(speed);
        uiAnime.UpdateSpeedTxt(speed);
    }

    void SetRandomSpeed()
    {
        foreach(CircleMovement c in circles)
        {
            int randomSpeed = Random.Range(1, 10);
            int randomAmplitude = Random.Range(1, 4);
            c.randomizeSpeed(randomSpeed);
            c.randomizeAmplitude(randomAmplitude);
        }
    }

    IEnumerator ShowOtherCircles()
    {
        for (int i = 1; i < circles.Length; i++)
        {
            circles[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
    }

    void ChangeSomeCircles()
    {
        for (int i = 4; i < circles.Length; i++)
        {
            circles[i].randomizeAmplitude(2);
            circles[i].randomizeSpeed(4);
        }
    }

    void ChangeSomeCirclesTwo()
    {

        for (int i = 0; i < 2; i++)
        {
            circles[i].randomizeAmplitude(3);
            circles[i].randomizeSpeed(4);
        }

        for (int i = 2; i < 4; i++)
        {
            circles[i].randomizeAmplitude(2);
            circles[i].randomizeSpeed(2);
        }
        for (int i = 4; i < 8; i++)
        {
            circles[i].randomizeAmplitude(3);
            circles[i].randomizeSpeed(4);
        }
    }

    void ChangeSomeCirclesThree()
    {

        for (int i = 0; i < 2; i++)
        {
            circles[i].randomizeAmplitude(3);
            circles[i].randomizeSpeed(2);
        }
        for (int i = 2; i < 4; i++)
        {
            circles[i].randomizeAmplitude(4);
            circles[i].randomizeSpeed(3);
        }

        for (int i = 4; i < 8; i++)
        {
            circles[i].randomizeAmplitude(2);
            circles[i].randomizeSpeed(4);
        }

       
    }
}
