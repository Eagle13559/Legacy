using UnityEngine;

public class ButtonObjectToggler : MonoBehaviour {

    public GameObject ObjectToToggle;

	public void Toggle()
    {
        ObjectToToggle.SetActive(!ObjectToToggle.activeSelf);
    }
}
