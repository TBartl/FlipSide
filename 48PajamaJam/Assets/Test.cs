using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
    public float flip;
    public Vector3 relativePosition;
    public float worldDifference;
    public Transform cameraTransform;

	// Use this for initialization
	void Start () {
        this.relativePosition = transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {
        flip = Mathf.Clamp(flip, -1, 1);
        transform.localScale = new Vector3(1, flip, 1);
        if (flip <0)
        {
            this.transform.position = relativePosition + Vector3.up * worldDifference;
        } else
        {
            this.transform.position = relativePosition;
        }
        cameraTransform.localRotation = Quaternion.Euler(new Vector3(0, 230, -180 + (flip + 1) / 2f * 180));

    }

 
}
