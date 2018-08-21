using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population {

    public List<Villager> Villagers { get; protected set; }

	public Population () {
        Villagers = new List<Villager>();
	}
	
    public void AddVillager(Villager villager)
    {
        if(villager == null) {
            return;
        }
        Villagers.Add(villager);
    }

    public void RemoveVillager(Villager villager)
    {
        if(villager == null) {
            return;
        }
        Villagers.Remove(villager);
    }

    public int GetHappiness()
    {
        if(Villagers.Count == 0) {
            return 100;
        }
        int happiness = 0;
        foreach(Villager villager in Villagers) {
            happiness += villager.Happiness;
        }
        return happiness / Villagers.Count;
    }
}
