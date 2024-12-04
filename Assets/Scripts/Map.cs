using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using static UnityEngine.GraphicsBuffer;

public class Map : MonoBehaviour
{
    // Random Start
    /*public bool chooseStart = false;
    Vector2Int startPos;
    MapNode startNode;*/

    public Vector2Int size { get; private set; } = new Vector2Int(5, 5);
    [SerializeField] float cellSize = 1;
    [SerializeField] MapNode[] nodes;

    public NodeSuperposition[,] superPosMatrix { get; private set; }
    public NodeSuperposition[] superPosArray { get; private set; }

    void Start()
    {
        Init();

        PrintSuperpositions();

        //Fill();
        //Create();
    }

    /// <summary>
    /// Fills the map matrix with Nodes in Superposition, with all possible states avail
    /// </summary>
    private void Init()
    {
        superPosMatrix = new NodeSuperposition[size.x, size.y];

        for (int i = 0; i < size.x; i++)
            for (int j = 0; j < size.y; j++)
                superPosMatrix[i, j] = new NodeSuperposition(this, new Vector2Int(i,j), new List<MapNode>(nodes));

        superPosArray = superPosMatrix.Cast<NodeSuperposition>().ToArray();

    }

    /// <summary>
    /// When the Wave Function Collapses
    /// </summary>
    private void Fill()
    {
        NodeSuperposition node = null;

        do {
            // Get non collapsed nodes / nodes in super position
            NodeSuperposition[] nodesNonCollapsed = superPosArray.Where(n => n.states.Count > 1).ToArray();

            // Done
            if (nodesNonCollapsed.Length == 0)
                return;

            // Get Node with least possible states
            int minStatesCount = nodesNonCollapsed.Min(n => n.states.Count);
            node = nodesNonCollapsed.First(n => n.states.Count == minStatesCount);
        }
        while (node.TrySelectState()); // Collapse that node
    }

    /// <summary>
    /// Make objs
    /// </summary>
    private void Create()
    {
        throw new NotImplementedException();
    }


    private void PrintIndices()
    {
        String matrixMessage = "";
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
                matrixMessage += (superPosMatrix[i, j].index) + "\t";
            matrixMessage += "\n";
        }

        Debug.Log("Map\n" + matrixMessage);
    }
    private void PrintSuperpositions()
    {
        String matrixMessage = "";
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
                matrixMessage += (superPosMatrix[i, j].ToString());
            matrixMessage += "\n";
        }

        Debug.Log("Map Super Positions\n" + matrixMessage);
    }
}