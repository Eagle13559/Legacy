using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSlide : MonoBehaviour {

    EdgeCollider2D _edge;
    Vector3 _upVector;

	// Use this for initialization
	void Start () {
        _edge = GetComponent<EdgeCollider2D>();
        Vector2 orientation = _edge.points[1]-_edge.points[0];
        orientation.Normalize();
        _upVector = new Vector3(-1*orientation.y,orientation.x,0);
        _upVector *= 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position += _upVector;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.transform.position += _upVector;

    }
}
