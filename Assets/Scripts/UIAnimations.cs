using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIAnimations : MonoBehaviour
{
    public static UIAnimations instance;

    [SerializeField] GameObject canvas;
    [SerializeField] TextMeshProUGUI unitCircleTxt;
    [SerializeField] TextMeshProUGUI radiusTxt;
    [SerializeField] TextMeshProUGUI angleTxt;
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] TextMeshProUGUI amplitudeTxt;
    [SerializeField] TextMeshProUGUI wavelengthTxt;
    
    [SerializeField] TextMeshProUGUI[] angleDegTxts;
    [SerializeField] TextMeshProUGUI[] angleRadTxts;
    List<Vector2> angleTxtStarPos = new List<Vector2>();

    [SerializeField] TextMeshProUGUI angleDegWaveTxt;
    string waveFunc;
    string[] degValues = { "90", "180","270", "360","360 + 90","360 + 180","360 + 270","720"};
    string[] radValues = { "π/2", "π", "3π/2", "2π", "2π + π/2", "2π + π", "2π + 3π/2", "4π" };
    int valueIndex;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        waveFunc = "Sin";

        foreach(TextMeshProUGUI txt in angleDegTxts)
        {
            angleTxtStarPos.Add(txt.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ChangeToCos(1);
            ConvertToRad();
        }

        if(Input.GetKeyDown(KeyCode.H))
        {
            HideAngleAndWaveTxts();
        }
    }

   

    public void ResetValueIndex() => valueIndex = 0;

    public void AnimateUnitCircleTxt()
    {
        StartCoroutine(UnitCircleTxtAnimation());
    }

    IEnumerator UnitCircleTxtAnimation()
    {
        unitCircleTxt.gameObject.SetActive(true);
        unitCircleTxt.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(2f);
        unitCircleTxt.transform.DOScale(Vector2.zero, 0.5f);

    }

    public void AnimateRadiusTxt()
    {
        StartCoroutine(RadiusTxtAnimation());
    }

    IEnumerator RadiusTxtAnimation()
    {
        radiusTxt.gameObject.SetActive(true);

        radiusTxt.transform.DOScale(Vector2.one, 0.5f);

        yield return new WaitForSeconds(1.5f);

        radiusTxt.transform.DOScale(Vector2.zero, 0.5f);

        yield return new WaitForSeconds(0.3f);

        radiusTxt.transform.DOScale(Vector2.zero, 0.5f);

        yield return new WaitForSeconds(0.6f);

        ShowDegTxts();

        yield return new WaitForSeconds(0.6f);

        SetAngleTxt(0);
        ShowAngleTxt();

        SetSineTxt(0);
        ShowSineTxt();

    }

    public void ShowAngleTxt()
    {
        angleTxt.gameObject.SetActive(true);

        angleTxt.transform.DOScale(Vector3.one, 0.25f);
    }

    public void SetAngleTxt(float angle)
    {
        angleTxt.text = "θ = " + angle.ToString("0");
    }

    public void SetAngleRadTxt(float angle)
    {
        angleTxt.text = "θ = " + angle.ToString("0.00");
    }

    public void ShowSineTxt()
    {
        waveText.gameObject.SetActive(true);

        waveText.transform.DOScale(Vector3.one, 0.25f);
    }

    public void SetSineTxt(float result)
    {
        waveText.text = waveFunc + "(<color=white>θ</color>) = " + "<color=white>" +  result.ToString("0.00") + "</color>";
    }

    public void ChangeToCos(float result)
    {
        angleTxt.transform.DOScale(Vector2.zero, 0.25f).OnComplete(() => angleTxt.transform.DOScale(Vector2.one, 0.25f));
        angleTxt.text = "θ = " + 0.ToString("0"); ;
        waveFunc = "Cos";
        string cosText = "Cos(<color=white>θ</color>) = " + "<color=white>" + result.ToString("0.00") + "</color>";
        Color color = new Color32(49, 187, 250, 255);  
        waveText.transform.DOScale(Vector2.zero, 0.25f).OnComplete(() => waveText.transform.DOScale(Vector2.one, 0.25f));
        waveText.text = cosText;
        waveText.color = color;
    }

    public void SetCosTxt(float result)
    {
        waveText.text = "Cos(<color=white>θ</color>) = " + "<color=white>" + result.ToString("0.00") + "</color>";
    }

    public void MultiplySineTxt(float peak)
    {
        waveText.text = waveFunc + "(<color=white>θ</color>) * " + "<color=white>" + peak.ToString("0.0") + "</color>" + " = " + "<color=white>" + 0.ToString("0.00") + "</color> " ;
    }
    public void MultiplyTetaTxt(float wide,float peak)
    {
        waveText.text = waveFunc + "(<color=white>θ * "+ wide.ToString("0.0") +"</color>) * " + "<color=white>" + peak.ToString("0.0") + "</color>" + " = " + "<color=white>" + 0.ToString("0.00") + "</color> ";
    }
    public void AddOffset(float value,float wide ,float peak)
    {
        string s = "+" + 2f.ToString("0.0");
        
        waveText.text = waveFunc + "(<color=white>θ * "+ wide.ToString("0.0") + "</color>) * " + "<color=white>" + peak.ToString("0.0") + "</color> + <color=white>" + value.ToString("0.0") +  "</color>" +  " = " + "<color=white>" + 0.5f.ToString("0.0") + "</color> ";
    }

    public void ShowDegTxts()
    {
        foreach(TextMeshProUGUI txt in angleDegTxts)
        {
            txt.gameObject.SetActive(true);
            txt.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
        }
    }

    public void ShowAmplitudeTxt(Vector2 pos,Color color)
    {
        amplitudeTxt.color = color;
        amplitudeTxt.rectTransform.anchoredPosition = ConvertFromWorldToCanavasPos(pos);
        amplitudeTxt.gameObject.SetActive(true);
        amplitudeTxt.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
    }

    public void HideAmplitudeTxt()
    {
        amplitudeTxt.transform.DOScale(Vector3.zero, 0.5f);
    }

    void MoveText(float posX)
    {
        angleTxt.transform.DOMoveX(angleTxt.transform.position.x + posX, 1f);
        waveText.transform.DOMoveX(waveText.transform.position.x + posX, 1f);

        foreach (TextMeshProUGUI txt in angleDegTxts)
        {
            txt.transform.DOMoveX(Mathf.Abs(txt.transform.position.x) + posX, 1f);
        }
    }

    public void MoveAngleTxtsToInitialPos()
    {
        for (int i = 0; i < angleDegTxts.Length; i++)
        {
            angleRadTxts[i].transform.DOMove(angleTxtStarPos[i], 1f);
        }

        Invoke(nameof(HideAngleTxts), 1.25f);
    }
    public void MoveTextsLeft()
    {
        MoveText(-755);
    }

    public void MoveTextsToCenter()
    {
        MoveText(755);
    }

    public void SpawnDegValueTxt(Vector3 worldPos)
    {
        StartCoroutine(SpawnDegValueTxtCoroutine(worldPos));
    }

    IEnumerator SpawnDegValueTxtCoroutine(Vector3 worldPos)
    {
        Vector2 UiPos = ConvertFromWorldToCanavasPos(worldPos);

        angleDegWaveTxt.rectTransform.anchoredPosition = UiPos;
        angleDegWaveTxt.gameObject.SetActive(true);
        angleDegWaveTxt.text = degValues[valueIndex];
        angleDegWaveTxt.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(3f);

        angleDegWaveTxt.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => angleDegWaveTxt.gameObject.SetActive(false));
        valueIndex++;
    }

    Vector2 ConvertFromWorldToCanavasPos(Vector3 worldPos)
    {
        RectTransform CanvasRect = canvas.GetComponent<RectTransform>();

        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(worldPos);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

        return WorldObject_ScreenPosition;
    }

    public void ShowWaveLength()
    {
        StartCoroutine(WaveLengthAnime());
    }

    IEnumerator WaveLengthAnime()
    {
        wavelengthTxt.gameObject.SetActive(true);
        wavelengthTxt.transform.DOScale(Vector2.one, 0.5f).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(1.5f);

        wavelengthTxt.transform.DOScale(Vector2.zero, 0.5f).OnComplete(() => wavelengthTxt.gameObject.SetActive(false));
    }

    public void ConvertToRad()
    {
        int i = 0;
        degValues = radValues;
        foreach(TextMeshProUGUI txt in angleRadTxts)
        {
            txt.rectTransform.anchoredPosition = angleDegTxts[i].rectTransform.anchoredPosition;
            angleDegTxts[i].transform.DOScale(Vector2.zero, 0.25f).OnComplete(() => txt.transform.DOScale(Vector2.one, 0.25f)).SetEase(Ease.OutBounce);
            i++;
        }
    }

    public void HideAngleAndWaveTxts()
    {
        waveText.transform.DOScale(Vector2.zero, 0.5f);
        angleTxt.transform.DOScale(Vector2.zero, 0.5f);
    }

    public void HideAngleTxts()
    {
        foreach(TextMeshProUGUI txt in angleRadTxts)
            txt.transform.DOScale(Vector2.one * 1.5f, 0.25f).OnComplete(() => txt.transform.DOScale(Vector2.zero,0.25f));
    }

}

