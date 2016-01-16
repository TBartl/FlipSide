using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {
    
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.name == "Player")
            GameManager.instance.curTime = 0;
    }
}
