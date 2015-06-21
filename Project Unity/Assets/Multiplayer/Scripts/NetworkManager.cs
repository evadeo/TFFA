using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
	private string typeName = "Duel";
	private string gameName = "Nom du salon";
	private string playerName = "Pseudo";
	public GameObject player;
	public GameObject cam;
	public GameObject spawnpoint;
	private float x;
	private float y;
	private float z;
	public static bool coop = false;
	public GameObject spawnEnnemies;



	private void StartServer ()
	{
		RefreshHostList ();
		if (hostList != null) {
				Network.InitializeServer (1, 25000, !Network.HavePublicAddress ());
				MasterServer.RegisterHost (typeName, gameName);
		} else
				GUI.Label (new Rect (Screen.width / 10, 8 * Screen.height / 10, Screen.width / 7, Screen.height / 8), "Ce nom est déjà utilisé");
	}

	void OnServerInitialized ()
	{
		Debug.Log ("Server Initializied");
		Spawn ();
	}

	void OnPlayerConnected () 
	{
				if (coop)
						Network.Instantiate (spawnEnnemies, new Vector3 (188, 2, 262), Quaternion.identity, 2);
	}
	void OnGUI ()
	{
		if (!Network.isClient && !Network.isServer ) {
			Cursor.visible = true;
			playerName = GUI.TextField (new Rect (Screen.width / 3,Screen.height / 20, Screen.width / 7, 20), playerName);
			gameName = GUI.TextField (new Rect (Screen.width / 10, Screen.height / 4 + Screen.height / 16, Screen.width / 7, 20), gameName);

			if (GUI.Button (new Rect (Screen.width / 10, 3 * Screen.height / 4, Screen.width / 7, Screen.height / 8), "Démarrer un serveur"))
					StartServer ();

			if (!coop)
			{
				if (GUI.Button (new Rect (Screen.width / 10, 4 * Screen.height / 8, Screen.width / 7, Screen.height / 8), "Mode Duel"))
				{
					typeName = "Coop";
						coop = true;
				}
			}
			else 
				if (GUI.Button (new Rect (Screen.width / 10, 4 * Screen.height / 8, Screen.width / 7, Screen.height / 8), "Mode Cooperation"))
			{
				coop = false;
				typeName = "Duel";
			}
			GUI.Label (new Rect (5 * Screen.width / 10, Screen.height / 4 + Screen.height / 16, Screen.width / 7, Screen.height / 8), "Liste des serveurs:");
			if (GUI.Button (new Rect (4.8f * Screen.width / 10, 3.7f * Screen.height / 9 , Screen.width / 7, Screen.height / 8), "Rafraichir la liste"))
				RefreshHostList ();
			if (hostList != null) {
				for (int i = 0; i < hostList.Length; i++) {
					if (GUI.Button (new Rect (7 * Screen.width / 10, Screen.height / 4 + (Screen.height / 7 * i), Screen.width / 7, Screen.height / 8), hostList [i].gameName + "\nJoueurs : " + hostList [i].connectedPlayers + "/" + hostList [i].playerLimit))
					{
						JoinServer (hostList [i]);
						typeName = hostList[i].gameType;
					}
				}
			}
			if (Network.isServer) 
				if (GUI.Button (new Rect (Screen.width / 10 + 2 * Screen.width / 7, 3 * Screen.height / 4, Screen.width / 7, Screen.height / 8), "Arreter le serveur"))
					MasterServer.UnregisterHost ();
		}
	}
	private HostData[] hostList;
	private void RefreshHostList ()
	{
		MasterServer.RequestHostList (typeName);
	}
	void OnMasterServerEvent (MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
				hostList = MasterServer.PollHostList ();
	}

	private void JoinServer (HostData hostData)
	{
		Network.Connect (hostData);

	}
	
	void OnConnectedToServer ()
	{
		Debug.Log ("Server Joined");
		Spawn ();
	}
	void OnPlayerDisconnected(NetworkPlayer player) {
		Debug.Log("Clean up after player " + player);
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}


	// Use this for initialization
	public void Spawn () {
		Destroy (cam);
		x = spawnpoint.transform.position.x + Time.fixedTime % 5; // histoire de pas spawn tout le temps au meme point
		y = spawnpoint.transform.position.y + 0.2f;
		z = spawnpoint.transform.position.z + Time.fixedTime % 5; // histoire de pas spawn tout le temps au meme point
		Network.Instantiate(player, new Vector3(x, y, z), Quaternion.identity, 2);
	}
}