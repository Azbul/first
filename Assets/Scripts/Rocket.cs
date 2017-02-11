using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {

	[Header("Aim Rocket")]
	public bool aiming;
	public GameObject target;
	public float rotatingSpeed;
	[Space(20)]
	public float speed;
	public float damage;
	public float Ballistic;

	private float ballistic;
	private float timeout;
	private Transform tr;
	private Rigidbody rb;

	void Start () {
		tr = transform;
		rb = GetComponent<Rigidbody>();
		target = GameObject.FindGameObjectWithTag("Respawn");
		}


	void Update () {

		timeout += Time.deltaTime;
		if(timeout>0.1f)
			{
			ballistic = Random.Range(-Ballistic, Ballistic);
			timeout = 0;
			}

		if(!aiming)
			{
		tr.Translate(Vector3.forward*speed*Time.deltaTime);
			}
		rb.AddForce(tr.right*ballistic, ForceMode.Impulse);
		//transform.RotateAround(tr.position, tr.TransformDirection(Vector3.up), Scatter*Time.deltaTime);
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
	void FixedUpdate () {

		if(aiming)	
			{
		Vector3 point2Target = tr.position - target.transform.position;

		point2Target.Normalize ();

		float value = Vector3.Cross (point2Target, transform.forward).y;

		/*
				if (value > 0) {
				
						rb.angularVelocity = rotatingSpeed;
				} else if (value < 0)
						rb.angularVelocity = -rotatingSpeed;
				else
						rotatingSpeed = 0;
*/

		rb.angularVelocity = tr.up * rotatingSpeed * value;


		rb.velocity = transform.forward * speed;
			}

		}
}
