using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private World world;

    private void Start ()
    {
        world = new World ();
    }

    void Update () {
        GameTime.Instance.Update(Time.deltaTime);
	}

}
