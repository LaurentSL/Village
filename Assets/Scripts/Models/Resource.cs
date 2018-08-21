using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource {

    public enum Type { Building, Food, Other};

    public string name;
    public string description;
    public string type;
    public List<string> resourcesNeeded = new List<string> ();
    public string buildingNeeded;
    public string consumedBy;

    public override string ToString ()
    {
        string msg = "Resource:";
        msg += " name = " + name;
        msg += ", description = " + description;
        msg += ", type = " + type;
        msg += ", resourcesNeeded = [";
        foreach(string resource in resourcesNeeded) {
            msg += resource + ", ";
        }
        msg += "]";
        msg += ", buildingNeeded = " + buildingNeeded;
        msg += ", consumedBy = " + consumedBy;
        return msg;
    }
}