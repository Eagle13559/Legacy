using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyController : MonoBehaviour {

    /// <summary>
    /// Text display for how much money this controller has at each frame
    /// </summary>
    [SerializeField]
    private Text Display;

    /// <summary>
    /// Text that will precede the output amount text
    /// </summary>
    [SerializeField]
    private string PreText;

    /// <summary>
    /// Controls the values of the currency within the game. 
    /// </summary>
    [SerializeField]
    private long ValueOfJewel;
    [SerializeField]
    private long ValueOfTime;
    [SerializeField]
    private long ValueOfCoin;

    /// <summary>
    /// Current total earnings
    /// </summary>
    private long CurrEarnings;

    /// <summary>
    /// Gets the current total amount of money in this account. 
    /// </summary>
    public long BankAccount { get { return CurrEarnings; } }


    public enum CurrencyTypes { Coin, Jewel, Time };

    // Use this for initialization
    void Start () {
        CurrEarnings = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        DisplayCurrency();
	}

    /// <summary>
    /// Will display the current currency amount
    /// </summary>
    private void DisplayCurrency ()
    {
        Display.text = PreText + " " + CurrEarnings;
    }
    
    /// <summary>
    /// Adds the given amount to the total amount of earnings. Has a max value of a long type. 
    /// </summary>
    /// <param name="amount"></param>
    public bool AddToBank (CurrencyTypes currency)
    {
        switch (currency)
        {
            case (CurrencyTypes.Coin):
                return TryToAddToBank(ValueOfCoin);
            case (CurrencyTypes.Jewel):
                return TryToAddToBank(ValueOfJewel);
            case (CurrencyTypes.Time):
                return TryToAddToBank(ValueOfTime);
            default:
                return false;
        }
    }


    private bool TryToAddToBank (long amount)
    {
        if (CurrEarnings + amount < long.MaxValue)
        {
            CurrEarnings = CurrEarnings + amount;
            return true;
        }

        return false;
       
    }

    /// <summary>
    /// Trys to remove the desired amount from the bank and then returns the amount removed. 
    /// The current earnings of this instance can not go into debt. 
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public long RemoveFromBank (long amount)
    {
        if (CurrEarnings - amount > 0)
        {
            CurrEarnings -= amount;
            return amount;
        }
        else
        {
            CurrEarnings = 0;
            return 0;
        }
        
    }
}
