using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> NeighborNode;

    public float GetDistance(Transform transform)
    {
        Vector3 w = gameObject.transform.position;
        Vector2 v = transform.position;

        float Distance = Vector3.Distance(w, v);
        return Distance;
    }
}
