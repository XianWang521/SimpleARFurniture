using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using DG.Tweening;

public class ButtonManager : MonoBehaviour
{
    private Button btn;
    public GameObject furniture;

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(SelectObject);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    //make the select furniture as DataHandler's instance, which means the furniture had now.
    //this object are handled to palce or other operating
    void SelectObject()
    {
        DataHandler.Instance.furniture = furniture;
    }
}
