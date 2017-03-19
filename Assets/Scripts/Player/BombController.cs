using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {

    // How long until the bomb blows
    [SerializeField]
    private float _bombTime = 10.0f;

    // Max radius the bomb will explode to (should be synced with explosion sprite)
    [SerializeField]
    private float _explosionRadius = 5.0f;

    // The collider attached to this object
    private CircleCollider2D _blastingZone;
    // The timer for the object's states
    private float _boomTimer = 0f;
    // The bomb "shell"
    private GameObject parent;

	// Use this for initialization
	void Start () {
        _blastingZone = gameObject.GetComponent(typeof(CircleCollider2D)) as CircleCollider2D;
        parent = transform.parent.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        _boomTimer += Time.deltaTime;
        if (_boomTimer >= _bombTime)
        {
            _blastingZone.radius += 0.2f;
            if (_blastingZone.radius >= _explosionRadius)
            {
                Destroy(parent);
                Destroy(gameObject);
            }
        }
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
