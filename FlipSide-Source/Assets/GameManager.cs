using UnityEngine;
using System.Collections;
public enum FlipState
{
    none, goingDown, goingUp
}

public class GameManager : MonoBehaviour {
    float flipPercent = 1;
    public float flip = 1;
    public FlipState flipState = FlipState.none;
    public float flipSpeed;
    public Light lightOverworld;

    public float curTime = -1;
    public float LoadLevelTime = 1;

    public bool inDialogue = false;
    public bool princeToSpeak = false;
    public bool princessToSpeak = false;
    public Texture princeFace;
    public string princeTalk;
    public Texture princessFace;
    public string princessTalk;
    public GameObject princeBox;
    public GameObject princessBox;
    public GUIText princeTextBox;
    public GUIText princessTextBox;
    public GUITexture princeFaceTexture;
    public GUITexture princessFaceTexture;
    public float timeSinceText = -1;

    public AudioSource source;
    public GameObject p;
    public AudioClip key;
    public AudioClip flipEffect;
    public AudioClip portal;
    public AudioClip drop;




    public static GameManager instance;

    void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start () {
        princeBox.SetActive(false);
        princessBox.SetActive(false);
        princeTextBox.text = princeTalk.Replace('\\','\n');
        princessTextBox.text = princessTalk.Replace('\\', '\n');    
        princeFaceTexture.texture = princeFace;
        princessFaceTexture.texture = princessFace;
    }
	
	// Update is called once per frame
	void Update () {
        
        if(Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            Application.LoadLevel(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9)) {
            Application.LoadLevel(Application.loadedLevel + 1);
        }
        if (timeSinceText >= 0)
            timeSinceText += Time.deltaTime;
        if(flip > 0 && princeToSpeak)
        {
            inDialogue = true;
            princeBox.SetActive(true);
            princeToSpeak = false;
            timeSinceText = 0;
        }
        if(flip < 0 && princessToSpeak)
        {
            inDialogue = true;
            princessBox.SetActive(true);
            princessToSpeak = false;
            timeSinceText = 0;
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetButtonDown("Jump") || timeSinceText >= 5)
        {
            princeBox.SetActive(false);
            princessBox.SetActive(false);
            inDialogue = false;
            timeSinceText = -1;
        }

        if (flipState == FlipState.goingUp)
        {
            flipPercent += flipSpeed * Time.deltaTime;
            if (flipPercent > 1)
                flipState = FlipState.none;
        }
        if (flipState == FlipState.goingDown)
        {
            flipPercent -= flipSpeed * Time.deltaTime;
            if (flipPercent < -1)
                flipState = FlipState.none;
        }


        flipPercent = Mathf.Clamp(flipPercent, -1, 1);
        flip = Mathf.Sin(flipPercent * Mathf.PI / 2f);

        if (flip > 0)
            lightOverworld.enabled = true;
        if (flip < 0)
            lightOverworld.enabled = false;
        //flip = flipPercent;

        if (curTime >= 0)
            curTime += Time.deltaTime;
        if (curTime >= LoadLevelTime)
            NextLevel();


    }

    public void TryFlip()
    {

        if (flipState == FlipState.none)
        {
            if (flip < 0)
            {
                flipState = FlipState.goingUp;
            }
            else
            {
                flipState = FlipState.goingDown;
            }

            if (GameAudioManager.instance != null)
                GameAudioManager.instance.PlayFlip();
            
        }
    }

    public bool IsFlipping()
    {
        return flipState != FlipState.none;
    }

    public float Flip0Dark1Light()
    {
        return (flip + 1) / 2f;
    }

    public void ResetFlip()
    {
        flipPercent = 1;
        flip = 1;
    }

    public void NextLevel()
    {
        int i = Application.loadedLevel + 1;
        Application.LoadLevel(i);
        //end = false;
    }
}
