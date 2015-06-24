using UnityEngine;
using System.Collections;

public class FireballMulti : ScriptableObject
{
		public Transform fireballbullet;
		public Transform player;
		public int manacost;
		public string key;
		public Transform spawn;
	private PersoMulti p;
		// Use this for initialization
		void Start ()
		{
			p = new PersoMulti ();
		}

		// Update is called once per frame
		void Update ()
		{

				if (player.GetComponent<NetworkView> ().isMine)
					if (Input.GetKeyUp (key)) {
						if (p.Mana >= manacost) {
								float my_y = player.rotation.eulerAngles.y + 180;
				                
				Fireballmouvementmulti f = ScriptableObject.CreateInstance<Fireballmouvementmulti>();
				f.Fbmm(player, 40, spawn, fireballbullet, my_y);
								p.Mana -= 10;
				WaitForCD();
						}
					}
		}

	private IEnumerator WaitForCD()
	{
		yield return new WaitForSeconds(2.5f);
	}
}
