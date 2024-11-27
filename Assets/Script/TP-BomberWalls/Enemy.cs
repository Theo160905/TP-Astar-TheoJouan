using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

//A NE PAS FAIRE ATTENTION JUSTE POUR MOI VOIR CE QUE JE DOIS CORRIGER PLUS TARD 

// Je me suis inspirer de tuto pour faire les dictionaires et la comparaison du cout car toutes mes tentatives ont été des échecs
// video : A* Pathfinding in Unity, CodeMonkey,  Pathfinding - Understanding A* (A star), Tarodev

// Je viens de passer le weekend sur un bug dont je ne comprends pas l'origine 
// lorsque l'ennemy pose un bomb et repars pour aller chercher une nouvelle bomb
// et qu'il en récupère une nouvelle il n'efface pas les NodeClose donc l'Enemy ne peut pas refaire le chemin inverse
// ptet un problème dans le Astar ??

//Nouvelle tentative mais jsp pourquoi ça marche pas \ (O_o) /
//Juste en de ff
//bon ca part sur un 0 mtn j'ai des erreurs inconnu au bataillon 

public class Enemy : MonoBehaviour
{
    public List<Box> NodeOpen = new();
    public List<Box> NodeClose = new();

    public Box currentNode;
    public GameObject FinalNode;
    public GameObject Goal;

    private Dictionary<Box, float> gCost = new Dictionary<Box, float>();
    private Dictionary<Box, float> fCost = new Dictionary<Box, float>();
    private Dictionary<Box, Box> ListBox = new Dictionary<Box, Box>();

    public Bomb bomb;

    List<Box> possibleNextNodes = new List<Box>();

    public bool CheckNode(Box node)
    {
        return NodeOpen.Contains(node);
    }

    public void AddInNodeList(List<Box> list, Box Value)
    {
        list.Add(Value);
    }

    public void RemoveInNodeList(List<Box> list, Box Value)
    {
        list.Remove(Value);
    }

    public float CalculateCost(Box node)
    {
        return node.GetDistance(FinalNode.transform);
    }

    public IEnumerator ToGoOnFinalNode()
    {
        if (bomb == null)
        {
            FinalNode = FindAnyObjectByType<Bomb>().gameObject;
        }
        else
        {
            FinalNode = Goal;
        }

        gCost[currentNode] = 0;
        fCost[currentNode] = gCost[currentNode] + CalculateCost(currentNode);

        while (currentNode.gameObject.transform.position != FinalNode.gameObject.transform.position)
        {
            

            foreach (Box neighbor in currentNode.ListNeighbor)
            {
                if (!NodeClose.Contains(neighbor))
                {
                    float tentativeGCost = gCost[currentNode] + currentNode.GetDistance(neighbor.gameObject.transform);
                    if (!gCost.ContainsKey(neighbor) || tentativeGCost < gCost[neighbor])
                    {
                        gCost[neighbor] = tentativeGCost;
                        fCost[neighbor] = gCost[neighbor] + CalculateCost(neighbor);

                        ListBox[neighbor] = currentNode;
                        possibleNextNodes.Add(neighbor);
                    }
                }
            }

            if (possibleNextNodes.Count == 0)
            {
                Debug.Log("Aucun voisin trouvé");
                break;
            }

            Box nextNode = possibleNextNodes.OrderBy(n => fCost.ContainsKey(n) ? fCost[n] : float.MaxValue).First();

            AddInNodeList(NodeClose, currentNode);
            currentNode = nextNode;

            gameObject.transform.position = currentNode.gameObject.transform.position;
            possibleNextNodes.Clear();
            yield return new WaitForSeconds(0.5f);
        }

        if (currentNode.gameObject.transform.position == FinalNode.gameObject.transform.position)
        {
            Debug.Log("Fini");
            if (bomb != null && FinalNode == Goal)
            {
                bomb.gameObject.transform.position = gameObject.transform.position;
                bomb.Explode();
                bomb = null;
            }
            ClearNodeClose();
            StartCoroutine(ToGoOnFinalNode());
        }
    }

    private IEnumerator Test()
    {
        Debug.Log("Ready?");
        yield return new WaitForSeconds(1f);
        StartCoroutine(ToGoOnFinalNode());
    }

    private void Start()
    {
        StartCoroutine(Test());
    }

    private void ClearNodeClose()
    {
        NodeOpen.AddRange(NodeClose);
        NodeClose.Clear();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            bomb = collision.gameObject.GetComponent<Bomb>();
            collision.gameObject.SetActive(false);
        }
    }
}
