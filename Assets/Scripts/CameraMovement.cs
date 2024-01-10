using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement instance;
    private Camera cam;
    private float startSize;

    [SerializeField] float xDistance;
    [SerializeField] float transitionTime;

    [SerializeField] UnityEvent returnToCenter;
    [SerializeField] Vector3 movePos;
    [SerializeField] Vector3 rotateVec;
    [SerializeField] Vector3 secondPos;
    [SerializeField] Vector3 Pos3;
    [SerializeField] Vector3 resetPos;
    [SerializeField] Vector3 forwadPos;
    [SerializeField] Vector3 lastPos;

    [SerializeField] PostProcessVolume volume;
    DepthOfField depth;

    int posIndex;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        startSize = cam.orthographicSize;
        volume.profile.TryGetSettings(out depth);
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    ZoomOut();
        //    MoveCamRight();
        //}

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            returnToCenter?.Invoke();
            ZoomInAndMoveCenter();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (posIndex == 0)
                PerspCam();        
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (posIndex == 0)
                MoveUp();
            else if (posIndex == 1)
                MoveUpMore();
            else if (posIndex == 2)
                BackToZero();
            else if (posIndex == 3)
                MoveForward();
            else if (posIndex == 4)
                BackBackward();

            posIndex++;
        }
           
    }

    void PerspCam()
    {
        cam.orthographic = false;
        transform.DOMove(movePos, 1.5f);
        transform.DORotate(rotateVec, 1.5f);
    }

    void MoveUp()
    {
        transform.DOMove(secondPos, 1f);
    }

    void MoveUpMore()
    {
        transform.DOMove(Pos3, 1f);
    }

    void BackToZero()
    {
        transform.DOMove(resetPos, 1f);
        transform.DORotate(Vector3.zero, 1.5f);
        depth.active = true;
    }

    void BackBackward()
    {
        transform.DOMove(lastPos, 1f);
    
    }

    void MoveForward()
    {
        transform.DOMove(forwadPos, 4f);
    }



    public void ZoomAndMoveRight()
    {
        ZoomOut();
        MoveCamRight();
    }

    void ZoomOut()
    {
        DOTween.To(() => cam.orthographicSize, x => cam.orthographicSize = x, 5f, transitionTime);
    }

    void MoveCamRight()
    {

        transform.DOMoveX(xDistance, transitionTime);    
    }

    public void ZoomInAndMoveCenter()
    {
        ZoomIn();
        MoveCameraToCenter();
    }

    void ZoomIn()
    {
        DOTween.To(() => cam.orthographicSize, x => cam.orthographicSize = x, startSize, transitionTime);
    }

    void MoveCameraToCenter()
    {
        transform.DOMoveX(0f, transitionTime);

    }
}
