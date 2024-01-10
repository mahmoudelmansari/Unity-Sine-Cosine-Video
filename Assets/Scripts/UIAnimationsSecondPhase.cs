using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIAnimationsSecondPhase : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] numberTxts;

    [SerializeField] TextMeshProUGUI amplitudeTxt;
    [SerializeField] TextMeshProUGUI speedTxt;
    [SerializeField] TextMeshProUGUI timeTxt;

    bool updateTime;
    public CircleMovement c;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ShowVariablesTxt();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            HideVariablesTxt();
            HideTimeTxt();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            float speed = 0;
            DOTween.To(() => speed, x => speed = x, 3f, 0.5f).onUpdate = () => speedTxt.text = "speed = " + speed.ToString("0.00");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            float amplitude = 0;
            DOTween.To(() => amplitude, x => amplitude = x, 2f, 0.5f).onUpdate = () => amplitudeTxt.text = "amplitude = " + amplitude.ToString("0.00");
        }

        UpdateTimeTxt(c.time);
        
    }

    public void ShowNumbers(int startIndex)
    {
        for (int i = startIndex; i < numberTxts.Length; i++)
        {
            numberTxts[i].transform.DOScale(Vector2.one, 0.5f).SetEase(Ease.OutBounce);
       }
    }

    public void HideNumbers(int startIndex)
    {
        for (int i = startIndex; i < numberTxts.Length; i++)
        {
            numberTxts[i].transform.DOScale(Vector2.zero, 0.25f);
        }

    }

    public void ChangeNumberTxts(string num1,string num2,float offset)
    {
        numberTxts[1].text = num1;
        Vector2 pos = numberTxts[1].rectTransform.anchoredPosition;
        pos.y *= offset;

        numberTxts[1].rectTransform.anchoredPosition = pos;

        numberTxts[2].text = num2;
        Vector2 pos2 = numberTxts[2].rectTransform.anchoredPosition;
        pos2.y *= offset;

        numberTxts[2].rectTransform.anchoredPosition = pos2;
    }

    public void ShowVariablesTxt()
    {
        amplitudeTxt.transform.DOScale(Vector2.one, 0.5f).SetEase(Ease.OutBounce);
        speedTxt.transform.DOScale(Vector2.one, 0.5f).SetEase(Ease.OutBounce);
        timeTxt.transform.DOScale(Vector2.one, 0.5f).SetEase(Ease.OutBounce);
    }

    public void HideVariablesTxt()
    {
        amplitudeTxt.transform.DOScale(Vector2.zero, 0.5f);
        speedTxt.transform.DOScale(Vector2.zero, 0.5f);
    }

    public void HideTimeTxt()
    {
        timeTxt.transform.DOScale(Vector2.zero, 0.5f);
    }

    public void UpdateAmplitudeTxt(float amplitude) => amplitudeTxt.text = "amplitude = " + amplitude.ToString();

    public void UpdateSpeedTxt(float speed) => speedTxt.text = "speed = " + speed.ToString();
  
    public void UpdateTimeTxt(float time) => timeTxt.text = "time = " + time.ToString("0.00");


}
