using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderController : MonoBehaviour {

    private float timer = 0;
    private float timeActive = 0.4f;
    private CapsuleCollider2D _collider;

    // Use this for initialization
    void Start () {
        _collider = GetComponent<CapsuleCollider2D>();
        _collider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (enabled)
        {
            timer += Time.deltaTime;
            if (timer > timeActive)
            {
                _collider.enabled = false;
                timer = 0;
            }
        }
	}

    public void setEnabled(bool isEnabled) { _collider.enabled = isEnabled; }
}
