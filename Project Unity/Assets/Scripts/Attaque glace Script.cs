using UnityEngine;
using System.Collections;

public class AttaqueglaceScript : MonoBehaviour {

    public Transform icebullet;
    public Transform player;
    public int manacost;
    public string key;





	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(key))
        {
            if (PersoPrincipal.Mana >= manacost)
            {
                float my_y = player.rotation.eulerAngles.y + 180;
                Instantiate(icebullet, GameObject.Find("Bullet_SpawnPoint").transform.position, Quaternion.Euler(0, my_y, 0));
                PersoPrincipal.Mana -= 5;
            }
        }

	}
}
