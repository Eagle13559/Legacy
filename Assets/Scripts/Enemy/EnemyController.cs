using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float speed = 1;
    [SerializeField]
    private float rovingPauseTime;
    [SerializeField]
    private string walkingAnimationName;

    private Vector3 startingPatrolPoint;
    private Vector3 endingPatrolPoint;

    private Vector3 minChasePoint;
    private Vector3 maxChasePoint;

    private bool chasePlayer = false;
    private Vector3 knownPlayerLoc;

    private float timer = 0;
    private bool outgoing = true;

    private AnimationController2D _animator;

    // Use this for initialization
    void Start () {
        _animator = gameObject.GetComponent<AnimationController2D>();

        startingPatrolPoint = this.transform.position;
        endingPatrolPoint = startingPatrolPoint + patrolDelta;

        minChasePoint = startingPatrolPoint - deltaNegChase;
        maxChasePoint = startingPatrolPoint + deltaPosChase;
        

    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime * speed;

        if (timer > rovingPauseTime + 1)
        {
            outgoing = !outgoing;
            timer = 0;
        }

        else if (timer < 1)
        {
            if (outgoing)
            {
                _animator.setFacing("Right");
                _animator.setAnimation(walkingAnimationName);
                this.transform.position = Vector3.Lerp(startingPatrolPoint, endingPatrolPoint, timer);
            }
            else
            {
                _animator.setFacing("Left");
                _animator.setAnimation(walkingAnimationName);
                this.transform.position = Vector3.Lerp(endingPatrolPoint, startingPatrolPoint, timer);
            }
        }

        //else _animator.setAnimation("Spooder_Idle");

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
    public void PlayerFound (Vector3 playerLocation)
    {
        chasePlayer = true;
        knownPlayerLoc = playerLocation;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="playerLocation"></param>
    public void PlayerLost()
    {
        chasePlayer = false;
    }
}
