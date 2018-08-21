using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour {

    public Text titleText;
    public Button quitButton;

	void Start () {
		
	}
	
	void Update () {
		
	}

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetTitle(string title)
    {
        titleText.text = title;
    }
}
