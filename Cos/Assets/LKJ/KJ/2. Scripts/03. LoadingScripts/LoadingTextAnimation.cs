using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingTextAnimation : MonoBehaviour
{
    [Header("LoadingText")]
    // text ������Ʈ
    public TextMeshProUGUI loadingText;
    // '.' �ؽ�Ʈ �ӵ�
    public float dotSpeed = 0.5f;
    // �⺻ ����
    public string baseText = "�ε� ��";
    // ���� '.' �ؽ�Ʈ ����
    public int dotCount = 0;
    void Start()
    {
        StartCoroutine(AnimateDots());
    }

    IEnumerator AnimateDots()
    {
        while (true)
        {
            // '.' �� ������ 0 ~ 4���� �ݺ�
            dotCount = (dotCount + 1) % 5;
            // �⺻ �ؽ�Ʈ�� '.' �߰�
            loadingText.text = baseText + new string('.', dotCount);
            // ������ �ð� ���� ���
            yield return new WaitForSeconds(dotSpeed);
        }
    }
}
