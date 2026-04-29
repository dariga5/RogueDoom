using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;



public class AimLaser : MonoBehaviour
{
    private LineRenderer laserLine; 
    private Camera mainCamera;      

    private Vector3 mouseScreen;
    private Vector3 mouseWorld;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        mainCamera = Camera.main;

    }

    void Update()
    {
        mouseScreen = Input.mousePosition;
        mouseWorld = mainCamera.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0f;

        laserLine.SetPosition(0, transform.position);
        laserLine.SetPosition(1, mouseWorld);
        
        if(Input.GetMouseButtonDown(0))
        {
            laserLine.startColor = Color.black;
            laserLine.endColor = Color.black;

            Invoke(nameof(RestoreLaserColor), 1f);
        }

    }
    void RestoreLaserColor()
    {
        laserLine.startColor = Color.red;
        laserLine.endColor = Color.red;
    }

}