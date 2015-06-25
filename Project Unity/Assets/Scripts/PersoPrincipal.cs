using UnityEngine;
using System.Collections;

public class PersoPrincipal : MonoBehaviour {
	#region health
	public static int health;
	
	public static int Health 
	{
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
	
	public static int Max_Health
	{
		get { return max_health;}
		set {
			if (value <0)
				max_health = -value;
			else 
				max_health = value;
		}
	}
	#endregion
	
	#region Mana
	public static int mana;
	
	public static int Mana
	{
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
	
	public static int Max_Mana
	{
		get { return max_mana;}
		set {
			if (value <0)
				max_mana = -value;
			else 
				max_mana = value;
		}
	}
	#endregion
	
	#region xp
	private static int current_xp;
	
	public static int Current_Xp
	{
		get { return current_xp;}
		set { 
			if(value >= max_xp)
			{
				current_xp = value;
				levelUp();
			}
			else 
				current_xp = value;
		}
	}
	
	private static int max_xp;
	
	public static int Max_Xp
	{
		get { return max_xp;}
		set { 
			if(value < 0)
				max_xp = -value;
			else 
				max_xp = value;
		}
	}
	#endregion
	public static int level;
	public GameObject player;
	public Transform explosion;
	private int compteur;
	public static int score = 0;

	void Start() {
		Current_Xp = 0;
		max_xp = 100;
		level = 1;
		Max_Health = 100;
		Health = Max_Health;
		Max_Mana = 100;
		Mana = Max_Mana;
		Cursor.visible = false;
		compteur = 0;
		if (PlayerPrefs.HasKey ("Load") && PlayerPrefs.GetInt ("Load") == 1) {
			Mana = PlayerPrefs.GetInt("mana");
			Health = PlayerPrefs.GetInt("health");
			current_xp = PlayerPrefs.GetInt("xp");
			level = PlayerPrefs.GetInt("level");
			score = PlayerPrefs.GetInt("score");
			levelUp();
			if (PlayerPrefs.GetInt("scene") != Application.loadedLevel)
				Application.LoadLevel(PlayerPrefs.GetInt("scene"));
		}
		PlayerPrefs.SetInt ("Load", 1);
	}

	void Update(){
		Cursor.visible = false;
		if (Input.GetKey(KeyCode.Tab))
			Cursor.visible = true;
		if (Input.GetKey (KeyCode.P)) {
			Health --;
				}
		if (Input.GetKey (KeyCode.R)) {
			level = 1;
			Current_Xp = 0;
			levelUp();
		}
		if (Input.GetKey (KeyCode.O)) {
			Mana --;
		}
		if (Input.GetKey (KeyCode.Escape)) {
			Cursor.visible = true;
			Application.LoadLevel(0);
		}

		if (compteur % 50 == 0 && Health >0) {
			Health += 1;
			Mana += 1;
				}
		if (Health <= 0) {
			Instantiate(explosion, player.transform.position,player.transform.rotation);
			Health = 0;
			PlayerPrefs.SetInt("Load", 0);
			Destroy(player);
		}

		compteur++;

		PlayerPrefs.SetFloat ("x", player.transform.position.x);
		PlayerPrefs.SetFloat ("y", player.transform.position.y);
		PlayerPrefs.SetFloat ("z", player.transform.position.z);
		PlayerPrefs.SetInt ("health", Health);
		PlayerPrefs.SetInt ("mana", Mana);
		PlayerPrefs.SetInt ("xp", current_xp);
		PlayerPrefs.SetInt ("level", level);
		PlayerPrefs.SetInt ("score", score);
		PlayerPrefs.SetInt ("scene", Application.loadedLevel);
		PlayerPrefs.Save ();
	}

	void degats(int dmg){
		Debug.Log ("Recu");
		Health -= dmg;

		}
	static void levelUp(){
		Max_Xp = 100 * Mathf.RoundToInt(Mathf.Pow(2,level - 1));
		Debug.Log("lvl");
		if (Current_Xp >= Max_Xp) {
			Debug.Log("ok");
						current_xp = 0;
						level += 1;
				}
		Max_Xp = 100 * Mathf.RoundToInt(Mathf.Pow(2,level - 1));
		max_health = 100 + 10 * (level-1);
		max_mana = 100 + 5 * (level - 1);
		if (max_mana / 4 + mana > Max_Mana)
						mana = Max_Mana;
				else 
						Mana += max_mana / 4;
		if (Max_Health / 4 + Health > Max_Health)
			Health = Max_Health;
		else 
			Health += Max_Health / 4;

	}

	void OnGUI (){
		GUI.Label (new Rect (Screen.width * 5 / 6, 20, 100, 30), "Score : " + score);
		}

}
