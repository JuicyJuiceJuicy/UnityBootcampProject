using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Text titleText;
    public Text titleText2;
    public Text versionText;
    public Button tutorialButton;
    public Text tutorialText;
    public Button stage1Button;
    public Text stage1Text;
    public Button stageEButton;
    public Text stageEText;

    void Start()
    {
        StartCoroutine(StageStart());
    }

    IEnumerator StageStart()
    {
        string[] titleText0 =
        {
            ".",
            "..",
            "...",
            "Mobius 01",
        };

        float[] titleTime0 =
        {
            0.5f,
            0.5f,
            0.5f,
            0,
        };
        
        for (int i = 0; i < titleText0.Length; i++)
        {
            titleText.text = titleText0[i];

            if (i == 3)
            {
                titleText2.GetComponent<Text>().color = new Color(0, 1, 0, 1); ;
                versionText.GetComponent<Text>().color = new Color(0, 1, 0, 1);
                tutorialButton.image.color = new Color(0, 1, 0, 1);
                tutorialText.GetComponent<Text>().color = new Color(0, 1, 0, 1);
                stage1Button.image.color = new Color(0, 1, 0, 1);
                stage1Text.GetComponent<Text>().color = new Color(0, 1, 0, 1);
                stageEButton.image.color = new Color(0, 1, 0, 1);
                stageEText.GetComponent<Text>().color = new Color(0, 1, 0, 1);

                GameManager.instance.SystemOnline();
            }

            yield return new WaitForSeconds(titleTime0[i]);
        }
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Stage0");
    }

    public void Stage1()
    {
        SceneManager.LoadScene("Stage1");
    }
    public void StageE()
    {
        SceneManager.LoadScene("StageE");
    }
}
