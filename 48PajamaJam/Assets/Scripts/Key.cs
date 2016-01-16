using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

    public GameObject turnOn;
    public GameObject turnOff;
    public Vector3 rotationSpeed;

    bool hit = false;
    public float targetScale = .001f;
    public float shrinkSpeed = 5f;
    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        transform.localRotation *=  Quaternion.Euler(Time.deltaTime * rotationSpeed);

        if (hit)
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(targetScale, targetScale, targetScale), Time.deltaTime * shrinkSpeed);
    }
    public void OnTriggerEnter(Collider c)
    {
        hit = true;
        Destroy(this, 5);
        turnOff.SetActive(false);
        turnOn.SetActive(true);
    }
}
