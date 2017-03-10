using UnityEngine;
using System.Collections;

	public class Enemy : DamageParent {

	public float hp = 100f;
	public float speed;

	private MainBehaviour mb;
	private Transform thisTr;


	void Start () {
		thisTr = transform;
		mb = Camera.main.GetComponent<MainBehaviour>();

		}


	void Update () {
		thisTr.Translate(Vector3.forward*speed*Time.deltaTime);
		thisTr.Rotate(0, 0.2f, 0);
		}
	public override void SetDamage(float damage, string name)
		{
		hp -= damage;
		if(hp > 0) 
			{

			}
		else
			{
			mb.Scores(name);
			mb.StartCor();
			Destroy(gameObject);
			}
	}

	}
