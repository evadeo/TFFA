using UnityEngine;
using System.Collections;

public class AIMulti : MonoBehaviour
{
	
		public int health;
	
		public int Health {
				get { return health; }
				set {
						if (value < 0)
								health = 0;
						else if (value > max_health)
								health = max_health;
						else
				
								health = value;
				}
		}
	
		private int max_health;
	
		public int Max_Health {
				get { return max_health;}
				set {
						if (value < 0)
								max_health = -value;
						else 
								max_health = value;
				}
		}
	
		protected  Vector3 moveDirection;
		public  float speed;
		private  CharacterController controller;
		public GameObject player;
		public Transform explosion;
		private bool dead;
		private GameObject[] players;
		private RaycastHit hit;
		private Vector3 dirToMain;
		private int compteur;
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;

		// Use this for initialization
		void Start ()
		{
				controller = (CharacterController)GetComponent ("CharacterController");
				Max_Health = 20;
				Health = Max_Health;
				dead = false;
				compteur = 20;
		}
	
		// Update is called once per frame
		void Update ()
		{
				try {
						players = GameObject.FindGameObjectsWithTag ("GameController");
			float p1 = Vector3.Distance (player.transform.position, players [0].transform.position);
			float p2 = Vector3.Distance (player.transform.position, players [1].transform.position);
			if (Mathf.Min (p1, p2) == p1)
				dirToMain = players [0].transform.position;
			else 
				dirToMain = players [1].transform.position;
			dirToMain.y = 0;
				} catch {

				}
				
		
				move ();
				if (Health == 0 && !dead) { 
						Instantiate (explosion, player.transform.position, player.transform.rotation);
						Health = 0;
						transform.GetComponent<Animation> ().CrossFade ("die", 0.5f * Time.deltaTime);
						Destroy (player, 1f);
						PersoPrincipal.Current_Xp += 50;
						dead = true;
				}
		}
	
		private void move ()
		{
				if (!dead) {
			
						if (dirToMain.magnitude < 2) {
								moveDirection = dirToMain * 0f; //ennemi s'arrete
								controller.Move (moveDirection * Time.deltaTime); 
								transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (dirToMain), 10f * Time.deltaTime);
								transform.GetComponent<Animation> ().CrossFade ("attack", 7f * Time.deltaTime);
								compteur++;
								if (compteur % 60 == 0) { // à 60 fps on à ici une minute
										compteur = 0;
										PersoPrincipal.Health -= 30;
								}
						} else {
								
								moveDirection = dirToMain * 0.5f;
								transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (dirToMain), 10f * Time.deltaTime);

						}
						transform.GetComponent<Animation> ().CrossFade ("run", 0.5f * Time.deltaTime);
						moveDirection.y -= 4;
						controller.Move (moveDirection * Time.deltaTime);
				}
		}


	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 syncPosition = Vector3.zero;
		if (stream.isWriting)
		{
			syncPosition = player.GetComponent<Rigidbody>().position;
		stream.Serialize(ref syncPosition);
	}
	else
	{
		stream.Serialize(ref syncPosition);
		syncTime = 0f;
		syncDelay = Time.time - lastSynchronizationTime;
		lastSynchronizationTime = Time.time;
			player.GetComponent<Rigidbody>().position = syncStartPosition;
		syncEndPosition = syncPosition;
	}
}
private void SyncedMovement(){
	syncTime += Time.deltaTime;
		player.GetComponent<Rigidbody> ().position = Vector3.Lerp (syncStartPosition, syncEndPosition, syncTime / syncDelay);
}



		private IEnumerator WaitForAnimation ()
		{
				yield return new WaitForSeconds (1);
		}
	
		void degats (int dmg)
		{
				Health -= dmg;
		}
	
		private void OnControllerColliderHit (ControllerColliderHit hit)
		{
				if (hit.transform.name != "Terrain") {
						transform.rotation = Quaternion.Slerp (transform.rotation, transform.rotation * Quaternion.Euler (0, 180, 0), 0.5f * Time.deltaTime);
				}
		}
	
}