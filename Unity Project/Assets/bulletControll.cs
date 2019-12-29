using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletControll : MonoBehaviour
{
    public Vector3 dic;
    public float speed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "Robot")
        {
            Robot.HP -= 5;
            Debug.Log("Hit");
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        transform.position += dic * speed;
    }
}
