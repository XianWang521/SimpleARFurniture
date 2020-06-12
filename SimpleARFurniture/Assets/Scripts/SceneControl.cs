using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//this part is to control scenes
public class SceneControl : MonoBehaviour
{
    //this function is to load the second scene(main scene)
    //main scene contains the full functions like tap to place an AR object.
    //when you click the button in the start scene, this function will be activated to enter the main scene
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}
