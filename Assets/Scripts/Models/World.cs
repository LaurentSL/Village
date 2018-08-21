using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class World {

    private static World _instance;
    public static World Instance {
        get {
            if(_instance == null) {
                _instance = new World ();
            }
            return _instance;
        }
    }

    private DataManager dataManager;

    private Population population;
    private Dictionary<string, Resource> resourcesModel;
    private Dictionary<string, int> resourcesCount;
    private Dictionary<string, Building> buildingsModel;

    public World ()
    {
        _instance = this;

        dataManager = new DataManager ();
        dataManager.AddDirectory (typeof (Resource), "Data/Resources");
        dataManager.AddDirectory (typeof (Building), "Data/Buildings");

        population = new Population ();
        resourcesModel = new Dictionary<string, Resource> ();
        resourcesCount = new Dictionary<string, int> ();
        buildingsModel = new Dictionary<string, Building> ();

        InitForTest ();
    }

    void InitForTest ()
    {
        Villager villager = new Villager ("ClaudE", Sex.Male, "10/02/0003", 95);
        population.AddVillager (villager);

        AddResource ("Berrie");
        AddResource ("Water");
        AddResource ("Bread");

    }

    public void AddResource (string resourceName)
    {
        if(resourcesModel.ContainsKey (resourceName) == false) {
            Resource resource = dataManager.Get<Resource> (resourceName);
            if(resource == null) {
                Debug.LogError ("World.AddResource - Resource '" + resourceName + "' is unknown!");
                return;
            }
            resourcesModel[resourceName] = resource;
            resourcesCount[resourceName] = 0;
        }
        resourcesCount[resourceName]++;
    }

    public void RemoveResource (string resourceName)
    {
        if(resourcesModel.ContainsKey (resourceName) == false) {
            Debug.LogWarning ("World.RemoveResource - Resource '" + resourceName + "' is unknown!");
            return;
        }
        if(resourcesCount.ContainsKey (resourceName) == false || resourcesCount[resourceName] <= 0) {
            Debug.LogWarning ("World.RemoveResource - Number of the resource '" + resourceName + "' is already 0!");
        }
        resourcesCount[resourceName]--;
        if(resourcesCount[resourceName] < 0) { resourcesCount[resourceName] = 0; }
    }

    public List<Resource> GetResources ()
    {
        return resourcesModel.Values.ToList ();
    }

    public Resource GetResource(string resourceName)
    {
        if(resourcesModel.ContainsKey (resourceName) == false) {
            return null;
        }
        return resourcesModel[resourceName];
    }

    public int NbResource (string resource)
    {
        return resourcesCount[resource];
    }

    public List<Villager> GetVillagers ()
    {
        return population.Villagers;
    }
}
