using UnityEngine;
using System.Collections;

public class TimedTransform : MonoBehaviour {
	public Vector3 newPos;
	public float speed;
	public float timeDelay;
	float percent;

	void Update() {
		if (timeDelay <= 0) {
			percent += speed * Time.deltaTime;
			transform.position = Vector3.Lerp (transform.position, newPos, percent);
		} else {
			timeDelay -= Time.deltaTime;
		}
	}
}
