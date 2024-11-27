using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            collision.gameObject.GetComponent<ThisIsAWallDestructible>().RemoveHealth();
        }
    }
}
