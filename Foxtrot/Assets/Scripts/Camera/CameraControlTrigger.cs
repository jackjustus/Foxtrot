using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;

public class CameraControlTrigger : MonoBehaviour
{
    // This allows for customization of the editor that appears in the inspector for this script
    public CustomInspectorObjects customInspectorObjects;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    private Collider2D collider;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

    private void Start() {
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (customInspectorObjects.panCameraOnContact) {
                // Pan the camera
                CameraManager.instance.PanCameraOnContact(customInspectorObjects.panDistance, customInspectorObjects.panTime, customInspectorObjects.panDirection, false);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {

            Vector2 exitDirection = (collision.transform.position - collider.bounds.center).normalized;
            
            if (customInspectorObjects.swapCameras && customInspectorObjects.cameraOnLeft != null && customInspectorObjects.cameraOnRight != null) {
                // Swap the cameras
                CameraManager.instance.SwapCamera(customInspectorObjects.cameraOnLeft, customInspectorObjects.cameraOnRight, exitDirection);
                
            }

            if (customInspectorObjects.panCameraOnContact) {

                // Pan the camera
                CameraManager.instance.PanCameraOnContact(customInspectorObjects.panDistance, customInspectorObjects.panTime, customInspectorObjects.panDirection, true);
            }
        }
    }

}

[System.Serializable]
public class CustomInspectorObjects {
    public bool swapCameras = false;
    public bool panCameraOnContact = false;

    [HideInInspector] public CinemachineVirtualCamera cameraOnLeft;
    [HideInInspector] public CinemachineVirtualCamera cameraOnRight;

    [HideInInspector] public PanDirection panDirection;
    [HideInInspector] public float panDistance = 3f;
    [HideInInspector] public float panTime = .35f;
}

public enum PanDirection {
    Up,
    Down,
    Left,
    Right
}

[CustomEditor(typeof(CameraControlTrigger))]
public class MyScriptEditor : Editor {

    CameraControlTrigger cameraControlTrigger;

    private void OnEnable() {
        cameraControlTrigger = (CameraControlTrigger)target;
        if (cameraControlTrigger == null) {
            Debug.LogError("CameraControlTrigger is null");
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        // Check if swapCameras is true
        if (cameraControlTrigger.customInspectorObjects.swapCameras) {
            // Display the relevant fields in the inspector
            cameraControlTrigger.customInspectorObjects.cameraOnLeft = EditorGUILayout.ObjectField("Camera On Left", cameraControlTrigger.customInspectorObjects.cameraOnLeft,
                typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;

            cameraControlTrigger.customInspectorObjects.cameraOnRight = EditorGUILayout.ObjectField("Camera On Right", cameraControlTrigger.customInspectorObjects.cameraOnRight, 
                typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;
        }

        if (cameraControlTrigger.customInspectorObjects.panCameraOnContact) {
            cameraControlTrigger.customInspectorObjects.panDirection = (PanDirection)EditorGUILayout.EnumPopup("Pan Direction", 
                cameraControlTrigger.customInspectorObjects.panDirection);

            cameraControlTrigger.customInspectorObjects.panDistance = EditorGUILayout.FloatField("Pan Distance", cameraControlTrigger.customInspectorObjects.panDistance);
            cameraControlTrigger.customInspectorObjects.panTime = EditorGUILayout.FloatField("Pan Time", cameraControlTrigger.customInspectorObjects.panTime);
        }

        if (GUI.changed) {
            EditorUtility.SetDirty(cameraControlTrigger);
        }
    }
}