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
    private PlayerMovement1 player;
    private bool isFacingRight;

    private void Awake() {
        player = playerTransform.gameObject.GetComponent<PlayerMovement1>();

        isFacingRight = player.IsFacingRight;
    }

    private void Update() {
        
    }
    


}
