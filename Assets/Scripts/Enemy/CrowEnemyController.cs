﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowEnemyController : MonoBehaviour {

    /// <summary>
    /// Controls the state of the game level
    /// </summary>
    private GameManager _levelManager;

    [SerializeField]
    private GameObject _crowHead;

    [SerializeField]
    private string _deathAnimation;
    [SerializeField]
    private string _idleAnimation;
    [SerializeField]
    private string _invinsibleAnimation;

    [SerializeField]
    private float _invisibleTime = 0.0f;
    [SerializeField]
    private float _idleTime = 0.0f;
    private bool isIdle = true;

    private float _deathTime = 0.5f;
    private float _deathTimer = 0f;
    private bool _isDying = false;

    [SerializeField]
    private MoneySpawner moneySpawn;

    /// <summary>
    /// Times the current animation cycle
    /// </summary>
    private float _timer = 0.0f;

    private AnimationController2D _animator;

    private Collider2D _collider;

    private AudioSource _source;

    [SerializeField]
    private AudioClip _crowInvincible;
    [SerializeField]
    private float _crowInvincibleVolume = 1.0f;

    [SerializeField]
    private AudioClip _crowDead;
    [SerializeField]
    private float _crowDeadVolume = 1.0f;

    // Use this for initialization
    void Start () {
        _levelManager = GameObject.Find("GameManagers").GetComponent<GameManager>();

        _levelManager.AddKeyEnemy();
        _animator = GetComponent<AnimationController2D>();
        _collider = GetComponent<Collider2D>();
        gameObject.tag = "Crow";

        _source = GetComponent<AudioSource>();
        _source.Play();
    }

    // runs every frame
    void Update()
    {
        if (_isDying)
        {
            _deathTimer += Time.deltaTime;
            if (_deathTimer > _deathTime)
            {
                
                _levelManager.RemoveKeyEnemy();

                Instantiate(_crowHead, transform.position, Quaternion.identity);
                moneySpawn.DropMoney(this.transform.position);

                Destroy(this.gameObject);
            }
        }
        else if (isIdle && _invisibleTime < _timer - Time.deltaTime)
        {
            _timer = 0;
            _animator.setAnimation(_idleAnimation);
            isIdle = false;
            gameObject.tag = "Crow";
            //_collider.enabled = true;
            _source.Play();
        }
        else if (!isIdle && _idleTime < _timer - Time.deltaTime)
        {
            _timer = 0;
            _animator.setAnimation(_invinsibleAnimation);
            isIdle = true;
            gameObject.tag = "Enemy";
            _source.Stop();
            _source.PlayOneShot(_crowInvincible, _crowInvincibleVolume);
            //_collider.enabled = false;
        }

        _timer += Time.deltaTime;
    } 

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "PlayerWeapon")
        {
            if (!isIdle && !_isDying)
            {
                _source.Stop();
                _source.PlayOneShot(_crowDead, _crowDeadVolume);
                isIdle = false;
                _isDying = true;
                _animator.setAnimation(_deathAnimation);
                
            }
        }
    }
}
