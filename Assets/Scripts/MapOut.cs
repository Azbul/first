using UnityEngine;
using System.Collections;

public class MapOut : MonoBehaviour 
	{
	void OnTriggerEnter(Collider col)
		{
		DamageParent plane = col.gameObject.GetComponent<DamageParent>();
		if (plane)
			{
			plane.mapOut();
			}
		}
	}




