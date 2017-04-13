using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopkeeperController : MonoBehaviour
{

    private AnimationController2D _animator;
    private string _idle = "ShopKeeperIdle";
    private string _purchase = "ShopkeeperPurchase";

    /// <summary>
    /// Shopkeeper is either in two states, purchasing or not. 
    /// </summary>
    private bool playerPurchasing = false;
    private bool purchaseAnimation = false;

    private PlayerController player;

    private TheBrain brain;

    /// <summary>
    /// Controls the switch from one animation to another. 
    /// </summary>
    private float timer = 0;
    private float purchaseAnimuLimit = 1;

    /// <summary>
    /// Reference to the text field to show bomb count
    /// </summary>
    [SerializeField]
    private Text BombCount;

    // The text that is already in BombCount. 
    private string BombCountPretext;

    /// <summary>
    /// Keeps track of the items that the player wishes to purchase. 
    /// </summary>
    public ShoppingCart cart { get; private set; }

    /// <summary>
    /// The info shown to player for this object. 
    /// </summary>
    [SerializeField]
    private TextBubbleController infoBox;

    // Use this for initialization
    void Start()
    {
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
    void Update()
    {
        // Play purchase animation
        if (playerPurchasing)
        {
            RemoveAllFromCart();
            _animator.setAnimation(_purchase);
            purchaseAnimation = true;
            playerPurchasing = false;
        }
        // Play idle animation
        else if (!purchaseAnimation || timer > purchaseAnimuLimit)
        {
            
            _animator.setAnimation(_idle);
            purchaseAnimation = false;
            playerPurchasing = false;
            timer = 0;
        }
        // increase timer for switch.
        else
        {
            timer += Time.deltaTime;
        }

        BombCount.text = BombCountPretext + cart.GetNumOfSpecificItem(TheBrain.ItemTypes.Bomb);
        player.ShowTimer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            infoBox.EnableStartTextBoxes();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            infoBox.DisableAllTextBoxes();
            playerPurchasing = false;
            _animator.setAnimation(_idle);
        }
    }

    /// <summary>
    /// Adds the given item to this instances shopping cart temporarily. 
    /// </summary>
    /// <param name="item"></param>
    public void AddToCart(ItemManager item)
    {
        long cashAmount;
        // Checks if player can remove the required amount to purchase item, and if the item is in stock.
        // and then will add the item to the cart if both conditions are satisfied. 
        if (player.BankAccount.TryToRemoveFromBank(item.CurrCost, false, out cashAmount) 
                && item.TryToPurchase(cashAmount))
        {
            if (item.ItemTag != TheBrain.ItemTypes.None)
            {
                cart.addItemToCart(item.ItemTag);
            }
            else
            {
                cart.addIncenseToCart(item.IncenseTag);
            }
                
        }
    }

    /// <summary>
    /// Removes all items from the cart, the appropriate amount of cash, and adds the total amount of possible items to the brain
    /// for the players cache. 
    /// </summary>
    public void RemoveAllFromCart()
    {
        long givenAmount;
        playerPurchasing = true;

        // Remove money from bank
        player.BankAccount.TryToRemoveFromBank(0, true, out givenAmount);

        // Add items to permanent inventory of brain to be used in next level
        foreach (ItemContainer item in cart.GetNumOfItems())
        {
            if (item.numOfItems > 0)
            {
                brain.playerItemCounts[item.itemType] += item.numOfItems;
            }
        }

        // Add items to permanent inventory of brain to be used in next level
        foreach (IncenseContainer item in cart.GetNumOfIncense())
        {
            if (item.numOfItems > 0)
            {
                brain.playerIncenseCounts[item.itemType]++;
            }
        }

        // clear the contents of the cart containing items to purchase. 
        cart.ClearCart();
    }

    

}
