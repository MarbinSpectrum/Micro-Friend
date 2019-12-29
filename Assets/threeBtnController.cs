using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class threeBtnController : MonoBehaviour
{
    #region [EnumGroup]

    public enum ControllGroup { 플레이어1, 플레이어2, 모든플레이어 };
    public enum RGBSwitch { R, G, B }
    public enum ActionGroup { 앞으로, 뒤로 , 왼쪽으로 , 오른쪽으로, 정지하기 };
    #endregion

    [Header("버튼상태")]
    public bool state = false;

    [Header("조작가능한 플레이어")]
    public ControllGroup controllGroup;

    [Header("R상태")]
    public ActionGroup RSet;

    [Header("G상태")]
    public ActionGroup GSet;

    [Header("B상태")]
    public ActionGroup BSet;

    public GameObject R, G, B;

    int rgbSwitch = 1;
    bool Player1 = false;
    bool Player2 = false;
    float spped = 1.5f;
    void Update()
    {
        if(Player1)
        {
            if (Input.GetKeyDown(KeyCode.G))
                Action();
        }
        if (Player2)
        {
            if (Input.GetKeyDown(KeyCode.L))
                Action();
        }

        ActionGroup emp = ActionGroup.정지하기;
        if (R.activeSelf)
            emp = RSet;
        else if (G.activeSelf)
            emp = GSet;
        else if (B.activeSelf)
            emp = BSet;

        if (emp == ActionGroup.뒤로)
        {
            Robot.instance.GetComponent<Rigidbody2D>().velocity = new Vector2(Robot.instance.GetComponent<Rigidbody2D>().velocity.x, -spped);
        }
        else if (emp == ActionGroup.앞으로)
        {
            Robot.instance.GetComponent<Rigidbody2D>().velocity = new Vector2(Robot.instance.GetComponent<Rigidbody2D>().velocity.x, +spped);
        }
        else if (emp == ActionGroup.오른쪽으로)
        {
            Robot.instance.GetComponent<Rigidbody2D>().velocity = new Vector2(+spped, Robot.instance.GetComponent<Rigidbody2D>().velocity.y);
        }
        else if (emp == ActionGroup.왼쪽으로)
        {
            Robot.instance.GetComponent<Rigidbody2D>().velocity = new Vector2(-spped, Robot.instance.GetComponent<Rigidbody2D>().velocity.y);
        }
        else if (emp == ActionGroup.정지하기)
        {
            if(RSet == ActionGroup.앞으로 && BSet == ActionGroup.뒤로)
                Robot.instance.GetComponent<Rigidbody2D>().velocity = new Vector2(Robot.instance.GetComponent<Rigidbody2D>().velocity.x, 0);
            else  if (RSet == ActionGroup.왼쪽으로 && BSet == ActionGroup.오른쪽으로)
                Robot.instance.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Robot.instance.GetComponent<Rigidbody2D>().velocity.y);
        }

    }

    void Action()
    {
        effectManager.Instance.PlaySound("button");
        rgbSwitch += 1;
        rgbSwitch = rgbSwitch % 3;
        switch ((RGBSwitch)rgbSwitch)
        {
            case RGBSwitch.R:
                R.SetActive(true);
                G.SetActive(false);
                B.SetActive(false);
                break;
            case RGBSwitch.G:
                R.SetActive(false);
                G.SetActive(true);
                B.SetActive(false);
                break;
            case RGBSwitch.B:
                R.SetActive(false);
                G.SetActive(false);
                B.SetActive(true);
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
