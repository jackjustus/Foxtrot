using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    
    public static CameraManager instance;                                   // There will only be one instance of this, so we can make it static
    [SerializeField] private CinemachineVirtualCamera[] allVirtualCameras;  // An array of all the virtual cameras in the scene

    [Header("Y Damping during jump / fall")]                                // These variables allow the camera to move down when the player is falling
    [SerializeField] private float fallTiltAmount = 0.25f;                   // The amount the camera tilt down when the player is falling
    [SerializeField] private float fallTiltTime = 0.35f;                    // The time it takes for the camera to tilt down
    public float fallSpeedYDampingChangeThreshold = -15f;                   // The speed at which the player must be falling for the camera to tilt down
    public bool isLerpingYDamping { get; private set; }                     // A boolean to check if the camera is currently lerping the Y damping
    public bool lerpedFromPlayerFalling { get; set; }                       // A boolean to check if the camera has lerped from the player falling
    private Coroutine lerpTiltCoroutine;                                    // The coroutine that lerps the camera tilt
    private CinemachineVirtualCamera currentCamera;                        // The current active virtual camera
    private CinemachineFramingTransposer framingTransposer;                // The framing transposer of the virtual camera
    private float normTiltAmount;                                          // The normal tilt amount
    private void Awake() {

        // Setting the active object as the instance
        if (instance == null)
            instance = this;

        // Setting the active camera to the currentCamera variable
        foreach(CinemachineVirtualCamera virtualCamera in allVirtualCameras) {
            if (virtualCamera.enabled) {
                // Setting the current camera to the active virtual camera
                currentCamera = virtualCamera;

                // Setting the framing transposer to the current camera's transposer
                framingTransposer = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

            }
        }


    }

    #region Public Methods
    public void SetBoundingRegion(PolygonCollider2D boundingRegion) {
        // Set the bounding region of the camera
        currentCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = boundingRegion;
    }
    #endregion

    #region Lerp the Y Damping

    public void LerpYDamping(bool isPlayerFalling) {
        lerpTiltCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
    }

    private IEnumerator LerpYAction(bool isPlayerFalling) {
        isLerpingYDamping = true;

        // Get starting damping amount
        float startDampAmount = framingTransposer.m_YDamping;
        float endDampAmount = 0f;

        // Dertermine the end damping amount
        if (isPlayerFalling) {
            endDampAmount = fallTiltAmount;
            lerpedFromPlayerFalling = true;
        } else {
            endDampAmount = normTiltAmount;
        }

        // Lerp the pan amount
        float elapsedTime = 0f;
        while (elapsedTime < fallTiltTime) {

            // Increment the time
            elapsedTime += Time.deltaTime;

            // Lerp the damping amount
            float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, elapsedTime / fallTiltTime);
            framingTransposer.m_YDamping = lerpedPanAmount;

            yield return null;
        }
        // After the lerping is done, update the corresponding variable
        isLerpingYDamping = false;
    }
    #endregion
}
