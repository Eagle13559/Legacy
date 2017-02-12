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

    private Vector3 startingPatrolPoint;
    private Vector3 endingPatrolPoint;

    private Vector3 minChasePoint;
    private Vector3 maxChasePoint;

    private float timer = 0;
    private bool outgoing = true;

    // Use this for initialization
    void Start () {
        startingPatrolPoint = this.transform.position;
        endingPatrolPoint = startingPatrolPoint + patrolDelta;

        minChasePoint = startingPatrolPoint - deltaNegChase;
        maxChasePoint = startingPatrolPoint + deltaPosChase;
        //endingPatrolPoint.x += endingPatrolPoint.x + deltaPatrol;
        //minChasePoint.x -= minChasePoint.x - deltaNegChase;
        //maxChasePoint.x += maxChasePoint.x + deltaPosChase;

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
                //_animator.setFacing("Left");
                //_animator.setAnimation("Spooder_Walk");
                this.transform.position = Vector3.Lerp(startingPatrolPoint, endingPatrolPoint, timer);
            }
            else
            {
                //_animator.setFacing("Right");
                //_animator.setAnimation("Spooder_Walk");
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
}
