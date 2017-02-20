using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;
	public enum selectAxis {xAxis, yAxis, zAxis, xzAxis, xyAxis, yzAxis, allAxis}
	public selectAxis useAxis;
	// How long the object should shake for.
	public float shakeDuration = 0f; //будет трясатся, пока это значение не будет =0
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f; //насколько сильно трясется
	public float decreaseFactor = 1.0f; //как долго заканчивается shakeDuration
	
	public Vector3 originalPos;
	
	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}
	
	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void Update()
	{
		if (shakeDuration > 0)
		{
			switch(useAxis)
			{
				case selectAxis.xAxis : 
				camTransform.localPosition = originalPos + new Vector3(Random.Range(-1.0f, 2.0f), 0, 0) * shakeAmount;
				break;
				
				case selectAxis.yAxis : 
				camTransform.localPosition = originalPos + new Vector3(0, Random.Range(-1.0f, 2.0f), 0) * shakeAmount;
				break;
				
				case selectAxis.zAxis : 
				camTransform.localPosition = originalPos + new Vector3(0, 0, Random.Range(-1.0f, 2.0f)) * shakeAmount;
				break;

				case selectAxis.xyAxis : 
				camTransform.localPosition = originalPos + new Vector3(Random.Range(-1.0f, 2.0f), Random.Range(-1.0f, 2.0f), 0) * shakeAmount;
				break;

				case selectAxis.xzAxis : 
				camTransform.localPosition = originalPos + new Vector3(Random.Range(-1.0f, 2.0f), 0, Random.Range(-1.0f, 2.0f)) * shakeAmount;
				break;

				case selectAxis.yzAxis : 
				camTransform.localPosition = originalPos + new Vector3(0, Random.Range(-1.0f, 2.0f), Random.Range(-1.0f, 2.0f)) * shakeAmount;
				break;

				case selectAxis.allAxis : 
					camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
				break;
			}
			
			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
			camTransform.localPosition = originalPos;
		}
	}
}