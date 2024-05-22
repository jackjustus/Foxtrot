using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug : MonoBehaviour
{

    public static void Log(string message)
    {
        UnityEngine.Debug.Log(message);
    }

    public static void LogError(string message)
    {
        UnityEngine.Debug.LogError(message);
    }

}
