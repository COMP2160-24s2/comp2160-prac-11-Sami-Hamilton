using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float zoomRate = 0.1f;
    [SerializeField] private float minSize = 2f;
    [SerializeField] private float maxSize = 10f;
    [SerializeField] private float minField = 20f;
    [SerializeField] private float maxField = 100f;
    private Actions actions;
    private InputAction zoomAction;
    private Camera camera;

    void Awake()
    {
        actions = new Actions();
        zoomAction = actions.camera.zoom;
        camera = GetComponent<Camera>();
    }

    private void ZoomIn()
    {
        if (camera.orthographic)
        {
            camera.orthographicSize /= zoomRate;
            camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, minSize, maxSize);
        } else
        {
            camera.fieldOfView /= zoomRate;
            camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, minField, maxField);
        }
    }

    private void ZoomOut()
    {
        if (camera.orthographic)
        {
            camera.orthographicSize *= zoomRate;
            camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, minSize, maxSize);
        } else
        {
            camera.fieldOfView *= zoomRate;
            camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, minField, maxField);
        }
    }

    void Update()
    {
        float mouseScroll = zoomAction.ReadValue<float>();
        Debug.Log(mouseScroll);
        if (mouseScroll > 0)
        {
            ZoomIn();
        }
        if (mouseScroll < 0)
        {
            ZoomOut();
        }
    }
}