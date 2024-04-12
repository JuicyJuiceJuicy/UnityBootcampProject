using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Stage1Manager : MonoBehaviour
{
    public static Stage1Manager instance;

    public Text startText;
    public Text eventText;

    public GameObject boss1Attack;

    int flag0 = 1;
    int flag1 = 1;

    int flag2 = 1;
    public float spawnEnemyTime = 1f;

    bool flag3 = true;
    public GameObject boss1;

    public bool boss1Killed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        StartCoroutine(StageStart());
    }

    IEnumerator StageStart()
    {
        string[] startText0 =
        {
            ".",
            "..",
            "...",
            "System Online",
            "Weapons Free",
            "Engage",
        };

        float[] startTime0 =
        {
            0.5f,
            0.5f,
            0.5f,
            1f,
            1f,
            1f,
        };

        for (int i = 0; i < startText0.Length; i++)
        {
            startText.text = startText0[i];

            if (i < 3)
            {
                SoundManager.instance.StartSound(i);
            }

            if (i == 3)
            {
                GameManager.instance.SystemOnline();
            }

            if (i == 5)
            {
                GameManager.instance.GunsOn();
            }

            yield return new WaitForSeconds(startTime0[i]);
        }
        startText.gameObject.SetActive(false);
    }

    void Update()
    {
        Restart();
        Stage();
    }

    void Stage()
    {
        if (flag0 == 4 && flag1 == 4)
        {
            flag0++;
            StartCoroutine(Event4());
        }
        else if (flag1 == 3)
        {
            flag0 = 4;
            Event3();
        }
        else if (flag1 == 2)
        {
            flag0 = 3;
            Event2();
        }
        else if (GameManager.instance.time > 4 && flag0 == 1 && flag1 ==1)
        {
            flag0++;
            StartCoroutine(Event1());
        }
    }

    IEnumerator Event1()
    {
        string[] eventText0 =
        {
            "스테이지 1 시작 대화 1",
            "스테이지 1 시작 대화 2",
            "스테이지 1 시작 대화 3",
            "스테이지 1 시작 대화 4",
            "",
        };

        float[] eventTime0 =
        {
            1f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
        };

        for (int i = 0; i < eventText0.Length; i++)
        {
            eventText.text = eventText0[i];
            yield return new WaitForSeconds(eventTime0[i]);
        }
        flag1++;
    }

    void Event2()
    {
        if (flag2 == 1)
        {
            flag2 = 2;
            SpawnManager.instance.spawnEnemy = true;
            SpawnManager.instance.SpawnEnemy();
        }
        
        if (GameManager.instance.score >= 30 && flag2 == 3)
        {
            flag1 = 3;
        }
        else if (GameManager.instance.score >= 25)
        {
            SpawnManager.instance.spawnBoss1Attack = false;
        }
        else if (GameManager.instance.score >= 5 && flag2 == 2)
        {
            flag2 = 3;
            SpawnManager.instance.spawnBoss1Attack = true;
            SpawnManager.instance.Boss1Attack();
        }
    }

    void Event3()
    {
        if (flag3)
        {
            flag3 = false;
            Instantiate(boss1, new Vector3(0, 5, 0), Quaternion.Euler(0, 0, 0));
        }

        if (boss1Killed && GameManager.instance.alive)
        {
            SpawnManager.instance.spawnEnemy = false;
            GameManager.instance.alive = false;
            GameManager.instance.gunsOn = false;
            flag1 = 4;
        }
    }

    IEnumerator Event4()
    {
        yield return new WaitForSeconds(2);
        string[] eventText0 =
        {
            "스테이지 1 종료 대화 1",
            "스테이지 1 종료 대화 2",
            "스테이지 1 종료 대화 3",
            ""
        };

        float[] eventTime0 =
        {
            1,
            0.5f,
            3f,
            0f,
        };

        for (int i = 0; i < eventText0.Length; i++)
        {
            eventText.text = eventText0[i];
            yield return new WaitForSeconds(eventTime0[i]);
        }
        flag1++;
        SceneManager.LoadScene("Main");
    }


    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene("Stage1");
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
