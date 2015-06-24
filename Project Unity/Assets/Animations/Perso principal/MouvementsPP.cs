using UnityEngine;
using System.Collections;

public class MouvementsPP : MonoBehaviour {

    //Variables publiques
    public float gravity;
    public float speed;
    public float speedRun;
    public float speedJump;
    public float sensiverticale = (float)150.0;
    public AudioClip saut;




    //Variables privées
    private CharacterController controller;
    private Vector3 moveDirection;
    private float deltaTime;
    private Transform characterContent;

    private Vector3 to;
    private Vector3 mouseDirection;
    private float x = (float)0.0;
    private float y = (float)0.0;
    private Vector3 angle;

    private AudioSource source;


	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        moveDirection = new Vector3(0f, 0f, 0f);
        characterContent = transform.Find("Perso Principal FInal 1");

        //On initialise l'angle
        angle = transform.eulerAngles;
        x = angle.y;
        y = angle.x;

        source = GetComponent<AudioSource>();
	}




	// Update is called once per frame
	void Update () {
		deplacement();
	}
	
	void deplacement()
	{
		if (characterContent != null) 
		{
			deltaTime = Time.deltaTime; //Ca evite de prendre de la mémoire pour rien.
			//Le personnage va tourner sur lui-meme en fonction du mouvement de la souris.
			x += Input.GetAxis ("Mouse X") * sensiverticale * (float)0.02;
			Quaternion rotation = Quaternion.Euler (y, x, 0);
			transform.rotation = rotation;
			if (controller.isGrounded) {
				//On prend les différentes valeurs des axes horizontaux et verticaux et on les met dans le moveDirection (vecteur du déplacement du perso)
				moveDirection.Set (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
				moveDirection *= speed;
				moveDirection = transform.TransformDirection (moveDirection); //transforme les axes globaux en axes locaux et vice versa.

				if (Input.GetKey ("left shift")) { //Si le perso court
					moveDirection *= speedRun;
					characterContent.GetComponent<Animation> ().CrossFade ("Anim - Courrir", 0.2f);
					WaitForAnimation ();
                    this.GetComponent<AudioSource>().pitch = 1.7f;
				}
				if (Input.GetButton ("Jump")) { //Si le perso saute. On le met avant le courrir car si on saute, on ne peut ni marcher, ni courrir.
					characterContent.GetComponent<Animation> ().CrossFade ("Animation - Saut", 0.2f);
					WaitForAnimation ();
                    source.PlayOneShot(saut);
					moveDirection.y += speedJump;
				} 
				else if (Input.GetButton ("Horizontal") || Input.GetButton ("Vertical")) {
					characterContent.GetComponent<Animation> ().CrossFade ("Anim - Marche"); //Si il ne court ni ne saute et qu'il bouge, alors il marche
                    WaitForAnimation();
                    if(!this.GetComponent<AudioSource>().isPlaying)
                    {
                        this.GetComponent<AudioSource>().pitch = 1f;
                        this.GetComponent<AudioSource>().Play();
                    }


				}
				if (!Input.anyKey)
                {
                    characterContent.GetComponent<Animation>().CrossFade("Anim - Idle", 0.2f);
                    source.Stop();
                    this.GetComponent<AudioSource>().Stop();
                }
			}
			//Gravity et on bouge le character controller
			moveDirection.y -= gravity * deltaTime; 
			controller.Move (moveDirection * deltaTime);
		}
	}
    //Fonction qui sert à attendre une seconde, soit le temps d'appliquer l'animation. On pourra la changer plus tard avec en paramètres le nombre de secondes
    //si on veut réaliser une animation plus longue.
    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1);
    }


}