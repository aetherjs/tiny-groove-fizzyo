using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {

    public int breathCount;
    public int setsCount;
    public Text breathCountText;
    public Text setsCountText;

    private void Start()
    {
        breathCount = 10;
        breathCountText.text = breathCount.ToString();
        setsCount = 3;
        setsCountText.text = setsCount.ToString();
    }

    public void PlayGame ()
    {
        PlayerPrefs.SetInt("MaxBreaths", breathCount);
        PlayerPrefs.SetInt("Sets", setsCount);
        PlayerPrefs.Save();
        Initiate.Fade("Game Scene", Color.black, 1);
    }

    public void IncrementbreathCount()
    {
        if (breathCount < 15)
        {
            breathCount++;
            breathCountText.text = breathCount.ToString();
        }
    }

    public void DecrementbreathCount()
    {
        if (breathCount > 1)
        {
            breathCount--;
            breathCountText.text = breathCount.ToString();
        }
    }

    
    public void IncrementSetsCount()
    {
        if (setsCount < 10)
        {
            setsCount++;
            setsCountText.text = setsCount.ToString();
        }
    }

    public void DecrementSetsCount()
    {
        if (setsCount > 1)
        {
            setsCount--;
            setsCountText.text = setsCount.ToString();
        }
    }
}
