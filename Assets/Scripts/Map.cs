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

    [SerializeField] bool step;

    [SerializeField] public Vector2Int size;
    [SerializeField] float cellSize = 1;
    [SerializeField] List<MapNode> nodes;

    public NodeSuperposition[,] superPosMatrix { get; private set; }
    public NodeSuperposition[] superPosArray { get; private set; }

    void Start()
    {
        InitMapMatrix();

        PrintSuperpositions();
        
        if (!step)
        {
            Fill();
            Create();
            PrintSuperpositions();
        }
    }

    void Update()
    {
        if (!step)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!StepFill())
                Create();
            PrintSuperpositions();
        }
    }

    /// <summary>
    /// Fills the map matrix with Nodes in Superposition, with all possible states avail
    /// </summary>
    private void InitMapMatrix()
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
            {
                Debug.Log("Map collapsed");
                return;
            }

            // Get Node with least possible states
            int minStatesCount = nodesNonCollapsed.Min(n => n.states.Count);
            node = nodesNonCollapsed.First(n => n.states.Count == minStatesCount);

            node.TrySelectState();
        }
        while (true/*node.TrySelectState()*/); // Collapse that node
    }

    /// <summary>
    /// Make objs
    /// </summary>
    private void Create()
    {
        for (int i = 0; i < size.x; i++)
            for (int j = 0; j < size.y; j++)
            {
                Vector3 pos = new Vector3(cellSize * i, 0, cellSize * j);
                superPosMatrix[i, j].Create(pos, transform);
            }
    }

    #region Step Functions

    /// <returns>False when the map is collapsed</returns>
    private bool StepFill()
    {
        NodeSuperposition node = null;

        // Get non collapsed nodes / nodes in super position
        NodeSuperposition[] nodesNonCollapsed = superPosArray.Where(n => n.states.Count > 1).ToArray();
        
        Debug.Log(nodesNonCollapsed.Count());

        // Done
        if (nodesNonCollapsed.Length == 0)
        {
            Debug.Log("Map collapsed");
            return false;
        }


        // Get Node with least possible states
        int minStatesCount = nodesNonCollapsed.Min(n => n.states.Count);
        node = nodesNonCollapsed.First(n => n.states.Count == minStatesCount);

        Debug.Log("Stepped Fill");

        node.TrySelectState();

        return true;
    }

    #endregion

    #region Prints
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

    #endregion
}