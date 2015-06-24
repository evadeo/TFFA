using UnityEngine;
using System.Collections;

public class Option : MonoBehaviour {
	public GUIStyle slider;
	public GUIStyle thumb;
	public static string langue;

	void OnGUI()
	{
		if (langue == "Anglais") {
			if (!AudioListener.pause) {
				if (GUI.Button (new Rect (Screen.width / 2.5f, 2*Screen.height / 8, Screen.width / 5, Screen.height / 14), "Sound: on")) 
				{
					AudioListener.pause = true;
				}
			} else {
				if (GUI.Button (new Rect (Screen.width / 2.5f, 2*Screen.height / 8, Screen.width / 5, Screen.height / 14), "Sound = off")) 
				{
					AudioListener.pause = false;
				}
			}
			if (GUI.Button (new Rect (2.5f*Screen.width / 8f, 3*Screen.height / 8, Screen.width / 8, Screen.height / 14), "Decrease quality")) 
			{
				QualitySettings.DecreaseLevel();
			}
			
			GUI.Button (new Rect (3.5f * Screen.width / 8f, 3 * Screen.height / 8, Screen.width / 8, Screen.height / 14), QualitySettings.names[QualitySettings.GetQualityLevel()]);
			
			if (GUI.Button (new Rect (4.5f*Screen.width / 8f, 3*Screen.height / 8, Screen.width / 8, Screen.height / 14), "Increase quality")) 
			{
				QualitySettings.IncreaseLevel();
			}
			if (GUI.Button (new Rect (Screen.width / 2.5f, 4*Screen.height / 8, Screen.width / 5, Screen.height / 14), "Language: English")) 
			{
				langue = "Francais";
			}
			
			if (GUI.Button (new Rect (Screen.width / 2.5f, 5*Screen.height / 8, Screen.width / 5, Screen.height / 14), "Return to menu")) 
			{
				Application.LoadLevel (0);
			}		
			
		}
		else{
			if (!AudioListener.pause) {
				if (GUI.Button (new Rect (Screen.width / 2.5f, 2 * Screen.height / 8, Screen.width / 5, Screen.height / 14), "Son: Activé")) {
					AudioListener.pause = true;
				}
			} else {
				if (GUI.Button (new Rect (Screen.width / 2.5f, 2 * Screen.height / 8, Screen.width / 5, Screen.height / 14), "Son: Désactivé")) {
					AudioListener.pause = false;
				}
			}
			if (GUI.Button (new Rect (2.5f * Screen.width / 8f, 3 * Screen.height / 8, Screen.width / 8, Screen.height / 14), "Reduire la qualité")) {
				QualitySettings.DecreaseLevel ();
			}
			
			GUI.Button (new Rect (3.5f * Screen.width / 8f, 3 * Screen.height / 8, Screen.width / 8, Screen.height / 14), QualitySettings.names [QualitySettings.GetQualityLevel ()]);
			
			if (GUI.Button (new Rect (4.5f * Screen.width / 8f, 3 * Screen.height / 8, Screen.width / 8, Screen.height / 14), "Augmente la qualité")) {
				QualitySettings.IncreaseLevel ();
			}
			if (GUI.Button (new Rect (Screen.width / 2.5f, 4 * Screen.height / 8, Screen.width / 5, Screen.height / 14), "Langue: Français")) {
				langue = "Anglais";
			}
			
			if (GUI.Button (new Rect (Screen.width / 2.5f, 5 * Screen.height / 8, Screen.width / 5, Screen.height / 14), "Retour au menu")) {
				Application.LoadLevel (0);
			}
		}
	}
	
}

