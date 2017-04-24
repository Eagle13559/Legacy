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
    /// Keeps Track of the total withdrawal amount when removal from bank is called. 
    /// </summary>
    public long TotalWithdrawalAmount { get; private set; }

    /// <summary>
    /// Gets the current total amount of money in this account. 
    /// </summary>
    public long BankAccount { get { return CurrEarnings; } }


    public enum CurrencyTypes { Coin, Jewel, Time };

    // Use this for initialization
    void Start () {
        //CurrEarnings = 0;
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
        Display.text = CurrEarnings.ToString();
    }

    public long ValueOfCurrency (CurrencyTypes currency)
    {
        switch (currency)
        {
            case (CurrencyTypes.Coin):
                return ValueOfCoin;
            case (CurrencyTypes.Jewel):
                return ValueOfJewel;
            case (CurrencyTypes.Time):
                return ValueOfTime;
            default:
                return 0;
        }
    }
    
    /// <summary>
    /// Adds the given amount to the total amount of earnings. Has a max value of a long type. 
    /// </summary>
    /// <param name="amount"></param>
    public bool AddToBank (CurrencyTypes currency, long amount = 1)
    {
        switch (currency)
        {
            case (CurrencyTypes.Coin):
                return TryToAddToBank(ValueOfCoin * amount);
            case (CurrencyTypes.Jewel):
                return TryToAddToBank(ValueOfJewel * amount);
            case (CurrencyTypes.Time):
                return TryToAddToBank(ValueOfTime * amount);
            default:
                return false;
        }
    }

    /// <summary>
    /// Will take the amount given and try to add that value to the current earnings of this Bank Account.
    /// If unsuccessful, the Bank Account will not change. 
    /// </summary>
    /// <param name="amount"></param>
    /// <returns> Whether the adding transaction was successful. </returns>
    public bool AddToBank (long amount)
    {
        return TryToAddToBank(amount);
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
    /// Tries to remove the asking amount from the bank and will return whether this transaction was successful. 
    /// </summary>
    /// <param name="askingAmount"> Amount item to remove is worth </param>
    /// <param name="givenAmount"> the amount left after removing the asking amount </param>
    /// <param name="removal"> Whether to remove the asking amount from the bank or not at the call of function </param>
    /// <returns></returns>
    public bool TryToRemoveFromBank(long askingAmount, bool removal, out long givenAmount)
    {
        if (removal)
        {
            givenAmount = RemoveFromBank(TotalWithdrawalAmount);
            TotalWithdrawalAmount = 0;
        }
        else
        {
            givenAmount = askingAmount;
        }

        if (givenAmount > 0)
        {
            TotalWithdrawalAmount += askingAmount;
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
    private long RemoveFromBank (long amount)
    {
        if (CurrEarnings - amount >= 0)
        {
            CurrEarnings -= amount;
            return amount;
        }

        return 0;

        
    }
}
