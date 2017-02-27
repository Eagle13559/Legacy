using UnityEngine;
using System.Collections;
using Prime31;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class PlayerController : MonoBehaviour {

    public GameObject gameCamera;
    public float gravity = -35f;
    public float jumpHeight = 2f;
    public float walkSpeed = 3f;
    public float dashSpeed = 5f;
    public float dashTime = 2f;
    public float dashCooldownTime = 5f;
    public float attackAnimationTimer = 2f;
    private float _dashTimer = 0f;
    private float _attackTimer = 0f;
    private bool _isDashing = false;
    private bool _canDash = true;
    private bool _isAttacking = false;

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

    /// <summary>
    /// Controls the total amount of currency this player has access to. 
    /// </summary>
    [SerializeField]
    private CurrencyController BankAccount;

    /// <summary>
    /// Contains all the pertinent information needed for each relevant object 
    /// controlled by this object. 
    /// </summary>
    private TheBrain brain;

    [SerializeField]
    private AttackColliderController _attackColliderController;

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
        try
        {
            // TODO: Discover a more efficient way to find the below game object
            brain = GameObject.Find("TheBrain").GetComponent<TheBrain>();
        }
        catch
        {
            Debug.Log("The Brain was not found for this object");
            brain = new TheBrain();
            brain.Time = float.PositiveInfinity;
            brain.PlayersMoney = 0;
        }
       
        
        _controller = gameObject.GetComponent<CharacterController2D>();
        _animator = gameObject.GetComponent<AnimationController2D>();
        gameCamera.GetComponent<CameraFollow2D>().startCameraFollow(this.gameObject);

        if (! Regex.IsMatch(   SceneManager.GetActiveScene().name, "Shop") )
        {
            _timer.StartTimer(brain.Time);
        }
        BankAccount.AddToBank( brain.PlayersMoney );
    }
	
	// Update is called once per frame
	void Update () {
        // The order of importance of player actions is as follows:
        //  1. Dashing (will interupt all other actions and override them)
        //  2. Attacking (can attack anytime other than when dashing)
        //  3. Jumping/Falling
        //  4. Walking
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
            _animator.setAnimation(_dashAnimation);
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
                _animator.setAnimation(_idleAnimation);
            }
        }
        // Attacking is next important
        else if (_isAttacking)
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer > attackAnimationTimer)
            {
                _isAttacking = false;
                _attackTimer = 0;
            }
        }
        // Only perform other checks if not dashing or attacking
        else
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                velocity.x = walkSpeed * -1;
                _animator.setFacing("Left");
                if (_controller.isGrounded) _animator.setAnimation(_walkAnimation);
                _isFacingRight = false;
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                velocity.x = walkSpeed;
                _animator.setFacing("Right");
                if (_controller.isGrounded) _animator.setAnimation(_walkAnimation);
                _isFacingRight = true;
            }
            else
            {
                if (_controller.isGrounded) _animator.setAnimation(_idleAnimation);
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
                    _isAttacking = true;
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
        else if (other.tag == "Coin")
        {
            BankAccount.AddToBank(CurrencyController.CurrencyTypes.Coin);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Jewel")
        {
            BankAccount.AddToBank(CurrencyController.CurrencyTypes.Jewel);
            Destroy(other.gameObject);
        }
    }

    /// <summary>
    /// When this game object is destroyed, we need to record the players time and total money
    /// </summary>
    void OnDestroy ()
    {
        brain.Time = _timer.CurrTime;
        brain.PlayersMoney = BankAccount.BankAccount;
    }

}
