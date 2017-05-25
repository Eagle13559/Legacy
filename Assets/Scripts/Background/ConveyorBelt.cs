using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class ConveyorBelt : MonoBehaviour {
    [SerializeField]
    private float directionX = 1;

    [SerializeField]
    private AudioSource soundFX;

	// Use this for initialization
	void Start () {
        if (soundFX != null)
        {
            soundFX.enabled = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        Vector2 thrust;
        thrust = new Vector3(50f, 0f);
        thrust.x *= directionX;
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(thrust);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        Vector3 thrust;
        thrust = new Vector3(10f, 0f, 0f);
        thrust.x *= directionX * Time.deltaTime;
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<CharacterController2D>().move(thrust);
            //collision.transform.position += thrust;
        }

    }
}
