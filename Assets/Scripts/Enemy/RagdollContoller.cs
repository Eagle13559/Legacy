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

    private AudioSource _source;

    [SerializeField]
    private AudioClip _headKick;
    [SerializeField]
    private float _headKickVolume = 1.0f;

    private PlayerController _player;

    private void Start()
    {
        parent = transform.parent.gameObject;
        _source = GetComponent<AudioSource>();

        _player = GameObject.Find("Player").GetComponent(typeof(PlayerController)) as PlayerController;
    } 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerWeapon")
        {
            _source.PlayOneShot(_headKick, _headKickVolume);
            Vector3 direction = gameObject.transform.position - other.transform.position;
            direction.Normalize();
            // Enforce the bomb goes in the direction the player is facing
            if (_player._isFacingRight)
            {
                if (direction.x < 0)
                {
                    direction *= -1;
                }
            }
            else
            {
                if (direction.x > 0)
                {
                    direction *= -1;
                }
            }
            parent.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x, direction.y) * 1000);
        }
        if(other.tag == "Player")
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), other, true);
        }

    }

}
