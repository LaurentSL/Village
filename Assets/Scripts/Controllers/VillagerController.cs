using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerController : MonoBehaviour {

    public Villager villager;

	void Start () {
        villager = new Villager("Claude", Sex.Male, "01/02/0010", 80);
	}
	
	void Update () {
		
	}
}
