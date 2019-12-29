using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static bool StageClear = false;
    public static bool GameOver = false;

    public static GameManager instance;

    float time = 0;

    public static int stage = 1;

    private void Awake()
    {
        StageClear = false;
        GameOver = false;
        instance = this;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (SceneManager.GetActiveScene().name == "Title")
        {
            stage = 1;
            if(time > 1)
            {
                if(Input.anyKeyDown)
                {
                    SceneManager.LoadScene("Stage1");
                    time = 0;
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "Clear")
        {
            if (time > 1)
            {
                if (Input.anyKeyDown)
                {
                    effectManager.Instance.StopSound();
                    if (stage == 2)
                        SceneManager.LoadScene("Stage2");
                    else if (stage == 3)
                        SceneManager.LoadScene("Stage3");
                    else
                        SceneManager.LoadScene("Title");
                    time = 0;
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "GameOver")
        {
            if (time > 1)
            {
                if (Input.anyKeyDown)
                {
                    effectManager.Instance.StopSound();
                    SceneManager.LoadScene("Title");
                    time = 0;
                }
            }
        }
        else
        {
            if (Robot.HP <= 0 && !GameOver)
            {
                effectManager.Instance.PlaySound("GameOver");
                GameOver = true;
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    public void Clear()
    {
        if(!StageClear)
        {
            effectManager.Instance.PlaySound("Clear");
            stage++;
            StageClear = true;
            SceneManager.LoadScene("Clear");
        }
    }

}
