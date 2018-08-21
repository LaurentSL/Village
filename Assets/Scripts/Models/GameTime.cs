using System;
using UnityEngine;

public class GameTime {

    #region Attributes

    /// <summary>
    /// Instance du jeu
    /// </summary>
    private static GameTime _instance;
    public static GameTime Instance {
        get {
            if(_instance == null) {
                _instance = new GameTime();
            }
            return _instance;
        }
        protected set {
            _instance = value;
        }
    }
    /// <summary>
    /// Jour actuel dans calendrier du jeu
    /// </summary>
    public DateTime Day { get; protected set; }
    /// <summary>
    /// Vitesse d'exécution du jeu (0 = pause, 1 = vitesse normale, 2 = vitesse doublée...)
    /// </summary>
    public int speed = 1;

    /// <summary>
    /// Durée en nombre de secondes réelles d'un jour de jeu (à vitesse normale)
    /// </summary>
    private int dayDurationInSeconds = 30;

    #endregion

    public GameTime()
    {
        if(_instance == null) {
            _instance = this;
        }
        Init();
    }

    public void Update(float deltaTime)
    {
        // TODO: Gérer le passage du temps in-game vs le temps réel
        Day = Day.AddDays(1);
    }

    public void Init(string dateInit = "01/01/0001")
    {
        Day = DateTime.Parse(dateInit);
    }

    public int GetAge(DateTime birthday)
    {
        return Mathf.FloorToInt((float)((Day - birthday).TotalDays / 365.2425));
    }

    public override string ToString()
    {
        return Day.ToLongDateString();
    }
}
