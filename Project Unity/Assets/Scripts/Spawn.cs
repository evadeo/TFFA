using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
	public GameObject player;
	public GameObject spawn;
	private float x;
	private float y;
	private float z;
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("Load") && PlayerPrefs.GetInt("Load") == 1) {
			x = PlayerPrefs.GetFloat ("x");
			y = PlayerPrefs.GetFloat ("y");
			z = PlayerPrefs.GetFloat ("z");
		} 
		else {
			x = spawn.transform.position.x;
			y = spawn.transform.position.y + 0.2f;
			z = spawn.transform.position.z;
		}
		if (z < 50) {
			x = spawn.transform.position.x;
			y = spawn.transform.position.y + 0.2f;
			z = spawn.transform.position.z;
				}
		Instantiate (player, new Vector3(x,y,z), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position.z < 50) {
			Destroy(player);
			x = spawn.transform.position.x;
			y = spawn.transform.position.y + 0.2f;
			z = spawn.transform.position.z;
			Instantiate(player, new Vector3(x,y,z), Quaternion.identity);
				}
	}
}
