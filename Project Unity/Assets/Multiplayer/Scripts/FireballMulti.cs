using UnityEngine;
using System.Collections;

public class FireballMulti : MonoBehaviour
{
		public Transform fireballbullet;
		public Transform player;
		public int manacost;
		public string key;
		public Transform spawn;

		// Use this for initialization
		void Start ()
		{

		}

		// Update is called once per frame
		void Update ()
		{
				if (player.GetComponent<NetworkView> ().isMine)
					if (Input.GetKeyUp (key)) {
						if (PersoMulti.Mana >= manacost) {
								float my_y = player.rotation.eulerAngles.y + 180;
								Network.Instantiate (fireballbullet, spawn.position, Quaternion.Euler (0, my_y, 0),0);
								PersoMulti.Mana -= 10;
				WaitForCD();
						}
					}
		}

	private IEnumerator WaitForCD()
	{
		yield return new WaitForSeconds(2.5f);
	}
}
