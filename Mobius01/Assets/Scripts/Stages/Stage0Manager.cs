using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Stage0Manager : MonoBehaviour
{
    public Text startText;
    public Text eventText;
    public GameObject dummyLeft;
    public GameObject dummyRight;
    public GameObject enemyMissile;
    public Transform playerT;

    int flag0 = 1;
    int flag1 = 1;
    float goal2 = 0;
    float goal4 = 0;
    int flag6 = 0;
    int flag8 = 0;
    int flag10 = 0;
    int flag12 = 0;

    void Start()
    {
        StartCoroutine(StartStage());
    }

    IEnumerator StartStage()
    {
        string[] startText0 =
{
            ".",
            "..",
            "...",
            "System Online",
        };

        float[] startTime0 =
        {
            0.5f,
            0.5f,
            0.5f,
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
        if (flag0 == 15 && flag1 == 15)
        {
            flag0++;
            StartCoroutine(Event15());
        }
        else if (flag0 == 14 && flag1 == 14)
        {
            flag0++;
            StartCoroutine(Event14());
        }
        else if (flag0 == 13 && flag1 == 13)
        {
            flag0++;
            StartCoroutine(Event13());
        }
        else if (flag1 == 12)
        {
            flag0 = 13;
            Event12();
        }
        else if (flag0 == 11 && flag1 == 11)
        {
            flag0++;
            StartCoroutine(Event11());
        }
        else if (flag1 == 10)
        {
            flag0 = 11;
            Event10();
        }
        else if (flag0 == 9 && flag1 == 9)
        {
            flag0++;
            StartCoroutine(Event9());
        }
        else if (flag1 == 8)
        {
            flag0 = 9;
            Event8();
        }
        else if (flag0 == 7 && flag1 == 7)
        {
            flag0++;
            StartCoroutine(Event7());
        }
        else if (flag1 == 6)
        {
            flag0 = 7;
            Event6();
        }
        else if (flag0 == 5 && flag1 == 5)
        {
            flag0++;
            StartCoroutine(Event5());
        }
        else if (flag1 == 4)
        {
            flag0 = 5;
            Event4();
        }
        else if (flag0 == 3 && flag1 == 3)
        {
            flag0++;
            StartCoroutine(Event3());
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
            "반갑습니다.",
            "차세대 공중우세전투기 XF-223 뫼비우스입니다.",
            "지금부터 기체 조작법에 대하여 설명드리겠습니다.",
            "기본적으로 방향키를 이용하여 이동할 수 있습니다.",
            "",
        };

        float[] eventTime0 =
        {
            1f,
            1f,
            1f,
            1f,
            0f,
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
        eventText.text = "Move : Arrow Key\n" + (goal2 * 100).ToString("0") + "%";
        if (goal2 >= 1)
        {
            goal2 = 0.99f;
            flag1 = 3;
        }
        else if (!(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0))
        {
             goal2 += Time.deltaTime;
        }
    }

    IEnumerator Event3()
    {
        string[] eventText0 =
        {
            "잘하셨습니다.",
            "대충 너무 빨라서 조준하기 어렵다는 내용",
            "Shift 키를 눌러 Air Break를 사용해 감속할 수 있습니다.",
            "",
        };

        float[] eventTime0 =
        {
            1f,
            1f,
            1f,
            0f,
        };

        for (int i = 0; i < eventText0.Length; i++)
        {
            eventText.text = eventText0[i];
            yield return new WaitForSeconds(eventTime0[i]);
        }

        flag1++;
    }

    void Event4()
    {
        eventText.text = "Left Shift : Air Break\n" + (goal4 * 100).ToString("0") + "%";
        if (goal4 >= 1)
        {
            flag1 = 5;
        }
        else if (!(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) && Input.GetKey(KeyCode.LeftShift))
        {
            goal4 += Time.deltaTime;
        }
    }

    IEnumerator Event5()
    {
        string[] eventText0 =
        {
            "잘하셨습니다.",
            "본 기에는 무장으로 GAU-79 펄스 개틀링이 탑재되어 있습니다.",
            "표적 드론을 사격하여 격추하여 보십시오.",
            "",
        };

        float[] eventTime0 =
        {
            1f,
            1f,
            1f,
            0f,
        };

        for (int i = 0; i < eventText0.Length; i++)
        {
            eventText.text = eventText0[i];
            yield return new WaitForSeconds(eventTime0[i]);
        }

        GameManager.instance.GunsOn();
        flag1++;
    }

    void Event6()
    {
        GameManager.instance.jammed = true;
        eventText.text = GameManager.instance.score + " / 5";
        if (flag6 == GameManager.instance.score && flag6 < 5)
            {
            int randomS = Random.Range(0, 2);
            float randomY = flag6 * 0.4f;
            if (randomS == 0)
            {
                Instantiate(dummyLeft, new Vector3(3, 3 + randomY, 0), Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Instantiate(dummyRight, new Vector3(-3, 3 + randomY, 0), Quaternion.Euler(0, 0, 0));
            }
            flag6++;
        }

        if (GameManager.instance.score >= 5)
        {
            flag1++;
            GameManager.instance.GunsOff();
        }
    }

    IEnumerator Event7()
    {
        string[] eventText0 =
        {
            "잘하셨습니다.",
            "또한 본 기에는 전투를 보조하는 AI가 탑재되어 있습니다.",
            "표적의 거리와 속도를 계산하여 조준 위치를 계산하여 줍니다.",
            "",
        };

        float[] eventTime0 =
        {
            1f,
            1f,
            1f,
            0f,
        };

        for (int i = 0; i < eventText0.Length; i++)
        {
            eventText.text = eventText0[i];
            yield return new WaitForSeconds(eventTime0[i]);
        }

        GameManager.instance.GunsOn();
        flag1++;
    }

    void Event8()
    {
        eventText.text = "Aim to Lead Circle\n" + (GameManager.instance.score - 10) + " / 10";
        GameManager.instance.jammed = false;
        if (flag8 == GameManager.instance.score - 5 && flag8 < 5)
        {
            int randomS = Random.Range(0, 2);
            float randomY = flag8 * 0.4f;
            if (randomS == 0)
            {
                Instantiate(dummyLeft, new Vector3(3, 3f + randomY, 0), Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Instantiate(dummyRight, new Vector3(-3, 3f + randomY, 0), Quaternion.Euler(0, 0, 0));
            }
            flag8++;
        }

        if (GameManager.instance.score - 5 >= 5)
        {
            flag1++;
            GameManager.instance.GunsOff();
        }
    }

    IEnumerator Event9()
    {
        string[] eventText0 =
        {
            "잘하셨습니다.",
            "너무 빨라서 조준이 어렵다면,",
            "Air Break를 사용하여 정밀하게 조준할 수 있습니다.",
            "",
        };

        float[] eventTime0 =
        {
            1f,
            1f,
            1f,
            0f,
        };

        for (int i = 0; i < eventText0.Length; i++)
        {
            eventText.text = eventText0[i];
            yield return new WaitForSeconds(eventTime0[i]);
        }

        GameManager.instance.GunsOn();
        flag1++;
    }

    void Event10()
    {
        eventText.text = "Use Air Break to Aim\n" + (GameManager.instance.score - 20) + " / 10";
        if (flag10 == GameManager.instance.score - 10 && flag10 < 5)
        {
            int randomS = Random.Range(0, 2);
            float randomY = flag10 * 0.4f;
            if (randomS == 0)
            {
                Instantiate(dummyLeft, new Vector3(3, 3f + randomY, 0), Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Instantiate(dummyRight, new Vector3(-3, 3f + randomY, 0), Quaternion.Euler(0, 0, 0));
            }
            flag10++;
        }

        if (GameManager.instance.score - 10 >= 5)
        {
            flag1++;
            GameManager.instance.GunsOff();
        }
    }

    IEnumerator Event11()
    {
        string[] eventText0 =
        {
            "잘하셨습니다.",
            "Space를 눌러 Burst 사격 모드를 사용할 수 있습니다.",
            "Heat가 90%를 돌파하면 자동적으로 종료됩니다.",
            "이를 사용해 특별히 꼴보기 싫은 상대를 눈앞에서 신속히 치울 수 있습니다.",
            "",
        };

        float[] eventTime0 =
        {
            1f,
            1f,
            1f,
            1f,
            0,
        };

        for (int i = 0; i < eventText0.Length; i++)
        {
            eventText.text = eventText0[i];
            yield return new WaitForSeconds(eventTime0[i]);
        }

        GameManager.instance.GunsOn();
        flag1++;
    }

    void Event12()
    {
        eventText.text = "Space: Burst\n" + (GameManager.instance.score - 15) + " / 10";
        if (flag12 == GameManager.instance.score - 15 && flag12 < 5)
        {
            int randomS = Random.Range(0, 2);
            float randomY = flag12 * 0.4f;
            if (randomS == 0)
            {
                Instantiate(dummyLeft, new Vector3(3, 3f + randomY, 0), Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Instantiate(dummyRight, new Vector3(-3, 3f + randomY, 0), Quaternion.Euler(0, 0, 0));
            }
            flag12++;
        }

        if (GameManager.instance.score - 15 >= 5)
        {
            flag1++;
            GameManager.instance.GunsOff();
        }
    }

    IEnumerator Event13()
    {
        string[] eventText0 =
        {
            "잘하셨습니다.",
            "또한 AI는 사격을 보조할 뿐만 아니라 위험을 감지하고 예측하여 줍니다.",
            "적의 공격과 예상 경로를 표시하며 위혐 지역에 있을 경우 경고합니다.",
            "참고로 본 기에는 동력원으로서 반물질 드라이브가 탑재되어 있어 피격시 대폭발을 일으킬 수 있습니다.",
            "따라서 피격에 각별히 주의하시길 바랍니다.",
            "",
        };

        float[] eventTime0 =
        {
            1f,
            1f,
            1f,
            1f,
            1f,
            0,
        };

        for (int i = 0; i < eventText0.Length; i++)
        {
            eventText.text = eventText0[i];
            yield return new WaitForSeconds(eventTime0[i]);
        }

        flag1++;
    }

    IEnumerator Event14()
    {
        int i = 0;
        float j = 1;
        eventText.text = "Evade\n" + i + "/ 20";
        for (i = 0; i < 21; i++)
        {
            float randomX = Random.Range(-0.2f, 0.2f);
            Instantiate(enemyMissile, new Vector3(playerT.position.x + randomX, 5, 0), Quaternion.Euler(0, 0, 0));
            eventText.text = "Evade\n" + i + "/ 20";
            j *= 0.9f;
            yield return new WaitForSeconds(j);
        }
        yield return new WaitForSeconds(2);
        flag1++;
    }

    IEnumerator Event15()
    {
        if (GameManager.instance.alive)
        {
            string[] eventText0 =
            {
            "잘하셨습니다.",
            "이것으로 마치겠습니다.",
             "",
             };

            float[] eventTime0 =
            {
            1f,
            1f,
            0,
             };

            for (int i = 0; i < eventText0.Length; i++)
            {
                eventText.text = eventText0[i];
                yield return new WaitForSeconds(eventTime0[i]);
            }

            SceneManager.LoadScene("Main");
            flag1++;
        }
    }

    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene("Stage0");
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
