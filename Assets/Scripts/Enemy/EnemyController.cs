using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour {

    /// <summary>
    /// Represents the total distance an enemy can patrol
    /// </summary>
    [SerializeField]
    private float deltaPatrol;

    /// <summary>
    /// Represents the total distance an enemy can chase a player in positive x direction
    /// </summary>
    [SerializeField]
    private float deltaPosChase;

    /// <summary>
    /// Represents the total distance an enemy can chase a player in negative y direction
    /// </summary>
    [SerializeField]
    private float deltaNegChase;

    /// <summary>
    /// Contains the original starting location and the max patrol location
    /// </summary>
    private Tuple<Transform, Transform> patrolPoints;

    /// <summary>
    /// Contains the min and max patrol location
    /// </summary>
    private Tuple<Transform, Transform> chasePoints;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
