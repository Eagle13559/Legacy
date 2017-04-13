using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class RagdollContoller : MonoBehaviour {

    [SerializeField]
    private float gravity = -9.8f;

    [SerializeField]
    private float ragdollTime = 5f;

    private bool hit = false;

    // The bomb "shell"
    private GameObject parent;

    private void Start()
    {
        parent = transform.parent.gameObject;
    } 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerWeapon")
        {
            Vector3 direction = gameObject.transform.position - other.transform.position;
            direction.Normalize();
            // Enforce the bomb goes in the direction the player is facing
            parent.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x, direction.y) * 1000);
        }

    }

}
