using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager {
    // Singleton
    public static DataManager instance = null;

    /// <summary>
    /// Référence le nom des répertoires où se trouvent les fichiers JSON à charger en fonction des classes demandées.
    /// Exemple : directories[typeof(CharacterData)] = "Character";
    /// </summary>
    public static Dictionary<System.Type, string> directories;

    /// <summary>
    /// Référence pour une classe donnée, les différents objets chargés ainsi que leurs noms.
    /// Exemple : 
    /// tmp = new Dictionary<string, object>();
    /// tmp["dwarf"] = dwarfObject;
    /// tmp["warrior"] = warriorObject;
    /// data[typeof(CharacterData)] = tmp;
    /// </summary>
    public static Dictionary<System.Type, Dictionary<string, object>> data;

    public DataManager ()
    {
        if(instance == null) {
            instance = this;
            directories = new Dictionary<System.Type, string> ();
            data = new Dictionary<System.Type, Dictionary<string, object>> ();
        }
    }

    public override string ToString ()
    {
        string msg = "";
        msg += "attribute 'data':\n";
        foreach(System.Type key1 in data.Keys) {
            foreach(string key2 in data[key1].Keys) {
                string value = data[key1][key2].GetType ().ToString ();
                msg += "--> data[" + key1.ToString () + "][" + key2 + "] = " + value + "\n";
            }
        }
        return msg;
    }

    public void AddDirectory (System.Type type, string directory)
    {
        if(type == null || directory == null || directory == "") {
            return;
        }
        directories[type] = directory;
    }

    /// <summary>
    /// Ajoute un nouveau (key, value) au dictionnaire des données 'data'.
    /// </summary>
    /// <param name="key">Nom de l'objet (Ex: Warrior)</param>
    /// <param name="value">Objet instancié avec les valeurs chargées depuis le fichier.</param>
    public void Add (string key, object value)
    {
        // On vérifie les arguments reçus.
        if(key == null || value == null) {
            Debug.LogWarning ("DataManager.Add(key=" + key + ", value=" + value + "): The key or value is null!");
            return;
        }

        // On vérifie (et on créé si besoin) que le dictionnaire a bien une entrée pour la classe demandée.
        Dictionary<string, object> tmp;
        System.Type key1 = value.GetType ();
        if(data.TryGetValue (key1, out tmp) == false) {
            Debug.Log ("Creation of the '" + key1.ToString () + "' entry in data dictionnary.");
            tmp = new Dictionary<string, object> ();
            data[key1] = tmp;
        }

        // On ajoute (ou on met à jour s'il existe déjà) l'objet dans le dictionnaire.
        if(tmp.ContainsKey (key)) {
            Debug.LogWarning ("Duplicate key '" + key + "', data is updating.");
            data[key1][key] = value;
        }
        else {
            tmp.Add (key, value);
        }
    }

    /// <summary>
    /// Supprime un objet du dictionnaire.
    /// </summary>
    /// <param name="key1">Classe de l'objet</param>
    /// <param name="key2">Nom de l'objet</param>
    public void Remove (System.Type key1, string key2 = null)
    {
        // On vérifie les paramètres.
        if(key1 == null) {
            Debug.LogWarning ("The key1 is null! We do nothing.");
            return;
        }

        if(key2 == null) {
            // We remove all the key 1
            try {
                data.Remove (key1);
            }
            catch(KeyNotFoundException) {
                Debug.LogWarning ("Unknown Key '" + key1 + "', no data removing.");
            }
        }
        else {
            // We remove only the subkey key2
            try {
                data[key1].Remove (key2);
            }
            catch(KeyNotFoundException) {
                Debug.LogWarning ("Unknown Key '" + key1 + "/" + key2 + "', no data removing.");
            }
        }
    }

    /// <summary>
    /// Renvoie l'objet correspondant à la classe T et ayant pour nom key.
    /// </summary>
    /// <typeparam name="T">La classe de l'objet à renvoyer.</typeparam>
    /// <param name="key">Le nom de l'objet à renvoyer.</param>
    /// <returns>L'objet demandé.</returns>
    public T Get<T> (string key) where T : class
    {
        System.Type key1 = typeof (T);
        try {
            // On essaye de renvoyer l'objet depuis le dictionnaire.
            return data[key1][key] as T;
        }
        catch(KeyNotFoundException) {
            // S'il n'existe pas dans le dictionnaire,
            try {
                // On essaye de le charger depuis les fichiers JSON.
                LoadFromFile<T> (key);
                return data[key1][key] as T;
            }
            catch(KeyNotFoundException) {
                Debug.LogWarning ("No data found for keys [" + key1 + "][" + key + "]!");
                return null;
            }
        }
    }

    /// <summary>
    /// Load a data class from json file.
    /// </summary>
    /// <param name="directory">Directory to upload</param>
    /// <typeparam name="T">Class of the loaded objects</typeparam>
    private void LoadFromFile<T> (string fileName = "*") where T : class
    {
        string jsonExtension = ".json";
        string directory = directories[typeof (T)];
        // Nb de données chargées
        int nbLoading = 0;
        // Dossier dans lequel se trouvent les fichier JSON à charger
        string path = System.IO.Path.Combine (Application.streamingAssetsPath, directory);
        //		Debug.Log ("Load: " + directory);
        //		Debug.Log ("Load from '" + path + "'");
        // On récupère les fichiers json présents dans le dossier et ses sous-dossiers
        string[] files = System.IO.Directory.GetFiles (path, fileName + jsonExtension, System.IO.SearchOption.AllDirectories);
        foreach(string file in files) {
            fileName = System.IO.Path.GetFileNameWithoutExtension (file);
            string fileExtension = System.IO.Path.GetExtension (file);
            if(fileExtension == jsonExtension) {
                Debug.Log ("Load from '" + directory + "'/'" + fileName + jsonExtension + "'");
                // On lit le fichier.
                string myLoadedData = System.IO.File.ReadAllText (file);
                // On parse le fichier pour récupérer l'objet.
                T data = JsonUtility.FromJson<T> (myLoadedData) as T;
                // On émet un évènement d'ajout de données
                if(data != null) {
                    Add (fileName, data);
                    nbLoading++;
                }
                else
                    Debug.LogWarning ("No data!");
            }
        }
        Debug.Log ("Nb objects loaded with " + directory + ": " + nbLoading);
    }

}
