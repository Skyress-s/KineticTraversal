using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KineticTags : MonoBehaviour
{
    public bool hookable, wallrunable;
    public bool walkable;

    /// <summary>
    /// check if gameobject has a GOTags on it, if it has return wallrunable bool, if no script is on it defualt return true
    /// </summary>
    /// <param name="g">gameobject to check</param>
    /// <returns></returns>
    public static bool IsWallrunable(GameObject g)
    {
        bool b = g.TryGetComponent(out KineticTags gotags);
        if (!b) return true;

        return gotags.wallrunable;
    }

    public static bool IsHookable(GameObject g)
    {
        bool b = g.TryGetComponent(out KineticTags gotags);
        if (!b) return false;

        return gotags.hookable;
    }

    public static bool IsWalkable(GameObject g)
    {
        bool b = g.TryGetComponent(out KineticTags gotags);
        if (!b) return true;

        return gotags.walkable;
    }
}
