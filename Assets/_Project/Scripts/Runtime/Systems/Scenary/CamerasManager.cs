using DreamTeam.Runtime.Systems.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CamerasManager : MonoBehaviour
{
    public List<GameObject> cameras = new List<GameObject>();

    private void Start()
    {
        CoreSingleton.Instance.camerasManager = this;
    }

    public void RegisterCamera(GameObject camera)
    {
        if(cameras.Contains(camera) == false)
        {
            cameras.Add(camera);
        }
    }

    public void HandleCamera(GameObject camera)
    {
        foreach(GameObject cam in cameras)
        {
            cam.SetActive(false);
        }
        camera.SetActive(true);
    }

}
