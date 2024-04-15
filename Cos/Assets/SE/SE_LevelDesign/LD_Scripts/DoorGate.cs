using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DoorGate : MonoBehaviour
{
    private Animator door; //�ִϸ����� ������Ʈ ����
    public bool isOpen; //�� �ʱ� ���� ����
    

    private void Start()
    {
        door = GetComponent<Animator>(); //��������: ��ũ��Ʈ���� �ִϸ��̼� ����.
    }
    public void ToggleDoor() //�� ���� �ݴ� �Լ�
    {
        isOpen = !isOpen; //������ ����. �����ִٸ� ������.
        door.SetBool("isOpen", true); //�Ķ���� ����.
        
    }

    void CheckDoorConditions() //���� ���� ������ üũ�ϴ� �Լ�
    {
        //FŰ�� ������ ToggleDoor() �Լ��� ȣ���ϵ��� ��
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleDoor();
        }
    }

    private void Update()
    {
        CheckDoorConditions();

        if (!isOpen)
        {
            door.SetBool("isOpen", false);
        }
        else if (isOpen)
        {
            door.SetBool("isOpen", true);
        }
    }
}
