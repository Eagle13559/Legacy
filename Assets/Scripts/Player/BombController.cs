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
    private float _animationTimer;
    // The bomb "shell"
    private GameObject parent;

    private AnimationController2D _animator;
    private bool explosionTriggered = false;
    private PlayerController _player;
    private bool _finishedExploding = false;

    // Use this for initialization
    void Start () {
        _blastingZone = gameObject.GetComponent(typeof(CircleCollider2D)) as CircleCollider2D;
        parent = transform.parent.gameObject;
        _animator = parent.GetComponent<AnimationController2D>();
        _animationTimer = _bombTime + 1.5f;
        _player = GameObject.Find("Player").GetComponent(typeof(PlayerController)) as PlayerController;
    }
	
	// Update is called once per frame
	void Update () {
        _boomTimer += Time.deltaTime;
        if (_boomTimer >= _bombTime)
        {
            _animator.setAnimation("BombExplode");
            if (_blastingZone.radius < _explosionRadius)
            {
                _blastingZone.radius += 0.05f;
            }
            else if (!_finishedExploding)
            {
                GetComponent<CircleCollider2D>().enabled = false;
                _finishedExploding = true;
            }
            if (_boomTimer >= _animationTimer)
            {
                Destroy(parent);
                Destroy(gameObject);
                _player._bombsPlaced--;
            }
            if (explosionTriggered == false)
            {
                // Why do this? If the bomb is falling while it explodes,
                //  we don't want it to keep falling. This will have it freeze 
                //  wherever it exploded.
                Rigidbody2D parentPhysics = parent.GetComponent<Rigidbody2D>();
                CircleCollider2D parentCollider = parent.GetComponent<CircleCollider2D>();
                parentCollider.enabled = false;
                parentPhysics.bodyType = RigidbodyType2D.Static;
                explosionTriggered = true;
            }
        }
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            _boomTimer = _bombTime;
        }
        if (other.tag == "PlayerWeapon")
        {
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
    }
}
