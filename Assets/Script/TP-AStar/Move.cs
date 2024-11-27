using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public static Move Instance;

    private static Move instance
    {
        get 
        {
            if (instance == null)
            {
                Instance = FindAnyObjectByType<Move>();
            }
            return instance; 
        }
    }

    Vector3 Position;

    public Transform DestinationFinal;

   void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            OnChooseDestination();
        }

        Vector3 NewPosition = new Vector3(Position.x, Position.y, 0);
        Vector3 Direction = NewPosition - transform.position;

        if (Direction.magnitude <= 0.3f) return;

        Vector3 velocite = 2f * Time.deltaTime* Direction.normalized;
        gameObject.transform.Translate(velocite, Space.World);
    }

    public void OnChooseDestination()
    {
        Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
