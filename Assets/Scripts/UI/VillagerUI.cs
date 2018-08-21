using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillagerUI : MonoBehaviour {

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
	
    public void ShowVillager()
    {
        List<Villager> villagers = World.Instance.GetVillagers ();
        if(villagers.Count > 0) {
            dialogBox.SetTitle (villagers[0].VillagerName);
        }
        dialogBox.Show();
    }

    public void ShowResources ()
    {
        dialogBox.SetTitle ("Liste des ressources");
        foreach(Resource resource in World.Instance.GetResources ()) {
            Debug.Log ("\n" + resource.name + ": " + World.Instance.NbResource (resource.name));
        }
        dialogBox.Show ();
    }
}
