using UnityEngine;
using System.Collections;

public class RedBox : MonoBehaviour {

    bool hit = false;
    public float targetScale = .001f;
    public float shrinkSpeed = 5f;

    public GameObject box;
    public GameObject particles;
    // Use this for initialization
    void Start () {
        particles.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {

        if (hit)
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(targetScale, targetScale, targetScale), Time.deltaTime * shrinkSpeed);
    }

    public void Hit()
    {
        box.gameObject.SetActive(false);
        particles.SetActive(true);
    }
}
