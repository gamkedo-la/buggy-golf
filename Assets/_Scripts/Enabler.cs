using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enabler : MonoBehaviour {

    // The list of things we'll enable on start
    public List<GameObject> enablees;

    // the number of enablees
    int enableeCount;













    // Use this for initialization
    void Start () {
        enableeCount = enablees.Count; // How many enablees are in the list?
        for (int i = 0; i < enablees.Count; i++)
        {
            enablees[i].gameObject.SetActive(true); // Deactivate all enablees in case we forgot to elsewhere
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
