using UnityEngine;
using System.Collections;

public class AI_perso : MonoBehaviour 
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
	public int Max_Health{
		get { return max_health;}
		set {
			if (value <0)
				max_health = -value;
			else 
				max_health = value;
		}
	}
	protected  Vector3 moveDirection;
	public  float speed;
	private  float delayRotation;
	private  float changeRotation;
	private  float newRotation;
	private  CharacterController controller;
	public GameObject player;
	public Transform explosion;
	private bool dead;
	private RaycastHit hit;
	private Vector3 dirToMain;
	private int compteur;
	
	
	// Use this for initialization
	void Start () {
		delayRotation = Random.Range (1, 6);
		controller = (CharacterController)GetComponent ("CharacterController");
		Max_Health = 20;
		Health = Max_Health;
		dead = false;
		compteur = 20;
	}
	
	// Update is called once per frame
	void Update () 
	{
		dirToMain = GameObject.Find ("Perso(Clone)").transform.position - transform.position;
		dirToMain.y = 0;
		
		move ();
		if (Health == 0 && !dead) { 
			GameObject go = Instantiate (explosion, player.transform.position, player.transform.rotation) as GameObject;
			Health = 0;
			transform.GetComponent<Animation>().CrossFade ("die",0.5f*Time.deltaTime);
			Destroy(player,2f);
			PersoPrincipal.Current_Xp += 50;
			dead = true;
			PersoPrincipal.score++;
			Destroy(go,3f);
		}
	}
	
	private void move(){
		if (!dead) {
			
			if (dirToMain.magnitude < 2) {
				moveDirection = dirToMain * 0f; //ennemi s'arrete
				controller.Move (moveDirection * Time.deltaTime); 
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (dirToMain), 10f * Time.deltaTime);
				transform.GetComponent<Animation> ().CrossFade ("attack", 7f * Time.deltaTime);
				compteur++;
				if (compteur % 60 == 0) { // à 60 fps on à ici une minute
					compteur = 0;
					PersoPrincipal.Health -= 10;
				}
			} else {
				if (Time.fixedTime % delayRotation == 0) {
					newRotation = Random.Range (-360, 361);
				}
				if (dirToMain.magnitude < 15) {
					moveDirection = dirToMain * 0.5f;
					transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (dirToMain), 10f * Time.deltaTime);
				} else {
					moveDirection = Vector3.forward * speed;
					moveDirection = transform.TransformDirection (moveDirection);
					try {
						if (Physics.Raycast (transform.Find ("origin").position, transform.forward, out hit)) {
							if (hit.distance < 10) {
								transform.rotation = Quaternion.Slerp (transform.rotation, transform.rotation * Quaternion.Euler (0, 180, 0), 0.5f * Time.deltaTime);
							} else {
							transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, newRotation, 0), 0.5f * Time.deltaTime);
							}
						}
					}
					catch (UnityException){ }
				}
				transform.GetComponent<Animation> ().CrossFade ("C4D Animation Take", 0.5f * Time.deltaTime);
				moveDirection.y -= 4;
				controller.Move (moveDirection * Time.deltaTime);
			}
		}
	}
	private IEnumerator WaitForAnimation()
	{
		yield return new WaitForSeconds(1);
	}
	
	void degats(int dmg){
		Health -= dmg;
	}
	
	private void OnControllerColliderHit(ControllerColliderHit hit){
		if (hit.transform.name != "Terrain") {
			transform.rotation = Quaternion.Slerp (transform.rotation, transform.rotation * Quaternion.Euler (0, 180, 0), 0.5f * Time.deltaTime);
		}
	}
}
