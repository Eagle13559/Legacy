using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowEnemyController : MonoBehaviour {

    /// <summary>
    /// Controls the state of the game level
    /// </summary>
    [SerializeField]
    private GameManager _levelManager;

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

    /// <summary>
    /// Times the current animation cycle
    /// </summary>
    private float _timer = 0.0f;

    private AnimationController2D _animator;

    private Collider2D _collider;

	// Use this for initialization
	void Start () {
        _levelManager.AddKeyEnemy();
        _animator = GetComponent<AnimationController2D>();
        _collider = GetComponent<Collider2D>();
	}

    // runs every frame
    void Update()
    {
        if (isIdle && _invisibleTime < _timer - Time.deltaTime)
        {
            _timer = 0;
            _animator.setAnimation(_idleAnimation);
            isIdle = false;
            _collider.enabled = true;
        }
        else if (!isIdle && _idleTime < _timer - Time.deltaTime)
        {
            _timer = 0;
            _animator.setAnimation(_invinsibleAnimation);
            isIdle = true;
            _collider.enabled = false;
        }

        _timer += Time.deltaTime;
    } 

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "PlayerWeapon")
        {
            Destroy(this.gameObject);
        }
    }

    // When object is destroyed, makes sure to update manager about destruction of this key enemy
    void OnDestroy()
    {
        _levelManager.RemoveKeyEnemy();
    }
}
