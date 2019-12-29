using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHPManager : MonoBehaviour
{
    public int hp = 100;

    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
