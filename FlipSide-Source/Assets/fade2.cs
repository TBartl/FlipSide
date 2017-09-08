using UnityEngine;
using System.Collections;

public class fade2 : MonoBehaviour
{
    

    bool fading = false;
    float time = 0;

    Color on = new Color(1f, 1f, 1f, 0.95f);
    Color off = new Color(1f, 1f, 1f, 0f);

    public GameObject g;

    
    // Use this for initialization
    void Start()
    {
        Color startColor = new Color(1f, 1f, 1f, 0);
        Color endColor = new Color(0f, 0f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
            time += Time.deltaTime;
            g.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0,  1 - (time / 1f));
        
    }

}
