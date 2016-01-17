using UnityEngine;
using System.Collections;

public class FieldGenerator : MonoBehaviour {
    public int numCubes;
    public Vector2 sizeBounds;
    public Vector2 radiusBounds;
    public float maxSpin;

    public GameObject spinnyCube;

	// Use this for initialization
	void Start () {
	    for (int index = 0; index < numCubes; index++)
        {
            GameObject newCube = (GameObject)GameObject.Instantiate(spinnyCube);
            newCube.transform.position = Random.onUnitSphere.normalized * Random.Range(radiusBounds.x, radiusBounds.y);
            newCube.GetComponent<RotateOverTime>().rotation = new Vector3(Random.Range(-maxSpin, maxSpin),
                Random.Range(maxSpin, maxSpin), Random.Range(-maxSpin, maxSpin));
            newCube.transform.localScale = Vector3.one * Random.Range(sizeBounds.x, sizeBounds.y);
            newCube.transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            newCube.transform.parent = this.transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
