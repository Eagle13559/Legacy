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

    private CharacterController2D _controller;
    private AnimationController2D _animator;

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
        if (Input.GetAxis("Horizontal") < 0)
        {
            velocity.x = walkSpeed * -1;
            _animator.setFacing("Left");
            _animator.setAnimation("p_run");
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            velocity.x = walkSpeed;
            _animator.setFacing("Right");
            _animator.setAnimation("p_run");
        }
        else
        {
            _animator.setAnimation("p_idle");
        }
        if ( Input.GetAxis("Jump") > 0 && _controller.isGrounded )
        {
            velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
            _animator.setAnimation("p_jump");
        }
        if ( Input.GetKeyDown("left ctrl") )
        {
            _animator.setAnimation("");
            _attackColliderController.setEnabled(true);
        }
        velocity.y += gravity * Time.deltaTime;
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
