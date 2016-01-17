using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

    bool hit = false;
    public float targetScale = .001f;
    public float shrinkSpeed = 5f;
    public float rotateRate = .75f;

    public float init = 4.75f;
    public float range = .25f / 2;
    public float TimeToBob = 1f;
    // Use this for initialization
    float time = 0;
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        Vector3 pos = transform.position;
        pos.y = init + Mathf.Sin(2 * Mathf.PI * Time.timeSinceLevelLoad) * range;
        gameObject.transform.position = pos;
        gameObject.transform.Rotate(0, rotateRate, 0);
        if(hit)
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(targetScale, targetScale, targetScale), Time.deltaTime* shrinkSpeed);
}

    public void Hit()
    {
        hit = true;
        GameManager.instance.Key();
    }
}
