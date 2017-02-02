using UnityEngine;
using System.Collections;

	public class Enemy : Plane {

	private MainBehaviour mb;


	void Start () {
		thisTr = transform;
		mb = Camera.main.GetComponent<MainBehaviour>();

		}


	void Update () {
		thisTr.Translate(Vector3.forward*speed*Time.deltaTime);
	}
	public override void SetDamage(float damage, string name)
		{
		hp -= damage;
		if(hp > 0) 
			{
			Debug.Log("11");
			}
		else
			{
			mb.Scores(name);
			mb.StartCor();
			Destroy(gameObject);
			}
	}

	}
