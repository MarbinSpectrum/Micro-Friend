using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop_Button : MonoBehaviour
{
    #region [EnumGroup]

    public enum ControllGroup { 플레이어1, 플레이어2, 모든플레이어 };

    #endregion

    [Header("버튼상태")]
    public bool state = false;

    [Header("조작가능한 플레이어")]
    public ControllGroup controllGroup;

    Animator animator;
    bool Player1 = false;
    bool Player2 = false;
    float time = 1;
    private void Awake()
    {
        time = Random.Range(10, 20);
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(state)
        {
            Robot.HP -= Time.deltaTime;
            if (Player1)
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    time = Random.Range(10, 20);
                    state = false;
                    effectManager.Instance.PlaySound("button");
                }
            }
            if (Player2)
            {
                if (Input.GetKeyDown(KeyCode.L))
                {
                    time = Random.Range(10, 20);
                    state = false;
                    effectManager.Instance.PlaySound("button");
                }
            }
        }
        else
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                effectManager.Instance.PlaySound("machine");
                state = true;
            }
        }
        animator.SetBool("State", state);

    }

    void Action()
    {
       
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (controllGroup == ControllGroup.플레이어1)
        {
            if (col.transform.name == "Player1")
                Player1 = true;
        }
        else if (controllGroup == ControllGroup.플레이어2)
        {
            if (col.transform.name == "Player2")
                Player2 = true;
        }
        else if (controllGroup == ControllGroup.모든플레이어)
        {
            if (col.transform.name == "Player1")
                Player1 = true;
            if (col.transform.name == "Player2")
                Player2 = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (controllGroup == ControllGroup.플레이어1)
        {
            if (col.transform.name == "Player1")
                Player1 = false;
        }
        else if (controllGroup == ControllGroup.플레이어2)
        {
            if (col.transform.name == "Player2")
                Player2 = false;
        }
        else if (controllGroup == ControllGroup.모든플레이어)
        {
            if (col.transform.name == "Player1")
                Player1 = false;
            if (col.transform.name == "Player2")
                Player2 = false;
        }
    }
}
