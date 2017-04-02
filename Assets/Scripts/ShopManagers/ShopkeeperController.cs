using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private Text BombCount;
    // The text that is already in BombCount. 
    private string BombCountPretext;
    private bool shopping = false;
    public ShoppingCart cart { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private TextBoxManager infoBox;

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

        player = GameObject.Find("Player").GetComponent<PlayerController>();
        _animator = GetComponent<AnimationController2D>();
        cart = new ShoppingCart();
        BombCountPretext = BombCount.text;
    }
	
	// Update is called once per frame
	void Update () {
		if (playerPurchasing)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                RemoveAllFromCart();
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

        BombCount.text = BombCountPretext + cart.GetNumOfSpecificItem(TheBrain.ItemTypes.Bomb);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            infoBox.EnableTextBox();
            playerPurchasing = true; 
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            infoBox.DisableTextBox();
            playerPurchasing = false;
        }
    }

    /// <summary>
    /// Adds the given item to this instances shopping cart temporarily. 
    /// </summary>
    /// <param name="item"></param>
    public void AddToCart(ItemManager item)
    {
        long cashAmount;
        if (player.BankAccount.TryToRemoveFromBank(item.CurrCost, false, out cashAmount) && item.TryToPurchase(cashAmount))
        {
            cart.addItemToCart(item.ItemTag);
        }
    }

    /// <summary>
    /// Removes all items from the cart, the appropriate amount of cash, and adds the total amount of possible items to the brain
    /// for the players cache. 
    /// </summary>
    public void RemoveAllFromCart()
    {
        long givenAmount;
        player.BankAccount.TryToRemoveFromBank(0, true, out givenAmount);

        foreach (ItemContainer item in cart.GetNumOfItems())
        {
            if (item.numOfItems > 0)
            {
                brain.playerItemCounts[item.itemType] += item.numOfItems;
            }
        }

        cart.ClearCart();
    }

}
