using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{

	void Start(){
		Cursor.visible = true;
	}
	void OnGUI()
	{
		if (Option.langue == "Anglais") {
			
			if (GUI.Button (new Rect (Screen.width / 2.5f, 2 * Screen.height / 8, Screen.width / 5, Screen.height / 10), " Multiplayer")) {
				Application.LoadLevel (3);
			}
			
			if (GUI.Button (new Rect (Screen.width / 2.5f, 3 * Screen.height / 8, Screen.width / 5, Screen.height / 10), " Load a game")) {
				PlayerPrefs.SetInt ("nbPlayers", 1);
				Application.LoadLevel (1);
				
			}
			
			if (GUI.Button (new Rect (Screen.width / 2.5f, 4 * Screen.height / 8, Screen.width / 5, Screen.height / 10), " Options")) {
				Application.LoadLevel (2);
			}
			
			if (GUI.Button (new Rect (Screen.width / 2.5f, 5 * Screen.height / 8, Screen.width / 5, Screen.height / 10), " Leave the game")) {
				Application.Quit ();
			}
		}
		else {
			
			if (GUI.Button (new Rect (Screen.width / 2.5f, 2 * Screen.height / 8, Screen.width / 5, Screen.height / 10), " Multijoueur")) {
				Application.LoadLevel (3);
			}
			
			if (GUI.Button (new Rect (Screen.width / 2.5f, 3 * Screen.height / 8, Screen.width / 5, Screen.height / 10), " Charger une Partie")) {
				PlayerPrefs.SetInt ("nbPlayers", 1);
				Application.LoadLevel (1);
				
			}
			
			if (GUI.Button (new Rect (Screen.width / 2.5f, 4 * Screen.height / 8, Screen.width / 5, Screen.height / 10), " Options")) {
				Application.LoadLevel (2);
			}
			
			if (GUI.Button (new Rect (Screen.width / 2.5f, 5 * Screen.height / 8, Screen.width / 5, Screen.height / 10), " Quitter le jeu")) {
				Application.Quit ();
			}
		}
	}

}
