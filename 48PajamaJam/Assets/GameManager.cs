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
    public float LoadLevelTime = 3;

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

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

    public void Key()
    {
        GameObject box = GameObject.Find("RedBox");
        //box.SetActive(false);
        box.SendMessage("Hit");
    }

    public void NextLevel()
    {
        int i = Application.loadedLevel + 1;
        Application.LoadLevel(i);
        //end = false;
    }
}
