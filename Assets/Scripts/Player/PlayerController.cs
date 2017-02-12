using UnityEngine;
using System.Collections;
using Prime31;

public class PlayerController : MonoBehaviour {

    public GameObject gameCamera;
    public float gravity = -35f;
    public float jumpHeight = 2f;
    public float walkSpeed = 3f;
    private CharacterController2D _controller;
    private AnimationController2D _animator;

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
            _animator.setFacing("Right");
            _animator.setAnimation("p_run");
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            velocity.x = walkSpeed;
            _animator.setFacing("Left");
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
        velocity.y += gravity * Time.deltaTime;
        _controller.move( velocity * Time.deltaTime );
	}
}
