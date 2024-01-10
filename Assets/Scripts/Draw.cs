using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    [SerializeField] LineRenderer gridLine;
    List<LineRenderer> gridLines = new List<LineRenderer>();
    int index;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))

        {   if(index ==0)
               StartCoroutine(DrawGrid());
            else if (index == 1)
                StartCoroutine(ResetGridLines());

            index++;
        }
    }

    IEnumerator DrawGrid()
    {
        int count = 16;
        float duration = 0.05f;

        for (int i = -16; i < count; i++)
        {
            float t = 0;
            LineRenderer gridLn = Instantiate(gridLine);
            gridLines.Add(gridLn);
            Vector2 pos_1 = new Vector2(i, 5f);
            gridLn.SetPosition(0, pos_1);
            Vector2 endPos = new Vector2(i, -5f);
            SetCentralLine(gridLn,i);
            while (t < 1)
            {
                t += Time.deltaTime / duration;
                Vector2 pos = Vector2.Lerp(pos_1, endPos, t);
                gridLn.SetPosition(1, pos);
                yield return null;

            }
        }

        for (int i = 9; i > -9; i--)
        {
            float t = 0;
            LineRenderer gridLn = Instantiate(gridLine);
            gridLines.Add(gridLn);
            Vector2 pos_1 = new Vector2(20, i);
            gridLn.SetPosition(0, pos_1);
            Vector2 endPos = new Vector2(-10, i);
            SetCentralLine(gridLn, i);
            while (t < 1)
            {
                t += Time.deltaTime / duration;
                Vector2 pos = Vector2.Lerp(pos_1, endPos, t);
                gridLn.SetPosition(1, pos);
                yield return null;

            }
        }

    }

    IEnumerator ResetGridLines()
    {
        float duration = 0.05f;
        for (int i = gridLines.Count - 1; i >= 0; i--)
        {
            float t = 0;
            LineRenderer line = gridLines[i];
            Vector2 startPos = line.GetPosition(1);
            Vector2 endPos = line.GetPosition(0);
            while (t < 1)
            {
                t += Time.deltaTime / duration;
                Vector2 pos = Vector2.Lerp(startPos, endPos, t);
                line.SetPosition(1, pos);
                yield return null;
            }
        }

        gridLine.gameObject.SetActive(false);
    }

    void SetCentralLine(LineRenderer gridLn, int index)
    {
        if(index == 0)
        {
            float width = 0.02f;
            gridLn.startWidth = width;
            gridLn.endWidth = width;

            Color color = Color.white;
            color.a = 0.9f;
            gridLn.startColor = color;
            gridLn.endColor = color;
        }
    }
    
}
