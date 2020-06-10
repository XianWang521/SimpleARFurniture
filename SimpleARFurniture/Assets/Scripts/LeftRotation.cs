using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftRotation : MonoBehaviour
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
        obj.transform.Rotate(new Vector3(0, -15, 0));
    }
}
