using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWave : MonoBehaviour
{
    [field: SerializeField] public WaveInfo waveInfo { get; set; }

    bool animateSin = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.P))
        {
            DrawSinWave();
        }

        if (Input.GetKeyDown(KeyCode.A))
            waveInfo.animate = true;

        if (waveInfo.animate)
            AnimateSineWave();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            waveInfo.ResetTimer();
            waveInfo.animateUnitWave = true;
        }

        if (waveInfo.animateUnitWave)
            AnimateSineWaveByAngle();


        if (waveInfo.startDrawing)
            waveInfo.UpdateWave(Mathf.Sin);

        if(animateSin)
        {
            if (Input.GetKeyDown(KeyCode.I))
                waveInfo.IncreaseAmplitude();

            if (Input.GetKeyDown(KeyCode.D))
                waveInfo.DecreaseAmplitude();

            if (Input.GetKeyDown(KeyCode.B))
                waveInfo.IncreaseWaveLength();

            if (Input.GetKeyDown(KeyCode.N))
                waveInfo.DecreaseWaveLength();

            if (Input.GetKeyDown(KeyCode.O))
                OffsetWave();

            if (Input.GetKeyDown(KeyCode.F))
                animateSin = false;

            if (Input.GetKeyDown(KeyCode.Q))
                waveInfo.ResetWave();
        }

       


        if (Input.GetKeyDown(KeyCode.W))
            StartCoroutine(waveInfo.DrawWaveLength());

        if(Input.GetKeyDown(KeyCode.J))
        {
            waveInfo.startDrawing = false;
            StartCoroutine(waveInfo.ReduceWave());
        }
    }

    public void DrawSinWave()
    {
        StartCoroutine(waveInfo.DrawWave(Mathf.Sin));
    }

    void AnimateSineWave()
    {
        //StartCoroutine(waveInfo.AnimateWaveAngle());
        waveInfo.AnimateWave(Mathf.Sin);
    }

    void AnimateSineWaveByAngle()
    {
        //StartCoroutine(waveInfo.AnimateWaveByAngle(Mathf.Sin));
        waveInfo.AnimateWaveByAngle(Mathf.Sin);
    }

    void OffsetWave()
    {
        waveInfo.OffsetWave();
    }

    public void SetLineFromCircle(Vector2 startPos,Vector2 endPos)
    {
        waveInfo.SetLineFromCircle(startPos, endPos);
    }
}
