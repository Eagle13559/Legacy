using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    /* The PlayerControl class has been created without much further thought for the purpose
     *  of showing the workings of the CameraManager in regards to following a moving/playercontrolled object.
     */

    public float walkSpeed;
    private new Rigidbody2D rigidbody;

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        rigidbody.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * walkSpeed, 2f), Mathf.Lerp(0, Input.GetAxis("Vertical") * walkSpeed, 2f));
    }
}