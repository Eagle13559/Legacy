using UnityEngine;
using System.Collections;
using Prime31;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour {

    public GameObject gameCamera;
    public float gravity = -35f;
    public float jumpHeight = 2f;
    public float walkSpeed = 3f;
    public float dashSpeed = 5f;
    public float dashTime = 2f;
    public float dashCooldownTime = 5f;
    private float _dashTimer = 0f;
    private bool _isDashing = false;
    private bool _canDash = true;

    private CharacterController2D _controller;
    private AnimationController2D _animator;
    private bool _isFacingRight = true;

    /// <summary>
    /// Timer control
    /// </summary>
    [SerializeField]
    private TimerController _timer;

    /// <summary>
    /// Manager of the game session
    /// </summary>
    [SerializeField]
    private GameManager _gameManager;

    /// <summary>
    /// Damage to player's timer
    /// </summary>
    [SerializeField]
    private float timerDamage;

    [SerializeField]
    private AttackColliderController _attackColliderController;

    [SerializeField]
    private string _runAnimation;
    [SerializeField]
    private string _walkAnimation;
    [SerializeField]
    private string _jumpAnimation;
    [SerializeField]
    private string _idleAnimation;
    [SerializeField]
    private string _dashAnimation;
    [SerializeField]
    private string _attackAnimation;

    // Use this for initialization
    void Start () {
        _controller = gameObject.GetComponent<CharacterController2D>();
        _animator = gameObject.GetComponent<AnimationController2D>();
        gameCamera.GetComponent<CameraFollow2D>().startCameraFollow(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 velocity = _controller.velocity;
        velocity.x = 0;
        if (!_canDash)
        {
            if (dashCooldownTime + dashTime < _dashTimer)
            {
                _canDash = true;
                _dashTimer = 0;
            }
            else
                _dashTimer += Time.deltaTime;
        }
        // Dashing overrides all other movements
        if ((Input.GetKeyDown("k") && _canDash) || _isDashing)
        {
            velocity.y = 0;
            if (!_isDashing)
            {
                _isDashing = true;
                _attackColliderController.setEnabled(true);
            }
            if (_isFacingRight)
                velocity.x = dashSpeed;
            else
                velocity.x = dashSpeed * -1;
            _dashTimer += Time.deltaTime;
            if (_dashTimer > dashTime)
            {
                _isDashing = false;
                _canDash = false;
            }
        }
        // Only perform other checks if not dashing
        else
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                velocity.x = walkSpeed * -1;
                _animator.setFacing("Left");
                _animator.setAnimation(_runAnimation);
                _isFacingRight = false;
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                velocity.x = walkSpeed;
                _animator.setFacing("Right");
                _animator.setAnimation(_runAnimation);
                _isFacingRight = true;
            }
            else
            {
                _animator.setAnimation(_idleAnimation);
            }
            if (Input.GetAxis("Jump") > 0 && _controller.isGrounded)
            {
                velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
                _animator.setAnimation(_jumpAnimation);
            }
            if (Input.GetKeyDown("j"))
            {
                if (_controller.isGrounded)
                {
                    _animator.setAnimation(_attackAnimation);
                    _attackColliderController.setEnabled(true);
                }
                else
                {
                    // perform jump attack
                    // stop falling?
                }
            }
            velocity.y += gravity * Time.deltaTime;
        }
        _controller.move( velocity * Time.deltaTime );
	}

    /// <summary>
    /// Player Dies
    /// </summary>
    public void Die()
    {
        _gameManager.GameOver();
    }

    /// <summary>
    /// If player has collided with enemy attack, this event will fire off. 
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            _timer.ReduceTimer(timerDamage);
        }
        else if (other.tag == "KillZ")
        {
            _gameManager.GameOver();
        }
    }

}
