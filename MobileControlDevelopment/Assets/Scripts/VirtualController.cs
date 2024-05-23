using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualController : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;

    [SerializeField] GameObject controllerBase;
    [SerializeField] GameObject neutralCircle;

    private void Awake()
    {

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(1, InputSystemManager.Instance.MousePositionScreen);
    }

    private void OnEnable()
    {
        transform.position = InputSystemManager.Instance.MousePositionDownScreen;
        transform.rotation = Quaternion.Euler(0, 0, InputSystemManager.Instance.angleAdjust);

        neutralCircle.transform.localScale = Vector3.one * InputSystemManager.Instance.distanceAdjust * InputSystemManager.Instance.MainCamera.orthographicSize * 4 / Screen.height;

        lineRenderer.SetPosition(0, InputSystemManager.Instance.MousePositionDownScreen);
    }
}
