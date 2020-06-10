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
    private GameObject spawnObject;
    private GameObject GameObjectToPlace;
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
        CrosshairCalculation();
        if (Input.touchCount > 0) touch = Input.GetTouch(0);

        if (Input.touchCount < 0 || touch.phase != TouchPhase.Began)
            return;
        
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
            if (spawnObject == null)
            {
                spawnObject = Instantiate(DataHandler.Instance.furniture, pose.position, pose.rotation);
                GameObjectToPlace = DataHandler.Instance.furniture;
            }
            else if (GameObjectToPlace != DataHandler.Instance.furniture)
            {
                spawnObject = Instantiate(DataHandler.Instance.furniture, pose.position, pose.rotation);
                GameObjectToPlace = DataHandler.Instance.furniture;
            }
            else
                spawnObject.transform.position = pose.position;
        }
    }
    
    bool IsPointerOverUI(Touch touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.position.x, touch.position.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
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
      
        if (_raycastManager.Raycast(ray, _hits))
        {
            pose = _hits[0].pose;
            crosshair.transform.position = pose.position;
            crosshair.transform.eulerAngles = new Vector3(90,0,0);
        }
    }
}