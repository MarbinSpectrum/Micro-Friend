using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : MonoBehaviour
{
    #region [EnumGroup]

    public enum ControllGroup { 플레이어1, 플레이어2, 모든플레이어 };
    public enum ActionGroup { 앞으로걷기, 뒤로걷기 };

    #endregion

    [Header("버튼상태")]
    public bool state = false;

    [Header("조작가능한 플레이어")]
    public ControllGroup controllGroup;

    [Header("상호작용기능")]
    public ActionGroup actionGroup;

    Animator animator;
    bool Player1 = false;
    bool Player2 = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Player1)
        {
            if (Input.GetKeyDown(KeyCode.G))
                state = !state;
        }
        if (Player2)
        {
            if (Input.GetKeyDown(KeyCode.L))
                state = !state;
        }

        animator.SetBool("State", state);
        if(state)
        {
            Action();
        }

    }

    void Action()
    {
        switch (actionGroup)
        {
            case ActionGroup.앞으로걷기:
                Debug.Log("앞으로걷기");
                break;

            case ActionGroup.뒤로걷기:
                Debug.Log("뒤로걷기");
                break;
        }
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
