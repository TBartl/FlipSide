using UnityEngine;
using System.Collections;

public class GameAudioManager : MonoBehaviour
{
    public GameAudioManager instance;

    public float maxVolume;
    public AudioSource daySource;
    public AudioSource nightSource;
    public AudioSource footStep;
    public AudioSource endLevel;
    public AudioSource keyPickup;
    public AudioSource flip;


    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this.gameObject);
    }
	
	// Update is called once per frame
	void Update ()
    {
        daySource.volume = maxVolume * (GameManager.instance.Flip0Dark1Light());
        nightSource.volume = maxVolume * (1 - GameManager.instance.Flip0Dark1Light());

    }
}
