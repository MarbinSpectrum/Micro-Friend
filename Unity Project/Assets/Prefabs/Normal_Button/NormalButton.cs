using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalButton : MonoBehaviour
{
    #region [EnumGroup]

    public enum ControllGroup { 플레이어1, 플레이어2, 모든플레이어 };
    public enum ActionGroup { 연료채우기, 뒤로걷기 };

    #endregion

    [Header("버튼상태")]
    public bool state = false;

    [Header("조작가능한 플레이어")]
    public ControllGroup controllGroup;

    [Header("상호작용기능")]
    public ActionGroup actionGroup;
    bool act = false;

    Animator animator;
    bool Player1 = false;
    bool Player2 = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool("State", state);

        if (state)
        {
            Action();
            act = true;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Normal_Button_On") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                state = false;
                act = false;
            }
        }
        else
        {
            if (Player1)
            {
                if (Input.GetKeyDown(KeyCode.G))
                    state = true;
            }
            if (Player2)
            {
                if (Input.GetKeyDown(KeyCode.L))
                    state = true;
            }
        }
    }

    void Action()
    {
        if (act) return;
        effectManager.Instance.PlaySound("button"); 
        switch (actionGroup)
        {
            case ActionGroup.연료채우기:
                Robot.HP+= 0.4f;
                break;

            case ActionGroup.뒤로걷기:
                Debug.Log("뒤로걷기");
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(controllGroup == ControllGroup.플레이어1)
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
