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

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Vector3 forceVelocity = coll.GetComponent<CharacterController2D>().velocity;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(forceVelocity.x, forceVelocity.y) * 200);
        }

    }

}
