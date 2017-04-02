using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

    public GameObject textBox;

    public Text theText;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    private bool isTyping = false;
    private bool cancelTyping = false;

    public float typeSpeed;

    public bool isActive;

   // private Timer timer;
    [SerializeField]
    private int bubbleActiveInterval;

    // Use this for initialization
    void Start()
    {

        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }

        //timer = new //timer();
        //timer.Interval = bubbleActiveInterval;
        //timer.AutoReset = false;
        //timer.Elapsed += ((object o, ElapsedEventArgs e) => { if (currentLine < textLines.Length) { cancelTyping = true; //timer.Stop(); } });

        DisableTextBox();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && !cancelTyping)
        {
            if(!isTyping)
            {
                currentLine += 1;

                if (currentLine > endAtLine)
                {
                    DisableTextBox();
                
                }
                else if (currentLine < textLines.Length)
                {
                    StartCoroutine(TextScroll(textLines[currentLine]));
                    //timer.Start();
                }
            }
            else
            {
                cancelTyping = true;
            }
        }

        if(currentLine > endAtLine || cancelTyping)
        {
            //currentLine = 0;
            DisableTextBox();
        }
    }

    /// <summary>
    /// Writes a line of text to the text box
    /// </summary>
    /// <param name="lineofText"></param>
    /// <returns></returns>
    private IEnumerator TextScroll (string lineofText)
    {
        int letter = 0;
        theText.text = "";
        isTyping = true;
        cancelTyping = false;
        while (isTyping && !cancelTyping && (letter < lineofText.Length - 1))
        {
            theText.text += lineofText[letter];
            letter += 1;
            yield return new WaitForSeconds(typeSpeed);
        }
        theText.text = lineofText;
        isTyping = false;
        //cancelTyping = true;
    
    }

    /// <summary>
    /// Turns the text box and starts //timer of when it will turn off. 
    /// </summary>
    public void EnableTextBox()
    {
        if (!textBox.activeSelf)
        {
            textBox.SetActive(true);
            StartCoroutine(TextScroll(textLines[currentLine]));
            //timer.Start();
        }
    }

    /// <summary>
    /// Turns off the text box and stops the //timer. 
    /// </summary>
    public void DisableTextBox()
    {

        textBox.SetActive(false);
        isTyping = false;
        cancelTyping = true;
        currentLine = 0;
        //timer.Stop();
    }

  
}
