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
    public GameObject lightOverworld;

    public float curTime = -1;
    public float LoadLevelTime = 1;

    public bool inDialogue = false;
    public bool princeToSpeak = false;
    public bool princessToSpeak = false;
    public string princeTalk;
    public string princessTalk;
    public GameObject princeBox;
    public GameObject princessBox;


    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
        princeBox.SetActive(false);
        princessBox.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if(flip > 0 && princeToSpeak)
        {
            inDialogue = true;
            princeBox.SetActive(true);
            princeToSpeak = false;
        }
        if(flip < 0 && princessToSpeak)
        {
            inDialogue = true;
            princessBox.SetActive(true);
            princessToSpeak = false;
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            princeBox.SetActive(false);
            princessBox.SetActive(false);
            inDialogue = false;
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
            lightOverworld.SetActive(true);
        if (flip < 0)
            lightOverworld.SetActive(false);
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

    public void Key()
    {

    }
}
