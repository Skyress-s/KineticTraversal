using System.Collections;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NewTestData", menuName = "Data/TestData", order = 1)]
public class TestScriptableObject : ScriptableObject
{

    public float movementSpeed;

    public float jumpForce;

    public float holdSpaceForce;

    public float coyoteMargin;

        
}