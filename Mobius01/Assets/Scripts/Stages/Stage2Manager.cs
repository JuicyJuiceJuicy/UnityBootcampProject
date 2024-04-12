using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage2Manager : MonoBehaviour
{
    public Text startText;
    public Text eventText;

    void Start()
    {
        StartCoroutine(StageStart());
    }

    void Update()
    {
        Restart();
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

    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene("Stage2");
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
