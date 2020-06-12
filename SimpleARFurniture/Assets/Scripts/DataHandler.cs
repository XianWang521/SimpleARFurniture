using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    public GameObject furniture;
    private static DataHandler instance;
    //consider if there exist an instance. If not, find the object as the instance and return it.
    public static DataHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataHandler>(); //find the object which type is DataHandler
            }
            return instance;
        }
    }
}
