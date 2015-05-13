using UnityEngine;
using System.Collections;

public class SquadSpawn : MonoBehaviour {
	public GameObject spawn1;
	public GameObject spawn2;
	public GameObject spawn3;
	public GameObject spawn4;
	public GameObject squad;
	public GameObject squad2;
	private GameObject my_squad;
	// Use this for initialization
	void Start () {
		my_squad = squad2;
	}
	
	// Update is called once per frame
	void Update () {
				int x = 0;
				if (Time.fixedTime % (11 - PersoPrincipal.level + Random.Range (-5, 10)) == 0) {
						x = Random.Range (1, 3);
						switch (x) {
						case 1:
								my_squad = squad;
								break;
						case 2:
								my_squad = squad2;
								break;
						}
						x = Random.Range (1, 5);
						switch (x) {
						case 1:
								Instantiate (my_squad, spawn1.transform.position, Quaternion.identity);
								break;
						case 2:
								Instantiate (my_squad, spawn2.transform.position, Quaternion.identity);
								break;
						case 3:
								Instantiate (my_squad, spawn3.transform.position, Quaternion.identity);
								break;
						case 4:
								Instantiate (my_squad, spawn4.transform.position, Quaternion.identity);
								break;
						}
				}
		}
}
