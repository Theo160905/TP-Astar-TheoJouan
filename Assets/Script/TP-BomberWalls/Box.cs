using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Box : MonoBehaviour
{
    public bool IsOccupied;

    public List<Box> ListNeighbor = new List<Box>();
    private float raycastDistance = 2f;

    void Start()
    {
        DetectNeighbors();
    }

    void DetectNeighbors()
    {
        ListNeighbor.Clear();

        Vector3[] directions = new Vector3[]
        {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right
        };

        foreach (Vector3 direction in directions)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance);

            if (hit.collider !=  null)
            {
                if (hit.collider.isTrigger && hit.collider.gameObject.layer == 9)
                {
                    Box boxScript = hit.collider.GetComponent<Box>();

                    if (boxScript != null)
                    {
                        ListNeighbor.Add(boxScript);
                    }
                }
            }
        }
    }

    public float GetDistance(Transform transform)
    {
        Vector3 w = gameObject.transform.position;
        Vector2 v = transform.position;

        float Distance = Vector3.Distance(w, v);
        return Distance;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 7 || collision.gameObject.layer == 6)
        {  
            IsOccupied = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 7 || collision.gameObject.layer == 6)
        {
            IsOccupied = false;
        }
    }
}

