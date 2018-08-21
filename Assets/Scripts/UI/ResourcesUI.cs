using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour {

    public GameObject dialogBoxPrefab;

    private Canvas canvas;
    private GameObject goDialogBox;
    private DialogBox dialogBox;

	void Start () {
        canvas = FindObjectOfType<Canvas>();
        goDialogBox = GameObject.Instantiate(dialogBoxPrefab, canvas.transform);
        dialogBox = goDialogBox.GetComponent<DialogBox>();
        dialogBox.Hide();
	}
	
	void Update () {
		
	}

    public void Show()
    {
        dialogBox.SetTitle("Liste des ressources");
        foreach(Resource resource in World.Instance.GetResources()) {
            Debug.Log (resource + " ==> " + World.Instance.NbResource (resource.name));
        }
        dialogBox.Show();
    }
}
