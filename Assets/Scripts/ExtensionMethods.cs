using System.Collections;
using UnityEngine;

public static class ExtensionMethods
{
    /// <summary>
    /// Rotates the transform along y-axis only so the forward vector point's at target's current
    /// position. 
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="target"></param>
    public static void LookAtTopDown(this Transform transform, Transform target)
    {
        transform.LookAt(target);
        var eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x = 0;
        eulerAngles.z = 0;

        transform.rotation = Quaternion.Euler(eulerAngles);
    }
}
