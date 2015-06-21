using UnityEngine;
using System.Collections;

public class Fireballmouvementmulti : MonoBehaviour {

	public Rigidbody RB;
	//SphereCollider sc;
	public float Force;
	public float rayon = 3f;
	GameObject[] ennemies;
	bool b;
	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;


	// Use this for initialization
	void Start ()
	{
		//sc = GetComponent<SphereCollider> ();
		RB.AddRelativeForce (Vector3.forward * -Force);
		ennemies = GameObject.FindGameObjectsWithTag ("Character");
		b = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		ennemies = GameObject.FindGameObjectsWithTag ("Character");
	}


	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 syncPosition = Vector3.zero;
		if (stream.isWriting)
		{
			syncPosition = RB.GetComponent<Rigidbody>().position;
			stream.Serialize(ref syncPosition);
		}
		else
		{
			stream.Serialize(ref syncPosition);
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
			RB.GetComponent<Rigidbody>().position = syncStartPosition;
			syncEndPosition = syncPosition;
		}
	}
	private void SyncedMovement(){
		syncTime += Time.deltaTime;
		RB.GetComponent<Rigidbody> ().position = Vector3.Lerp (syncStartPosition, syncEndPosition, syncTime / syncDelay);
	}



	void OnTriggerEnter (Collider collider)
	{
		if (b) {
			Debug.Log ("tag: " + collider.tag);
			if (collider.tag == "Character") {
			}
			RB.AddRelativeForce (Vector3.forward * Force * 0.2f);
			foreach (GameObject go in ennemies) {
				if (go != null) {
					float my_x = go.transform.position.x - RB.transform.position.x;
					float my_y = go.transform.position.y - RB.transform.position.y;
					float my_z = go.transform.position.z - RB.transform.position.z;
					if ( Mathf.Abs(my_x) <= rayon && Mathf.Abs(my_y) <= rayon && Mathf.Abs(my_z) <= rayon)
					{
						Debug.Log ("tag rayon: " + go.tag + " " + (go.name == "Perso(Clone)") + go.name);
						Debug.Log ("oui");
						GameObject objet = GameObject.FindGameObjectWithTag("Joueur");
						objet.SendMessage("degats", 15, SendMessageOptions.DontRequireReceiver);
					}
				}
			}
			b = false;
			Wait(0.3f);
			Network.Destroy (gameObject);
		}
		
	}
	private IEnumerator Wait(float f)
	{
		yield return new WaitForSeconds(f);
	}

}
