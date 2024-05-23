using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerTransform;

    [Header("Flip Rotation")]
    [SerializeField] private float flipYRotationTime = 0.5f;
    private Coroutine turnCoroutine;
    private PlayerMovement player;
    private bool isFacingRight;

    private void Awake() {
        player = playerTransform.gameObject.GetComponent<PlayerMovement>();

        isFacingRight = player.IsFacingRight;

        
    }

    private void Update() {
        // Move the cameraFollowObject to the player's position
        transform.position = playerTransform.position;

        isFacingRight = player.IsFacingRight;
    }

    public void CallTurn() {
        turnCoroutine = StartCoroutine(FlipYLerp());
    }

    private IEnumerator FlipYLerp() {

        float startRotation = transform.localEulerAngles.y;
        float endRotationAmount = DetermineEndRotation();
        float yRotation = 0f;

        float elapsedTime = 0f;
        while (elapsedTime < flipYRotationTime) {
            elapsedTime += Time.deltaTime;

            // Lerp the rotation
            yRotation = Mathf.Lerp(startRotation, endRotationAmount, elapsedTime / flipYRotationTime);
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            yield return null;
        }
    }

    private float DetermineEndRotation() {
        isFacingRight = !isFacingRight;

        if (isFacingRight) 
            return 0;
        else
            return 180f;
    }
    


}
