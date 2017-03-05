using UnityEngine;

public class ButtonObjectToggler : MonoBehaviour {

    public GameObject ObjectToToggle;
    public GameObject ObjectToToggle2;
 

    public void Toggle()
    {
        ObjectToToggle.SetActive(!ObjectToToggle.activeSelf);
        ObjectToToggle2.SetActive(!ObjectToToggle2.activeSelf);


    }
}
