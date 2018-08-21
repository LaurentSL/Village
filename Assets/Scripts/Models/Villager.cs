using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sex { Male, Female };

public class Villager {

    public string VillagerName { get; protected set; }
    public Sex Sex { get; protected set; }
    public DateTime Birthday { get; protected set; }
    public int Happiness { get; protected set; }

    public Villager(string villagerName="Unknown", Sex sex = Sex.Male, string birthday = "01/01/0001", int happiness=100)
    {
        VillagerName = villagerName;
        Sex = sex;
        Birthday = DateTime.Parse(birthday);
        Happiness = happiness;
    }

}
