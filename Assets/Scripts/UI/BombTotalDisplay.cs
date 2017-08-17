using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombTotalDisplay : MonoBehaviour {

    /// <summary>
    /// Contact with the total items the player currently has
    /// </summary>
    private TheBrain brain;

    private Text bombDisplay;

    private string pretext;

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

        bombDisplay = GetComponent<Text>();
        pretext = bombDisplay.text;
    }

    // Update is called once per frame
    void Update () {
        bombDisplay.text = brain.playerItemCounts[TheBrain.ItemTypes.Bomb].ToString();
	}
}
