using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Astar : MonoBehaviour
{
    [Serializable]
    public class ListOfList
    {
        [field: SerializeField]
        public List<Node> List { get; set; }

        [field: SerializeField]
        public float Amount { get; set; }
    }

    [SerializeField]
    private List<ListOfList> listOfLists = new();

    public List<Node> NodeOpen = new();
    public List<Node> NodeClose = new();

    public Node currentNode;
    public Node FinalNode;

    public bool CheckNode(Node node)
    {
        for (int i = 0; i < NodeOpen.Count; i++)
        {
            if (NodeOpen[i] == node) return true;  
        }
        return false;
    }

    public void AddInNodeList(List<Node> list, Node Value)
    {
        list.Add(Value);
    }

    public void RemoveInNodeList(List<Node> list, Node Value)
    {
        list.Remove(Value);
    }

    public void ToGoOnFinalNode()
    {
        while (currentNode.gameObject.transform.position != FinalNode.gameObject.transform.position)
        {
            List<Node> possibleNextNodes = new List<Node>();

            foreach (Node neighbor in currentNode.NeighborNode)
            {
                if (!NodeClose.Contains(neighbor))
                {
                    float distanceToFinalNode = neighbor.GetDistance(FinalNode.transform);
                    ListOfList list = new ListOfList
                    {
                        List = new List<Node> { neighbor },
                        Amount = distanceToFinalNode
                    };
                    possibleNextNodes.Add(neighbor);
                }
            }

            if (possibleNextNodes.Count == 0)
            {
                Debug.Log("Aucun voisin trouvé");
                break;
            }

            // Sélectionner le voisin ayant la distance la plus proche du noeud final
            Node nextNode = possibleNextNodes.OrderBy(n => n.GetDistance(FinalNode.transform)).First();

            AddInNodeList(NodeClose, currentNode);
            currentNode = nextNode;
            Debug.Log(currentNode);
        }

        if (currentNode.gameObject.transform.position == FinalNode.gameObject.transform.position)
        {
            Debug.Log("Fini");
        }
    }

    private IEnumerator Test()
    {
        Debug.Log("Ready?");
        yield return new WaitForSeconds(1f);
        ToGoOnFinalNode();
    }

    private void Start()
    {
        StartCoroutine(Test());
    }
}