using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScaleUp : MonoBehaviour {
    GUIText guiText;
    int initialFontSize;

    void Awake() {
        guiText = this.GetComponent<GUIText>();
        initialFontSize = guiText.fontSize;
    }


    // Use this for initialization
    void Update () {
        guiText.fontSize = initialFontSize * Screen.width / 1280;		
	}
}
