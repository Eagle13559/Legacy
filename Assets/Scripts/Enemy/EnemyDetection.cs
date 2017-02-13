﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour {

    /// <summary>
    /// Represents the enemy that is the parent of this detection child
    /// </summary>
    private EnemyController enemy;

    /// <summary>
    /// Keeps track of the last location the player was 'seen'
    /// </summary>
    //private Vector3 playersLastLoc;

    private void Start()
    {
        enemy = GetComponentInParent<EnemyController>();
        //playersLastLoc = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemy.PlayerFound();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemy.PlayerLost();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemy.PlayerFound();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemy.PlayerFound();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemy.PlayerLost();
        }
    }
}