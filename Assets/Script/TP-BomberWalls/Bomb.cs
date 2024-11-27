using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Box box;
    public GameObject Explosion;
    private SpawnBomb spawnBomb;
    
    private void Awake()
    {
        spawnBomb = FindAnyObjectByType<SpawnBomb>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            collision.gameObject.GetComponent<Enemy>().bomb = this;
            gameObject.SetActive(false);
        }
    }

    public void Explode()
    {
        this.gameObject.SetActive(true);
        Explosion.SetActive(true);
    }

    IEnumerator TimeExplode()
    {
        yield return new WaitForSeconds(0.25f);
        Explosion.SetActive(false);
        spawnBomb.ReturnObject(this.gameObject);
    }
}
