using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBubbleController : MonoBehaviour {

    [SerializeField]
    private TextBoxManager[] setOfBubbleTextBoxes;

    private int currTextBoxIndex = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    /// <summary>
    /// Disable all text boxes and resets the current index
    /// </summary>
    public void DisableAllTextBoxes ()
    {
        foreach (TextBoxManager textBox in setOfBubbleTextBoxes)
        {
            textBox.DisableTextBox();
        }
    }

    /// <summary>
    /// Enables the start text box
    /// </summary>
    public void EnableStartTextBoxes ()
    {
        setOfBubbleTextBoxes[0].EnableTextBox();
    }

    /// <summary>
    /// Enables the given text box and disables the rest. 
    /// </summary>
    /// <param name="newTextBox"></param>
    public void EnableSpecificTextBox (TextBoxManager newTextBox)
    {
        foreach (TextBoxManager textBox in setOfBubbleTextBoxes)
        {
            textBox.DisableTextBox();
        }

        newTextBox.EnableTextBox();
    }
}
