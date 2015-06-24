using UnityEngine;
using System.Collections;

public class PersoMulti : MonoBehaviour {
	public int health;
	public int Health {
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
	public int mana;
	public int Mana {
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
	public Transform fireballbullet;
	public Transform spawn;
	private int compteur;
	private int last = 0;
	void Start() {
		if (PlayerPrefs.HasKey ("Load"))
				PlayerPrefs.SetInt ("Load", 0);
		if (GetComponentInParent<NetworkView>().isMine)
		{

			Max_Health = 100;
			Health = Max_Health;
			Max_Mana = 100;
			if (!NetworkManager.coop)
				Max_Mana = int.MaxValue;
			Mana = Max_Mana;
			compteur = 0;

		}
	}

	void Update(){
		if (GetComponentInParent<NetworkView>().isMine){
			Mana = 100;
			Cursor.visible = false;
			if (Input.GetKey (KeyCode.Tab))
			{
				Cursor.visible = true;
				Debug.developerConsoleVisible = true;
			}
			else
				Debug.developerConsoleVisible = false;
			if (Input.GetKey(KeyCode.A))
				Fireball();
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

	void Fireball(){
		if (Mana >= 10 && compteur >= (last+120)) {
			float my_y = transform.rotation.eulerAngles.y + 180;
			Fireballmouvementmulti f = new Fireballmouvementmulti();
			f.Fbmm (player.transform, 40, spawn, fireballbullet, my_y);
			Mana -= 10;
			last = compteur;
		}
	}
	private IEnumerator CD(){
		yield return new WaitForSeconds (2f);
		}
	void degats(int dmg){

		Health -= dmg;
		Debug.Log ("Recu" + Health);
		}
	void Sort(int nb){
		Mana -= nb;
		}

}
