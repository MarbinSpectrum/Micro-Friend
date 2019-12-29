using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("플레이어 이동속도")]
    public int speed;

    [Header("플레이어번호")]
    public int playerNum;

    BoxCollider2D boxCollider2D;
    Rigidbody2D myRigidbody;
    bool climbingSW;
    RaycastHit2D grounded;
    float ladderx = 0;
    bool ladder = false;
    Animator animator;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Moving(playerNum);
        Climbing(climbingSW);
        animator.SetBool("Ladder", myRigidbody.gravityScale == 0);
        animator.SetBool("Run", Mathf.Abs(myRigidbody.velocity.x) > 0);
        animator.SetBool("Jump", !grounded);
        float dic = myRigidbody.velocity.x > 0 ? Mathf.Abs(transform.localScale.x) : myRigidbody.velocity.x < 0 ? -Mathf.Abs(transform.localScale.x) : transform.localScale.x;
        transform.localScale = new Vector3(dic, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //사다리 오르기
        if (collision.transform.tag == "ladder")
        {
            ladderx = collision.transform.position.x;
            climbingSW = true;
            if(myRigidbody.velocity.y >= 0)
                ladder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "ladder")
        {
            myRigidbody.gravityScale = 1;
            climbingSW = false;
            ladder = false;
        }
    }

    void Climbing(bool sw)
    {
        //trap Climbing
        if (sw == true)
        {
            if (playerNum == 1)
            {
                if (!ladder)
                {
                    if (Input.GetKeyDown(KeyCode.W))
                        ladder = true;
                    else if (Input.GetKeyDown(KeyCode.S))
                        ladder = true;
                }
                else
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        transform.position = transform.position + transform.up * Time.deltaTime * speed / 4;
                        transform.position = new Vector3(ladderx, transform.position.y, transform.position.z);
                        myRigidbody.gravityScale = 0;
                        myRigidbody.velocity = Vector2.zero;
                        animator.speed = 1;
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        transform.position = transform.position - transform.up * Time.deltaTime * speed / 4;
                        transform.position = new Vector3(ladderx, transform.position.y, transform.position.z);
                        myRigidbody.gravityScale = 0;
                        myRigidbody.velocity = Vector2.zero;
                        animator.speed = 1;
                    }
                    else
                        animator.speed = 0;
                }
            }
            else if (playerNum == 2)
            {
                if(!ladder)
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                        ladder = true;
                    else if (Input.GetKeyDown(KeyCode.DownArrow))
                        ladder = true;
                }
                else
                {
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        transform.position = transform.position + transform.up * Time.deltaTime * speed / 4;
                        transform.position = new Vector3(ladderx, transform.position.y, transform.position.z);
                        myRigidbody.gravityScale = 0;
                        myRigidbody.velocity = Vector2.zero;
                        animator.speed = 1;
                    }
                    else if (Input.GetKey(KeyCode.DownArrow))
                    {
                        transform.position = transform.position - transform.up * Time.deltaTime * speed / 4;
                        transform.position = new Vector3(ladderx, transform.position.y, transform.position.z);
                        myRigidbody.gravityScale = 0;
                        myRigidbody.velocity = Vector2.zero;
                        animator.speed = 1;
                    }
                    else
                        animator.speed = 0;
                }
            }
        }
        else
            animator.speed = 1;
    }

    void Moving(int playerNum)
    {
        grounded = Physics2D.BoxCast((Vector2)transform.position + boxCollider2D.offset - Vector2.up * 0.1f, boxCollider2D.size * new Vector2(0.7f, 1), 0, Vector2.zero, 1, 1 << LayerMask.NameToLayer("Terrain"));
        if(animator.GetBool("Jump") && grounded)
            effectManager.Instance.PlaySound("landing");
        if (grounded)
        {
            myRigidbody.gravityScale = 1;
        }

        if (playerNum == 1)
        {
            if (myRigidbody.gravityScale == 1)
            {
                if (Input.GetKey(KeyCode.A))
                    myRigidbody.velocity = new Vector2(-speed, myRigidbody.velocity.y);
                else if (Input.GetKey(KeyCode.D))
                    myRigidbody.velocity = new Vector2(+speed, myRigidbody.velocity.y);
                else
                    myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
            }

            //Jump & Function
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (grounded || (climbingSW && myRigidbody.gravityScale == 0))
                {
                    myRigidbody.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
                    myRigidbody.gravityScale = 1;
                    climbingSW = false;
                    effectManager.Instance.PlaySound("jump");
                    ladder = false;
                }
            }
        }
        else if (playerNum == 2)
        {
            if (myRigidbody.gravityScale == 1)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                    myRigidbody.velocity = new Vector2(-speed, myRigidbody.velocity.y);
                else if (Input.GetKey(KeyCode.RightArrow))
                    myRigidbody.velocity = new Vector2(+speed, myRigidbody.velocity.y);
                else
                    myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
            }

            //Jump & Function
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (grounded || (climbingSW && myRigidbody.gravityScale == 0))
                {
                    myRigidbody.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
                    myRigidbody.gravityScale = 1;
                    climbingSW = false;
                    effectManager.Instance.PlaySound("jump");
                    ladder = false;
                }
            }
        }
    }
}