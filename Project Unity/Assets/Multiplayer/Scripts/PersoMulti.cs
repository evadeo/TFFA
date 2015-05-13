using UnityEngine;
using System.Collections;

public class PersoMulti : MonoBehaviour {
	public static int health;
	public static int Health {
		get { return health; }
		set {
			if (value <= 0)
			{
				health = 0;
			}
			else if (value > max_health)
				health = max_health;
			else
				
				health = value;
		}
	}
	private static int max_health;
	public static int Max_Health{
		get { return max_health;}
		set {
			if (value <0)
				max_health = -value;
			else 
				max_health = value;
		}
	}
	public static int mana;
	public static int Mana {
		get { return mana; }
		set {
			if (value < 0)
				mana = 0;
			else if (value > Max_Mana)
				mana = max_mana;
			else
				
				mana = value;
		}
	}
	private static int max_mana;
	public static int Max_Mana{
		get { return max_mana;}
		set {
			if (value <0)
				max_mana = -value;
			else 
				max_mana = value;
		}
	}
	public GameObject player;
	public GameObject parent;
	public Transform explosion;
	private static int compteur;
	
	void Start() {
		if (PlayerPrefs.HasKey ("Load"))
				PlayerPrefs.SetInt ("Load", 0);
		if (GetComponentInParent<NetworkView>().isMine)
		{

			Max_Health = 100;
			Health = Max_Health;
			Max_Mana = 100;
			Mana = Max_Mana;
			compteur = 0;
		}
	}

	void Update(){
		if (GetComponentInParent<NetworkView>().isMine){
			Cursor.visible = false;
			if (Input.GetKey (KeyCode.Tab))
			{
				Cursor.visible = true;
				Debug.developerConsoleVisible = true;
			}
			else
				Debug.developerConsoleVisible = false;
			if (Input.GetKey (KeyCode.P)) 
				Health --;
			if (Input.GetKey (KeyCode.O)) 
				Mana --;
			if (Input.GetKey (KeyCode.Escape)) {
				Cursor.visible = true;
				if (Network.isServer)
				{
					NetworkPlayer[] test= Network.connections;
					foreach (NetworkPlayer np in test) {
						Network.CloseConnection(np,true);
					}
				}
				Network.Destroy(parent);
				Network.Disconnect();
				Application.LoadLevel(0);
			}

			if (compteur % 50 == 0 && Health >0) {
				Health += 1;
				Mana += 1;
			}
			if (Health == 0) {
				Network.Instantiate(explosion, player.transform.position,player.transform.rotation, 0);
				Health = 0;
				Network.Destroy(player);
			}
		compteur++;
		}
	}

	void degats(int dmg){
		Debug.Log ("Recu");
		Health -= dmg;

		}
}
