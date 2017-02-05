using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed;
	private Transform tr;

	void Start () {
		tr = transform;
	}
	

	void Update () {
		tr.Translate(Vector3.forward*speed*Time.deltaTime);
	}

	void OnCollisionEnter(Collision col)
		{
		
		Plane planeScript = col.gameObject.GetComponent<Plane>();
		if(planeScript)
			{
			string name = tr.name;
			planeScript.SetDamage(10, name);
			}
		Destroy(gameObject);

		}
}
