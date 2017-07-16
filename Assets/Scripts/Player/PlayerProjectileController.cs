using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileController : MonoBehaviour, Projectile {
    private float timer = 0;
    private float timeActive = 0.5f;
    private CapsuleCollider2D _collider;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;

    // Use this for initialization
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {         
         timer += Time.deltaTime;
         if (timer > timeActive)
         {
            Destroy(this.gameObject);
         }
    }

    public void shootProjectile(Vector2 velocityVector, bool flipAnimation)
    {
        _sprite.flipX = flipAnimation;
        _rigidbody.AddForce(velocityVector);
    }

}
