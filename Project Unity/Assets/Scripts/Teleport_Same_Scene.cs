using UnityEngine;
using System.Collections;

public class Teleport_Same_Scene : MonoBehaviour {
    public Transform cible;
    private bool devant;
    private Collider perso;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "GameController")
        {
            devant = true;
            perso = col;
        }
        else
        {
            devant = false;
        }
    }




    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("bonjour");
            perso.transform.position = cible.position;
        }

    }
}
