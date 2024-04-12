using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Text scoreText;
    public Text timeText;
    public Text xText;
    public Text yText;
    public Text gunsOnText;
    public Text busrtText;
    public Text gunsHeatText;
    public Text airBreakText;
    public Text lockedOnText;
    public Text warningText;
    public Text incommingText;
    public Text radarJamText;
    public Text signalLostText;
    public Text restartText;

    Color greenOn = new Color(0, 1, 0, 1);
    Color greenOff = new Color(0, 1, 0, 5 / 255f);
    Color yellowOn = new Color(1, 1, 0, 1);
    Color yellowOff = new Color(1, 1, 0, 5 / 255f);
    Color redOn = new Color (1, 0, 0, 1);
    Color redOff = new Color(1, 0, 0, 5 / 255f);

    float time;
    int deg;
    int sec;
    int min;

    public Transform mCamTf;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (GameManager.instance.systemOnline)
        {
            UITimer();
        }
    }

    void UITimer()
    {
        if (GameManager.instance.alive)
        {
            time = GameManager.instance.time - 1.5f;
            min = Convert.ToInt32(time / 60);
            sec = Convert.ToInt32(time % 60);
            deg = Convert.ToInt32((time % 1) * 60);
            timeText.text = string.Format("{0:D2}", min) + ":" + string.Format("{0:D2}", sec) + ":" + string.Format("{0:D2}", deg);
        }
    }

    public void DashBoard()
    {
        xText.GetComponent<Text>().color = greenOn;
        yText.GetComponent<Text>().color = greenOn;
        xText.text = (GameManager.instance.playerX * 100).ToString("0");
        yText.text = (GameManager.instance.playerY * 100 + 500).ToString("0");
    }

    public void DashOff()
    {
        xText.GetComponent<Text>().color = greenOff;
        yText.GetComponent<Text>().color = greenOff;
    }

    public void ScoreText(int score)
    {
        scoreText.text = "Kill :" + score;
    }

    public void GunsOn()
    {
        gunsOnText.GetComponent<Text>().color = greenOn;
    }
    public void GunsOff()
    {
        gunsOnText.GetComponent<Text>().color = greenOff;
    }

    public void BurstOn()
    {
        busrtText.GetComponent<Text>().color = greenOn;
    }

    public void BurstOff()
    {
        busrtText.GetComponent<Text>().color = greenOff;
    }

    public void GunsHeat(float gunsHeat, float heatRate)
    {
        if (GameManager.instance.gunsOn)
        {
            if (gunsHeat < 45)
            {
                gunsHeatText.GetComponent<Text>().color = new Color(gunsHeat / 45, 1, 0);
            }
            else if (45 <= gunsHeat && gunsHeat < 90)
            {
                gunsHeatText.GetComponent<Text>().color = new Color(1, (90 - gunsHeat) / 45, 0);
            }
            else
            {
                gunsHeatText.GetComponent<Text>().color = new Color(1, 0, 0);
            }


            int gunsHeatInt = Convert.ToInt32((float)gunsHeat);
            gunsHeatText.text = "Heat: " + (gunsHeatInt).ToString("D2") + "%";
        }
    }

    public void GunsHeatOff()
    {
        gunsHeatText.GetComponent<Text>().color = greenOff;
    }

    public void AirBreakOn()
    {
        airBreakText.GetComponent<Text>().color = greenOn;
    }
    public void AirBreakOff()
    {
        airBreakText.GetComponent<Text>().color = greenOff;
    }

    public void LockedOn()
    {
        lockedOnText.GetComponent<Text>().color = greenOn;
    }
    public void LockedOff()
    {
        lockedOnText.GetComponent<Text>().color = greenOff;
    }

    public void WarningOn()
    {
        warningText.GetComponent<Text>().color = redOn;
    }
    public void WarningOff()
    {
        warningText.GetComponent<Text>().color = redOff;
    }
    
    public void IncommingOn()
    {
        incommingText.GetComponent<Text>().color = redOn;
    }
    public void IncommingOff()
    {
        incommingText.GetComponent<Text>().color = redOff;
    }

    public void JamOn()
    {
        radarJamText.GetComponent<Text>().color = redOn;
    }
    public void JamOff()
    {
        radarJamText.GetComponent<Text>().color = redOff;
    }

    public void SignalLost()
    {
        signalLostText.GetComponent<Text>().color = redOn;
        restartText.GetComponent<Text>().color = redOn;
    }

    public IEnumerator ShakeCam(float shake)
    {
        mCamTf.position = new (0, -shake, -10);
        yield return new WaitForSeconds(0.02f);
        mCamTf.position = new(0, 0, -10);
        yield return new WaitForSeconds(0.02f);
        mCamTf.position = new(0, -shake * 0.5f, -10);
        yield return new WaitForSeconds(0.02f);
        mCamTf.position = new(0, 0, -10);
        yield return new WaitForSeconds(0.02f);
        mCamTf.position = new(0, -shake * 0.25f, -10);
        yield return new WaitForSeconds(0.01f);
        mCamTf.position = new(0, 0, -10);
    }

    public void ResetCam()
    {
        mCamTf.position = new Vector3(0, 0, -10);
    }
}
