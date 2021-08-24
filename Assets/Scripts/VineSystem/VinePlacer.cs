using System;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class VinePlacer : MonoBehaviour
{
    private bool placing = false;

    private Vine vine;
    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            placing = true;
            vine = new Vine();
        }

        if (placing && Input.GetMouseButtonUp(0))
        {
            placing = false;
            return;
        }

        if (!placing)
        {
            return;
        }
        
        RaycastHit rayHit;
        Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10);
        var ray = Camera.main.ScreenPointToRay(screenPos);
        var result = Physics.Raycast(ray, out rayHit);
        

        if (result)
        {
            vine.AddPoint(rayHit.point, rayHit.normal);
        }
    }
}