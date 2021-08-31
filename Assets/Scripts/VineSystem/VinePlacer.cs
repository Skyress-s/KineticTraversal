using System;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;
using UnityEngine.InputSystem;

public class VinePlacer : MonoBehaviour
{
    private bool placing = false;

    private Vine vine;

    private Keyboard kb;

    private void Start()
    {
        kb = InputSystem.GetDevice<Keyboard>();
    }

    private void Update()
    {
        
        if (kb.hKey.wasPressedThisFrame)
        {
            placing = true;
            vine = new Vine();
        }

        if (placing && kb.hKey.wasReleasedThisFrame)
        {
            placing = false;
            return;
        }

        if (!placing)
        {
            return;
        }
        
        RaycastHit rayHit;
        Vector3 screenPos = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, -10);
        var ray = Camera.main.ScreenPointToRay(screenPos);
        var result = Physics.Raycast(ray, out rayHit);
        

        if (result)
        {
            vine.AddPoint(rayHit.point, rayHit.normal);
        }
    }
}