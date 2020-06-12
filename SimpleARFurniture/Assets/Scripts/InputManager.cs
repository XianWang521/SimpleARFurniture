using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera arCam;
    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private GameObject crosshair;
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    private Touch touch;
    private Pose pose;
    private GameObject spawnObject; //the object to spawn
    private GameObject GameObjectToPlace;   //the object which is used to place. Here are used to store the object.
    static public GameObject nowObject;
    public Button buttonUp;
    public Button buttonDown;
    public Button buttonLeft;
    public Button buttonRight;
    public Button buttonDelete;
    // Start is called before the first frame update
    void Start()
    {
        nowObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        //fisrt to load a marker to show the place a object
        CrosshairCalculation();
        //if there has a touch input, save the touch info, else return.
        if (Input.touchCount > 0) touch = Input.GetTouch(0);
        if (Input.touchCount < 0 || touch.phase != TouchPhase.Began)
            return;
        //check the touch whether the touch is point on the UI
        if (IsPointerOverUI(touch)) return;

        if (IsPointerOverGameObject(touch))
        {
            buttonUp.gameObject.SetActive(true);
            buttonDelete.gameObject.SetActive(true);
            buttonDown.gameObject.SetActive(true);
            buttonLeft.gameObject.SetActive(true);
            buttonRight.gameObject.SetActive(true);
        }
        else if (Input.touchCount > 0)
        {
            buttonUp.gameObject.SetActive(false);
            buttonDown.gameObject.SetActive(false);
            buttonDelete.gameObject.SetActive(false);
            buttonLeft.gameObject.SetActive(false);
            buttonRight.gameObject.SetActive(false);
            //check the object and decide the manipulations
            if (spawnObject == null)    //if no object in the vision
            {
                spawnObject = Instantiate(DataHandler.Instance.furniture, pose.position, pose.rotation); //instantiate the instance which is handled.
                GameObjectToPlace = DataHandler.Instance.furniture; //save the handled instance
            }
            else if (GameObjectToPlace != DataHandler.Instance.furniture) //if select the new object
            {
                spawnObject = Instantiate(DataHandler.Instance.furniture, pose.position, pose.rotation); //re-instantiate the instance
                GameObjectToPlace = DataHandler.Instance.furniture;
            }
            else //tap other place but the instance don't change
                spawnObject.transform.position = pose.position; //change position of the object showed in the vision
        }
    }
    
    //this part i learned from website
    bool IsPointerOverUI(Touch touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.position.x, touch.position.y); //store the position of touch
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results); //Emit rays to the click
        return results.Count > 0;
    }

    bool IsPointerOverGameObject(Touch touch)
    {
        RaycastHit hit;
        Vector3 origin = arCam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));
        Ray ray = arCam.ScreenPointToRay(origin);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject obj = hit.collider.gameObject;
            if (obj.name.Substring(0, 2) != "AR")
            {
                nowObject = obj;
                return true;
            }
        }
        return false;
    }

    void CrosshairCalculation()
    {
        Vector3 origin = arCam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));
        Ray ray = arCam.ScreenPointToRay(origin);

        //find the pose and position of marker
        if (_raycastManager.Raycast(ray, _hits))
        {
            pose = _hits[0].pose;
            crosshair.transform.position = pose.position;
            crosshair.transform.eulerAngles = new Vector3(90,0,0); //let the marker parallel to plane
        }
    }
}