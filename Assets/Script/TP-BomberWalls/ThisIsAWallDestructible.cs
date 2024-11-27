using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisIsAWallDestructible : MonoBehaviour
{
    int health = 10;

    public void RemoveHealth()
    {
        health--;
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
