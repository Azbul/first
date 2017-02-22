using UnityEngine;
using System.Collections;

public class Bullet : BulletsParent {

	public float speed;
	public float damage;

	private Transform tr;

	void Start () {
		tr = transform;
	}

	public override void Shellsetting(float sp, float dm)
		{
		damage = dm;
		speed = sp;
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
			planeScript.SetDamage(damage, name);
			}
		Destroy(gameObject);

		}
}
