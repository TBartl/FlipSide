using UnityEngine;
using System.Collections;

public class TimedScale : MonoBehaviour {

	public Vector3 newScale;
	public float speed;
	public float timeDelay;
	float percent;
	
	// Update is called once per frame
	void Update () {
		if (timeDelay <= 0) {
			percent += speed * Time.deltaTime;
			transform.localScale = Vector3.Lerp (transform.localScale, newScale, percent);
		} else {
			timeDelay -= Time.deltaTime;
		}
	}
}
