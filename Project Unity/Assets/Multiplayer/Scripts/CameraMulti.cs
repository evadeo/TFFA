using UnityEngine;
using System.Collections;

public class CameraMulti : MonoBehaviour {

	private Transform target;
	private Vector3 to;
	private Vector3 mouseDirection;
	public GameObject Cible; 
	public float distance = (float)3.0;
	public float sensihorizontale = (float)250.0;
	public float sensiverticale = (float)150.0;
	public float minverticale = (float)7.0;
	public float maxverticale = (float)80.0;
	Vector3 angle;
	private float x = (float)0.0;
	private float y = (float)0.0;
	
	// Use this for initialization
	void Start () {
		target = Cible.transform;
		angle = transform.eulerAngles;
		x = angle.y;
		y = angle.x;
		if (!GetComponent<NetworkView> ().isMine)
						GetComponent<Camera> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<NetworkView> ().isMine) {
						movement ();
			if (Input.GetKey (KeyCode.Escape)) {
				Cursor.visible = true;
				if (Network.isServer)
				{
					NetworkPlayer[] test= Network.connections;
					foreach (NetworkPlayer np in test) {
						Network.CloseConnection(np,true);
					}
				}
				Network.Destroy(Cible);
				Network.Disconnect();
				Application.LoadLevel(0);
			}
				}
	}
	void movement(){
		if (target) {
			distance -= Input.GetAxis ("Mouse ScrollWheel") * 3;
			if (distance < 3) {
				if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
					distance += Input.GetAxis ("Mouse ScrollWheel") * 3;
				} else {
				}
			} else if (distance > 10) {
				if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
					distance += Input.GetAxis ("Mouse ScrollWheel") * 3;
				} else {
				}
			}
			x += Input.GetAxis ("Mouse X") * sensiverticale * (float)0.02;
			y -= Input.GetAxis ("Mouse Y") * sensihorizontale * (float)0.02;
			
			y = ClampAngle (y, minverticale, maxverticale);
			
			Quaternion rotation = Quaternion.Euler (y, x, 0);
			Vector3 position = rotation * new Vector3 ((float)0.0, (float)2.0, -distance) + target.position;
			
			transform.rotation = rotation;
			transform.position = position;
		}
	}
	static float ClampAngle (float angle, float min, float max) {
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}
}
