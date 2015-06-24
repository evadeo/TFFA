using UnityEngine;
using System.Collections;

public class IceAttackScript : MonoBehaviour {
	public Transform fireballbullet;
	public Transform player;
	public Rigidbody fumee;
	public int manacost;
	public string key;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyUp (key)) 
		{
			if (PersoPrincipal.Mana >= manacost)
			{
				float my_y = player.rotation.eulerAngles.y + 180;
				Instantiate(fireballbullet,GameObject.Find("Bullet_SpawnPoint").transform.position,Quaternion.Euler(0,my_y,0));
				//SphereCollider sc = bulletfire.GetComponent<SphereCollider> ();
				//if (sc.isTrigger)
				//	{
				//	Rigidbody smoke;
				//	smoke = Instantiate(fumee,fireballbullet.transform.position,Quaternion.Euler(0,0,0)) as Rigidbody ;
				//}
				PersoPrincipal.Mana -= 3;
			}
		}
		
	}
}
