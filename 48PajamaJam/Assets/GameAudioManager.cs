using UnityEngine;
using System.Collections;

public class GameAudioManager : MonoBehaviour
{
    public static GameAudioManager instance;

    public float maxVolume;
    public AudioSource daySource;
    public AudioSource nightSource;

    public AudioSource respawn;
    public AudioSource portal;
    
    public AudioSource flip;
    public AudioSource flipFailed;

    public AudioSource key;

    bool musicMute = false;

    void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this.gameObject);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!musicMute)
        {
            daySource.volume = maxVolume * (GameManager.instance.Flip0Dark1Light());
            nightSource.volume = maxVolume * (1 - GameManager.instance.Flip0Dark1Light());
        } 

        if (Input.GetKeyDown(KeyCode.M))
        {
            musicMute = !musicMute;
            daySource.volume = 0;
            nightSource.volume = 0;
        }

        

    }

    public void PlayRespawn()
    {
        respawn.Play();
    }

    public void PlayPortal()
    {
        portal.Play();
    }

    public void PlayFlip()
    {
        flip.Play();
    }

    public void PlayFlipFailed()
    {
        flipFailed.Play();
    }

    public void PlayKey()
    {
        key.Play();
    }
    void OnLevelWasLoaded(int level)
    {
        if (level == 15)
            Destroy(this.gameObject);

    }

    

    
}
