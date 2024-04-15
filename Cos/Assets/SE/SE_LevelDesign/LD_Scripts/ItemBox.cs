using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public GameObject item; // ������ ������
    public Transform itemSpawn;//������ ���� ��ġ
    public float openSpeed = 1.0f;
    public float delayBeforeDisappear = 1.0f;

    private Animator boxAnimator;
    private bool Break = false;

    void Start()
    {
        boxAnimator = GetComponent<Animator>();
        if (boxAnimator == null)
        {
            Debug.LogError("Animator ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !Break)
        {
            StartCoroutine(OpenBox());
        }
    }

    IEnumerator OpenBox()
    {
        // �ڽ� ���� �ִϸ��̼� ���
        boxAnimator.SetBool("Break", true);
        Break = true;

        // ���� �ִϸ��̼� �ð���ŭ ���
        yield return new WaitForSeconds(openSpeed);

        // ������ Ȱ��ȭ
        item.SetActive(true);

        // delayBeforeDisappear �ð���ŭ ��� �� �ڽ� ������� �ִϸ��̼� ����
        yield return new WaitForSeconds(delayBeforeDisappear);

        // �ڽ� ������� �ִϸ��̼� ���
        boxAnimator.SetBool("IsOpen", false);

        // �ڽ� ����� �� ������ ��Ȱ��ȭ
        item.SetActive(false);

        Break = false;
    }




}
