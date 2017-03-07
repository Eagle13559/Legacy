using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class RagdollContoller : MonoBehaviour {

    [SerializeField]
    private float gravity = -9.8f;

    [SerializeField]
    private float ragdollTime = 5f;

    private CharacterController2D _charController;

    private bool hit = false;

    void Start ()
    {
        _charController = GetComponent<CharacterController2D>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Vector3 forceVelocity = coll.GetComponent<CharacterController2D>().velocity;
            StartCoroutine(Ragdoll(forceVelocity));
        }

    }

    void Update ()
    {
        Vector3 velocity = _charController.velocity;
        velocity.y += gravity * Time.deltaTime;
        if (hit)
        {
            _charController.rigidBody2D.rotation -= Mathf.PI;
        }
        _charController.move(velocity * Time.deltaTime);
    }

    private IEnumerator Ragdoll (Vector3 vectorOfForce)
    {
        Vector3 velocity = _charController.velocity;
        velocity.x += vectorOfForce.x;
        _charController.move(velocity * Time.deltaTime);
        hit = true;

        yield return new WaitForSeconds(ragdollTime);

        velocity.x = 0;
        hit = false;
        _charController.move(velocity * Time.deltaTime);
    }
}
