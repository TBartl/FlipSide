using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

    bool hit = false;
    public float targetScale = .001f;
    public float shrinkSpeed = 5f;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if(hit)
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(targetScale, targetScale, targetScale), Time.deltaTime* shrinkSpeed);
}

    public void Hit()
    {
        hit = true;
        GameManager.instance.Key();
    }
}
