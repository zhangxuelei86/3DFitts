﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.Mouse;

public class Mouse2DInputBehaviour : CursorPositioningController
{
    Vector3 mousePosition;

    public Transform targetPlane;
    public PlaneOrientation plane = PlaneOrientation.PlaneXY;
    public Vector3 spaceSize = new Vector3(1, 1, 1);
    public Vector3 offset = Vector3.zero;

    private void Update()
    {
        Vector3 screenPos = Input.mousePosition;

        float minScreenSize = Mathf.Min(Screen.width, Screen.height);
        float xCoord = Mathf.Clamp((screenPos.x - 0.5f*Screen.width) / minScreenSize, -1, 1) * spaceSize.x;
        float yCoord = Mathf.Clamp((screenPos.y - 0.5f*Screen.height) / minScreenSize, -1, 1) * spaceSize.y;

        switch (plane)
        {
            case PlaneOrientation.PlaneXY:
                mousePosition.x = xCoord + offset.x;
                mousePosition.y = offset.y;
                mousePosition.z = yCoord + offset.z;
                break;
            case PlaneOrientation.PlaneYZ:
                mousePosition.x = Mathf.Cos(-Mathf.PI * targetPlane.rotation.eulerAngles.y / 180) * xCoord + offset.x;
                mousePosition.y = yCoord + offset.y;
                mousePosition.z = Mathf.Sin(-Mathf.PI * targetPlane.rotation.eulerAngles.y / 180) * xCoord + offset.z;
                break;
            case PlaneOrientation.PlaneZX:
                mousePosition.x = offset.x;
                mousePosition.y = xCoord + offset.y;
                mousePosition.z = yCoord + offset.z;
                break;
        }        
    }

    public override string GetDeviceName()
    {
        return "Mouse";
    }

    public override Vector3 GetCurrentCursorPosition()
    {
        return mousePosition;
    }

    public override int GetTrackedHandId()
    {
        return 0;
    }
}