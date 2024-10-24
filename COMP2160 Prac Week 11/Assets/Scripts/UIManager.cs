/**
 * A singleton class to allow point-and-click movement of the marble.
 * 
 * It publishes a TargetSelected event which is invoked whenever a new target is selected.
 * 
 * Author: Malcolm Ryan
 * Version: 1.0
 * For Unity Version: 2022.3
 */

using UnityEngine;
using UnityEngine.InputSystem;
using WordsOnPlay.Utils;

// note this has to run earlier than other classes which subscribe to the TargetSelected event
[DefaultExecutionOrder(-100)]
public class UIManager : MonoBehaviour
{
#region UI Elements
    [SerializeField] private Transform crosshair;
    [SerializeField] private Transform target;
#endregion 

#region Paramaters and Attributes
    [SerializeField] private Transform board;
    private Plane plane;
    [SerializeField] private float sensitivity = 5f;
    [SerializeField] private bool useMouseDelta = true;
#endregion

#region Singleton
    static private UIManager instance;
    static public UIManager Instance
    {
        get { return instance; }
    }
#endregion 

#region Actions
    private Actions actions;
    private InputAction mouseAction;
    private InputAction deltaAction;
    private InputAction selectAction;
#endregion

#region Events
    public delegate void TargetSelectedEventHandler(Vector3 worldPosition);
    public event TargetSelectedEventHandler TargetSelected;
#endregion

#region Init & Destroy
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There is more than one UIManager in the scene.");
        }

        instance = this;

        actions = new Actions();
        mouseAction = actions.mouse.position;
        deltaAction = actions.mouse.delta;
        selectAction = actions.mouse.select;

        Cursor.visible = false;
        target.gameObject.SetActive(false);

        plane = new Plane(board.up, Vector3.zero);
    }

    void OnEnable()
    {
        actions.mouse.Enable();
    }

    void OnDisable()
    {
        actions.mouse.Disable();
    }
#endregion Init

#region Update
    void Update()
    {
        MoveCrosshair();
        SelectTarget();
    }

    private void MoveCrosshair() 
    {
        //Step 5 Solution
        Camera camera = Camera.main;

        if (useMouseDelta)
        {
            Vector2 mouseDelta = deltaAction.ReadValue<Vector2>();
            Vector3 crosshairCurrentPos = camera.WorldToScreenPoint(crosshair.position);
            Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
            Vector3 crosshairNewPos = crosshairCurrentPos + ((Vector3)mouseDelta * sensitivity);
            crosshairNewPos = screenRect.Clamp(crosshairNewPos);
            Ray crosshairRay = camera.ScreenPointToRay(crosshairNewPos);
            float boardDist;
            plane.Raycast(crosshairRay, out boardDist);
            Vector3 crosshairPos = crosshairRay.GetPoint(boardDist - 0.1f);
            crosshair.position = crosshairPos;
        } else 
        {
            Vector2 mousePos = mouseAction.ReadValue<Vector2>();
            Ray crosshairRay = camera.ScreenPointToRay(mousePos);
            float boardDist;
            plane.Raycast(crosshairRay, out boardDist);
            Vector3 crosshairPos = crosshairRay.GetPoint(boardDist - 0.1f);
            crosshair.position = crosshairPos;
        }

        
        

        
        
        

        /*
        //Step 1 Solution
        Camera camera = Camera.main;
        Vector2 mousePos = mouseAction.ReadValue<Vector2>();
        //Debug.Log(mousePos);
        Vector3 crosshairCurrentPos = camera.WorldToScreenPoint(crosshair.position);
        Vector3 crosshairNewPos = new Vector3(mousePos.x, mousePos.y, crosshairCurrentPos.z);
        crosshair.position = camera.ScreenToWorldPoint(crosshairNewPos);
        */

        /*
        //Step 4 Solution
        Camera camera = Camera.main;
        Vector2 mousePos = mouseAction.ReadValue<Vector2>();
        Ray mouseRay = camera.ScreenPointToRay(mousePos);
        float boardDist;
        plane.Raycast(mouseRay, out boardDist);
        Vector3 crosshairPos = mouseRay.GetPoint(boardDist - 0.1f);
        crosshair.position = crosshairPos;
        */
    }

    private void SelectTarget()
    {
        if (selectAction.WasPerformedThisFrame())
        {
            // set the target position and invoke 
            target.gameObject.SetActive(true);
            target.position = crosshair.position;     
            TargetSelected?.Invoke(target.position);       
        }
    }

#endregion Update

}
