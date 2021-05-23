using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentalTags : MonoBehaviour
{
    public static bool hookable, wallrunable;

    ///// <summary>
    ///// Method to check if the game object has a GOTags with active tags acociated with it, if
    ///// not, retrun true
    ///// </summary>
    ///// <param name="g">targetGameObject</param>
    ///// <param name="boolToCheck">use GlobalTags.(the bool you want to test)</param>
    ///// <returns></returns>
    //public static bool CheckTag(GameObject g, bool boolToCheck)
    //{
    //    bool b = g.TryGetComponent(out GOTags tags);
    //    if (!b) 
    //    { 
    //        //Debug.Log("GameObject does not have a GOTags compoment. Returning boolean TRUE");
    //        return true;
    //    }
        
    //    if (boolToCheck.ToString() == ExperimentalTags.hookable.ToString() && tags.hookable)
    //    {
    //        return true;
    //    }
    //    else if (boolToCheck.ToString() == ExperimentalTags.wallrunable.ToString() && tags.wallrunable)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
    

    /// <summary>
    /// check if gameobject has a GOTags on it, if it has return wallrunable bool, if no script is on it defualt return true
    /// </summary>
    /// <param name="g">gameobject to check</param>
    /// <returns></returns>
    public static bool IsWallrunable(GameObject g)
    {
        bool b = g.TryGetComponent(out GOTags gotags);
        if (!b) return true;

        return gotags.wallrunable;
    }

    public static bool IsHookable(GameObject g)
    {
        bool b = g.TryGetComponent(out GOTags gotags);
        if (!b) return false;

        return gotags.hookable;
    }

}

//public class GlobalTags
//{
//    public static bool hookable, wallrunable;
//}

