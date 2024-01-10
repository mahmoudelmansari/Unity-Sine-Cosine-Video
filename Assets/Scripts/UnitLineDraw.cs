using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitLineDraw : MonoBehaviour
{
    [SerializeField] UIAnimationsSecondPhase uiAnimation;
    [SerializeField] LineRenderer unitLine_1;
    [SerializeField] LineRenderer upperLine;
    [SerializeField] LineRenderer lowerLine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            StartCoroutine(DrawUnitLine());

        if (Input.GetKeyDown(KeyCode.J))
            StartCoroutine(RemoveLine());
    }

    IEnumerator DrawUnitLine()
    {
        float t = 0;
        float duration = 0.25f;

        unitLine_1.positionCount = 3;
        unitLine_1.SetPosition(0, Vector2.left);
       
        Vector2 startPos = unitLine_1.GetPosition(0);
        Vector2 endPos = startPos + Vector2.up;
        Vector2 endPos_2 = startPos + Vector2.down;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            Vector2 pos = Vector2.Lerp(startPos, endPos, t);
            Vector2 pos2 = Vector2.Lerp(startPos, endPos_2, t);
            unitLine_1.SetPosition(1, pos);
            unitLine_1.SetPosition(2, pos2);
            yield return null;
        }
        t = 0;

        upperLine.positionCount = 2;
        lowerLine.positionCount = 2;

        upperLine.SetPosition(0, new Vector2(-1.035f,1));
        lowerLine.SetPosition(0, new Vector2(-1.035f,-1));

        startPos = upperLine.GetPosition(0);
        endPos = startPos + (Vector2.right * 0.3f);

        Vector2 startPosBotton = lowerLine.GetPosition(0);
        Vector2 endPosBottom = startPosBotton + (Vector2.right * 0.3f);

        while (t < 1)
        {
            t += Time.deltaTime / duration;
            Vector2 pos = Vector2.Lerp(startPos, endPos, t);
            Vector2 pos2 = Vector2.Lerp(startPosBotton, endPosBottom, t);

            upperLine.SetPosition(1, pos);
            lowerLine.SetPosition(1, pos2);
            yield return null;
        }

        uiAnimation.ShowNumbers(0);
        //uiAnimation.ShowVariablesTxt();
    }

    public void ChangeUnitLine(float yValue, string num1, string num2, float offset)
    {
        StartCoroutine(ChangeUnitLineCoroutine(yValue, num1, num2, offset));
    }

    IEnumerator ChangeUnitLineCoroutine(float yValue,string num1,string num2, float offset)
    {
        uiAnimation.HideNumbers(1);
        float t = 0;
        float duration = 0.25f;

        Vector2 startPos = unitLine_1.GetPosition(1);
        Vector2 endPos = startPos + Vector2.up * yValue;

        Vector2 startPos_2 = unitLine_1.GetPosition(2);
        Vector2 endPos_2 = startPos_2 + Vector2.up * -yValue;

        Vector2 upperStartPos1 = upperLine.GetPosition(0);
        Vector2 upperStartPos2 = upperLine.GetPosition(1);

        Vector2 upperEndPos1 = upperStartPos1 + Vector2.up * yValue;
        Vector2 upperEndPos2 = upperStartPos2 + Vector2.up * yValue;

        Vector2 lowerStartPos1 = lowerLine.GetPosition(0);
        Vector2 lowerStartPos2 = lowerLine.GetPosition(1);

        Vector2 lowerEndPos1 = lowerStartPos1 + Vector2.up * -yValue;
        Vector2 lowerEndPos2 = lowerStartPos2 + Vector2.up * -yValue;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            Vector2 pos = Vector2.Lerp(startPos, endPos, t);
            Vector2 pos2 = Vector2.Lerp(startPos_2, endPos_2, t);

            unitLine_1.SetPosition(1, pos);
            unitLine_1.SetPosition(2, pos2);

            Vector2 upperPos1 = Vector2.Lerp(upperStartPos1, upperEndPos1, t);
            Vector2 upperPos2 = Vector2.Lerp(upperStartPos2, upperEndPos2, t);

            Vector2 lowerPos1 = Vector2.Lerp(lowerStartPos1, lowerEndPos1, t);
            Vector2 lowerPos2 = Vector2.Lerp(lowerStartPos2, lowerEndPos2, t);

            upperLine.SetPosition(0, upperPos1);
            upperLine.SetPosition(1, upperPos2);

            lowerLine.SetPosition(0, lowerPos1);
            lowerLine.SetPosition(1, lowerPos2);
            yield return null;
        }

        uiAnimation.ChangeNumberTxts(num1,num2,offset);
        uiAnimation.ShowNumbers(1);

    }

    IEnumerator RemoveLine()
    {
        uiAnimation.HideNumbers(0);
        float t = 0;
        float duration = 0.25f;

        unitLine_1.positionCount = 3;
        unitLine_1.SetPosition(0, Vector2.left);

        Vector2 startPos = unitLine_1.GetPosition(1);
        Vector2 startPos_2 = unitLine_1.GetPosition(2);
        Vector2 endPos = unitLine_1.GetPosition(0);

        Vector2 upperStartPos = upperLine.GetPosition(1);
        Vector2 upperEndPos = upperLine.GetPosition(0);

        Vector2 startPosBotton = lowerLine.GetPosition(1);
        Vector2 endPosBottom = lowerLine.GetPosition(0);


        while(t < 1)
        {
            t += Time.deltaTime / duration;

            Vector2 pos3 = Vector2.Lerp(upperStartPos, upperEndPos, t);
            Vector2 pos4 = Vector2.Lerp(startPosBotton, endPosBottom, t);

            upperLine.SetPosition(1, pos3);
            lowerLine.SetPosition(1, pos4);
            yield return null;
        }
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            Vector2 pos = Vector2.Lerp(startPos, endPos, t);
            Vector2 pos2 = Vector2.Lerp(startPos_2, endPos, t);
            unitLine_1.SetPosition(1, pos);
            unitLine_1.SetPosition(2, pos2);
            yield return null;
        }

     

    }
}
