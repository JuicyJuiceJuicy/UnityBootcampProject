using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float time;

    public Transform playerT;
    public float playerX;
    public float playerY;

    public int score = 0;
    public float difficulty = 1;

    public bool jammed = false;

    public bool systemOnline = false;
    public bool gunsOn = false;
    public bool gunsOff = true;
    public bool alive;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        Timer();
        playerX = playerT.position.x;
        playerY = playerT.position.y;
    }

    void Timer()
    {
        if (alive)
        {
            time += 1 * Time.deltaTime;
        }
    }

    public void SystemOnline()
    {
        systemOnline = true;
    }

    public void GunsOn()
    {
        gunsOn = true;
    }
    public void GunsOff()
    {
        gunsOn = false;
        gunsOff = true;
    }

    public void AddScore(int num)
    {
        score += num;
        difficulty *= 0.99f;
        UIManager.instance.ScoreText(score);
    }

    public void JamOn()
    {
        jammed = true;
        UIManager.instance.JamOn();
    }

    public void JamOff()
    { 
        jammed = false;
        UIManager.instance.JamOff();
    }
}
