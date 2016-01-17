using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour
{

    public GameObject turnOn;
    public GameObject turnOff;
    public Transform meshTransform;
    public Vector3 rotationSpeed;

    bool hit = false;
    public float targetScale = .001f;
    public float shrinkSpeed = 5f;
    public float rotateRate = .75f;
    
    public float range = .25f / 2;
    public float TimeToBob = 1f;
    // Use this for initialization

    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        meshTransform.localPosition = new Vector3(0, Mathf.Sin(2 * Mathf.PI * Time.timeSinceLevelLoad) * range,0);

        meshTransform.localRotation *=  Quaternion.Euler(Time.deltaTime * rotationSpeed);

        if (hit)
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(targetScale, targetScale, targetScale), Time.deltaTime * shrinkSpeed);
    }

    public void OnTriggerEnter(Collider c) {
        hit = true;
        GameManager.instance.PlaySound(0);
        Destroy(this.gameObject, 1);
        turnOff.SetActive(false);
        turnOn.SetActive(true);
    }
}