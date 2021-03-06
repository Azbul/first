using UnityEngine;
using System.Collections;

	public class PerlinShake : MonoBehaviour {

	public Transform camTransform;
	public float duration = 0.5f;
	[Tooltip("This value is about 10 times greater than Duration")]
	public float speed = 1.0f;
	public float magnitude = 0.1f;

	public bool test = false;
	Vector3 originalPos;

	// -------------------------------------------------------------------------
		public void PlayShake() {

		StopAllCoroutines();
		StartCoroutine("Shake");
		}
	//--------------------------------------------------------------------------
	void Awake()
		{
		if (camTransform == null)
			{
			camTransform = GetComponent(typeof(Transform)) as Transform;
			}
		}

	void OnEnable()
		{
		originalPos = Camera.main.transform.localPosition;
		}
	// -------------------------------------------------------------------------
		void Update() {
			if (test) {
			test = false;
			PlayShake();
			}
		}

	// -------------------------------------------------------------------------
		IEnumerator Shake() {

		float elapsed = 0.0f;
		float randomStart = Random.Range(-1000.0f, 1000.0f);

			while (elapsed < duration) {

			elapsed += Time.deltaTime;			

			float percentComplete = elapsed / duration;			

			// We want to reduce the shake from full power to 0 starting half way through
			float damper = 1.0f - Mathf.Clamp(2.0f * percentComplete - 1.0f, 0.0f, 1.0f);

			// Calculate the noise parameter starting randomly and going as fast as speed allows
			float alpha = randomStart + speed * percentComplete;

			// map noise to [-1, 1]
			float x = Util.Noise.GetNoise(alpha, 0.0f, 0.0f) * 2.0f - 1.0f;
			float y = Util.Noise.GetNoise(0.0f, alpha, 0.0f) * 2.0f - 1.0f;
			//float z = Util.Noise.GetNoise(0.0f, 0.0f, alpha) * 2.0f - 1.0f;

			x *= magnitude * damper;
			y *= magnitude * damper;
			//z *= magnitude * damper;

			camTransform.localPosition = new Vector3(x, y, originalPos.z);

			yield return null;
			}

			camTransform.localPosition = originalPos;
		}
	}
