using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollContoller : MonoBehaviour {

    [SerializeField]
    private float gravity = -9.8f;
    private Vector2 velocity;

    void Start ()
    {
        velocity = GetComponent<Rigidbody2D>().velocity;
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
            Debug.Log("Hit");

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
            Debug.Log("Hit");

    }

    void Update ()
    {
        velocity.y += gravity * Time.deltaTime;
    }
}
