using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperController : MonoBehaviour {

    private AnimationController2D _animator;
    private string _idle = "ShopKeeperIdle";
    private string _purchase = "ShopkeeperPurchase";

    private bool playerPurchasing = false;

    private bool purchaseAnimation = false;

    private PlayerController player;

    private TheBrain brain;

    private float timer = 0;
    private float purchaseAnimuLimit =  0.1f;

	// Use this for initialization
	void Start () {
        if (GameObject.Find("TheBrain") != null)
        {
            brain = GameObject.Find("TheBrain").GetComponent<TheBrain>();
        }
        else
        {
            Debug.Log("The Brain was not found for this object");
            brain = new TheBrain();
        }

        _animator = GetComponent<AnimationController2D>();
    }
	
	// Update is called once per frame
	void Update () {
		if (playerPurchasing)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                player.RemoveAllFromCart();
                _animator.setAnimation(_purchase);
                purchaseAnimation = true;
                playerPurchasing = false;
            }
        }
        else if (!purchaseAnimation || timer > purchaseAnimuLimit)
        {
            _animator.setAnimation(_idle);
            purchaseAnimation = false;
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerPurchasing = true;
            if (player == null)
            {
                player = collision.gameObject.GetComponent<PlayerController>();
            }       
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerPurchasing = false;
        }
    }
}
