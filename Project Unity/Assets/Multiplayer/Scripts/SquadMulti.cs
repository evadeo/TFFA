using UnityEngine;
using System.Collections;

public class SquadMulti : MonoBehaviour {

	public GameObject spawn1;
	public GameObject spawn2;
	public GameObject spawn3;
	public GameObject spawn4;
	public GameObject squad;
	private GameObject my_squad;
	// Use this for initialization
	void Start () {
		my_squad = squad;
	}
	
	// Update is called once per frame
	void Update () {
		int x = 0;
		if (Time.fixedTime % 15 == 0) {

			x = Random.Range (1, 5);
			switch (x) {
			case 1:
				Network.Instantiate (my_squad, spawn1.transform.position, Quaternion.identity, 1);
				break;
			case 2:
				Network.Instantiate (my_squad, spawn2.transform.position, Quaternion.identity, 1);
				break;
			case 3:
				Network.Instantiate (my_squad, spawn3.transform.position, Quaternion.identity, 1);
				break;
			case 4:
				Network.Instantiate (my_squad, spawn4.transform.position, Quaternion.identity, 1);
				break;
			}
		}
	}
}
