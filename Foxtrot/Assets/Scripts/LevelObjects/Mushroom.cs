using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] private float jumpForce = 20f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject player = collider.gameObject;


        collider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }
}
