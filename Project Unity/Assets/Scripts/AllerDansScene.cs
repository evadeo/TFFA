using UnityEngine;
using System.Collections;

public class AllerDansScene : MonoBehaviour {

    public string nom_scene;



    private bool devant;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "GameController")
        {
            devant = true;
        }
        else
        {
            devant = false;
        }
    }




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (devant)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Application.LoadLevel(nom_scene);
            }
        }

	}
}
