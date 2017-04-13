using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour {
    [SerializeField]
    float directionX;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        Vector3 direction = new Vector3(1f, 0f, 0f);
        direction *= directionX;
        collision.transform.position += direction;
    }
}
