﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class EnemyController : MonoBehaviour {

    /// <summary>
    /// Represents the total distance an enemy can patrol
    /// </summary>
    [SerializeField]
    private Vector3 patrolDelta;

    /// <summary>
    /// Represents the total distance an enemy can chase a player in positive x direction
    /// </summary>
    [SerializeField]
    private Vector3 deltaPosChase;

    /// <summary>
    /// Represents the total distance an enemy can chase a player in negative y direction
    /// </summary>
    [SerializeField]
    private Vector3 deltaNegChase;

    [SerializeField]
    private float walkSpeed = 1;
    [SerializeField]
    private float runSpeed = 1;
    [SerializeField]
    private float rovingPauseTime;
    private float turnTime = 1f;
    private float deathTime = 1f;
    [SerializeField]
    private string anim_walk;
    [SerializeField]
    private string anim_idle;
    [SerializeField]
    private string anim_death;
    [SerializeField]
    private string anim_turn;

    private float gravity = -35.0f;

    private Vector3 startingPatrolPoint;
    private Vector3 endingPatrolPoint;

    private Vector3 minChasePoint;
    private Vector3 maxChasePoint;

    private bool chasePlayer = false;
    //private Vector3 knownPlayerLoc;

    private float timer = 0;
    private bool outgoing = true;
    private bool waiting = false;
    //private bool turning = false;

    private AnimationController2D _animator;
    private CharacterController2D _controller;

    private bool _dying = false;
    private float _deathTimer = 0f;
    private float _deathTime = 1f;

    [SerializeField]
    private AudioClip _kappaDeath;
    [SerializeField]
    private float _kappaDeathVolume = 1.0f;

    private AudioSource _source;


    // Use this for initialization
    void Start () {
        _animator = gameObject.GetComponent<AnimationController2D>();
        _controller = gameObject.GetComponent<CharacterController2D>();

        startingPatrolPoint = this.transform.position;
        endingPatrolPoint = startingPatrolPoint + patrolDelta;

        minChasePoint = startingPatrolPoint - deltaNegChase;
        maxChasePoint = startingPatrolPoint + deltaPosChase;

        turnTime += rovingPauseTime;

        _source = GetComponent<AudioSource>();
        _source.Play();
    }
	
	// Update is called once per frame
	void Update () {
        if (_dying)
        {
            _source.Stop();
            _source.PlayOneShot(_kappaDeath, _kappaDeathVolume);
            _deathTimer += Time.deltaTime;
            if (_deathTimer >= _deathTime)
            {
                Destroy(gameObject);
            }
            return;
        }

        Vector3 velocity = _controller.velocity;
        velocity.x = 0;

        // If the enemy has reached the end of the patrol point
        if (((!waiting) && (!chasePlayer) && (((this.transform.position.x > endingPatrolPoint.x) && outgoing) || ((this.transform.position.x < startingPatrolPoint.x) && !outgoing)))
            || ((!waiting) && chasePlayer && (((this.transform.position.x > maxChasePoint.x) && outgoing) || ((this.transform.position.x < minChasePoint.x) && !outgoing))))
        {
            waiting = true;
            _source.Stop();
        }

        if (timer > turnTime)
        {
            outgoing = !outgoing;
            waiting = false;
            _source.Play();
            timer = 0;
        }
        // This code fires when the character is about to begin moving
        else if (timer > rovingPauseTime)
        {
            _animator.setAnimation(anim_turn);
            timer += Time.deltaTime;
        }
        // This is when the enemy has reached the end of the patrol
        else if (waiting)
        {
            _animator.setAnimation(anim_idle);
            timer += Time.deltaTime;
            
        }
        // Handle all variations of movement
        if (!waiting)
        {
            _animator.setAnimation(anim_walk);
            if (outgoing)
            {
                _animator.setFacing("Right");
                if (!chasePlayer)
                {
                    
                    velocity.x = walkSpeed;
                }
                else
                {
                    //_animator.setAnimation(anim_run);
                    velocity.x = runSpeed;
                }
            }
            else
            {
                _animator.setFacing("Left");
                if (!chasePlayer)
                {
                    //_animator.setAnimation(anim_walk);
                    velocity.x = walkSpeed * -1;
                }
                else
                {
                    //_animator.setAnimation(anim_run);
                    velocity.x = runSpeed * -1;
                }
            }
        }

        if (!_controller.isGrounded) velocity.y += gravity * Time.deltaTime;
        _controller.move(velocity * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Vector3 myPosition = this.transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(myPosition, patrolDelta + myPosition);

        Gizmos.color = Color.blue;
        myPosition.y -= 0.1f;
        Gizmos.DrawLine(myPosition - deltaNegChase, myPosition + deltaPosChase);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="playerLocation"></param>
    public void PlayerFound ()
    {
        chasePlayer = true;
        waiting = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="playerLocation"></param>
    public void PlayerLost()
    {
        chasePlayer = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerWeapon")
        {
            _dying = true;
            _animator.setAnimation("KappaDead");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //Rigidbody2D myBod = gameObject.GetComponent<Rigidbody2D>();
            //myBod.bodyType = RigidbodyType2D.Dynamic;
            //myBod.AddForce(new Vector2(0, 20));
            //myBod.AddTorque(10);
        }
        
    }

    ////void OnCollisionEnter2D(Collision2D coll)
    //{
    //    if (coll.gameObject.tag == "PlayerWeapon")
    //    {
    //        Debug.Log("collision has been detected");
    //        Destroy(gameObject);
    //    }
    //}
}
