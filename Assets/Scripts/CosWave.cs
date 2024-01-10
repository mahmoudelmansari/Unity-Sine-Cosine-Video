using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosWave : MonoBehaviour
{
    [field: SerializeField] public WaveInfo waveInfo { get; set; }
    bool animateCos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            DrawCosWave();

        if (Input.GetKeyDown(KeyCode.A))
            waveInfo.animate = true;

        if (waveInfo.animate)
            AnimateCosWave();

        if (Input.GetKeyDown(KeyCode.X))
        {
            waveInfo.ResetTimer();
            waveInfo.animateUnitWave = true;
            animateCos = true;
        }

        if (waveInfo.animateUnitWave)
            AnimateConWaveByAngle();

        if(animateCos)
        {
            if (waveInfo.startDrawing)
                waveInfo.UpdateWave(Mathf.Cos);

            if (Input.GetKeyDown(KeyCode.I))
                waveInfo.IncreaseAmplitude();

            if (Input.GetKeyDown(KeyCode.D))
                waveInfo.DecreaseAmplitude();

            if (Input.GetKeyDown(KeyCode.B))
                waveInfo.IncreaseWaveLength();

            if (Input.GetKeyDown(KeyCode.N))
                waveInfo.DecreaseWaveLength();

            if (Input.GetKeyDown(KeyCode.O))
                waveInfo.OffsetWave();

            if (Input.GetKeyDown(KeyCode.Q))
                waveInfo.ResetWave();

        }

    }

    public void DrawCosWave()
    {
        StartCoroutine(waveInfo.DrawWave(Mathf.Cos));
    }
    void AnimateCosWave()
    {

        waveInfo.AnimateWave(Mathf.Cos);
    }

    public void SetLineFromCircle(Vector2 startPos, Vector2 endPos)
    {
        waveInfo.SetLineFromCircle(startPos, endPos);
    }

    void AnimateConWaveByAngle()
    {
        //StartCoroutine(waveInfo.AnimateWaveByAngle(Mathf.Sin));
        waveInfo.AnimateWaveByAngle(Mathf.Cos);
    }


}
