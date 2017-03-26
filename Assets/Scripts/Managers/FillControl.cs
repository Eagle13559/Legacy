using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class FillControl : MonoBehaviour {

    /// <summary>
    /// Will change the fill amount of component
    /// </summary>
    [SerializeField]
    private float FillAmount;

    /// <summary>
    /// UI image to control
    /// </summary>
    private Image UIImage;

    /// <summary>
    /// Creates a fillcontrol for the image given
    /// </summary>
    /// <param name="BarImage"></param>
    public FillControl (Image BarImage)
    {
        UIImage = BarImage;
        UIImage.fillAmount = 1;
    }

    /// <summary>
    /// Displays the current fill amount UI Image for the bar. 
    /// </summary>
    private void DisplayBar()
    {
         UIImage.fillAmount = FillAmount;
    }

    /// <summary>
    /// Takes the changeAmount argument and augments the FillAmount of the image based off that amound
    /// </summary>
    /// <param name="changeAmount"></param>
    public void ChangeBarFill (float changeAmount)
    {
        FillAmount = changeAmount;

        DisplayBar();
    }


}
