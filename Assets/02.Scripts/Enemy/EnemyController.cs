using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float HP = 10;

    public void onHit(float Damage)
    {
        HP -= Damage;
        if (HP < 0)
        {
            Destroy(gameObject);
        }
    }
}
