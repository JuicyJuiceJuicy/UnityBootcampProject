using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualControllerUI : MonoBehaviour
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
        lineRenderer.SetPosition(1, VirtualControllerManager.Instance.MousePositionScreen);
    }

    private void OnEnable()
    {
        transform.position = VirtualControllerManager.Instance.MousePositionDownScreen;
        transform.rotation = Quaternion.Euler(0, 0, VirtualControllerManager.Instance.angleAdjust);

        controllerBase.transform.localScale = Vector3.one * VirtualControllerManager.Instance.MainCamera.orthographicSize / 5;
        neutralCircle.transform.localScale = Vector3.one * VirtualControllerManager.Instance.distanceAdjust * VirtualControllerManager.Instance.MainCamera.orthographicSize * 4 / Screen.height;

        lineRenderer.SetPosition(0, VirtualControllerManager.Instance.MousePositionDownScreen);
    }
}
