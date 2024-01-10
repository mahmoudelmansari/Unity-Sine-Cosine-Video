using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    float time;
    bool startMoving;
    [SerializeField] Material mat;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one * 0.2f, 0.25f).SetEase(Ease.OutBack).OnComplete(() => startMoving = true);
    }

    // Update is called once per frame
    void Update()
    {
        if (startMoving)
            Move();

        if (Input.GetKeyDown(KeyCode.N))
            Hide();

        if (Input.GetKeyDown(KeyCode.N))
            Hide();
    }

    void Move()
    {
        time += Time.deltaTime;
        float sinOutput = Mathf.Sin(time) * 0.5f;
        transform.position =  new Vector3(transform.position.x, sinOutput,transform.position.z);
    }

    public void ChangeColor(Color32 color)
    {
        mat.color = color;
    }

    void Hide()
    {
        transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(gameObject));
    }
}
