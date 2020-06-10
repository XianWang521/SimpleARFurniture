using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpManager : MonoBehaviour
{
    private Button btn;
    private GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(DeleteObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void DeleteObject()
    {
        obj = InputManager.nowObject;
        obj.transform.localScale = new Vector3((float)(obj.transform.localScale.x * 1.25), (float)(obj.transform.localScale.y * 1.25), (float)(obj.transform.localScale.z * 1.25));
    }
}
