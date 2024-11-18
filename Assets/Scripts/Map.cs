using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Map : MonoBehaviour
{
    // Random Start
    /*public bool chooseStart = false;
    Vector2Int startPos;
    MapNode startNode;*/

    [SerializeField] Vector2Int size = new Vector2Int(5, 5);
    [SerializeField] float cellSize = 1;
    [SerializeField] MapNode[] nodes;

    public NodeSuperposition[,] theMap;

    void Start()
    {
        Init();
        //Fill();
        //Create();
    }

    /// <summary>
    /// Fills the map matrix with Nodes in Superposition, with all possible states avail
    /// </summary>
    private void Init()
    {
        theMap = new NodeSuperposition[size.x, size.y];

        for (int i = 0; i < size.x; i++)
            for (int j = 0; j < size.y; j++)
                theMap[i, j] = new NodeSuperposition(this, new Vector2Int(i,j), new List<MapNode>(nodes));

        #region Debug
        for (int i = 0; i < size.x; i++)
            for (int j = 0; j < size.y; j++)
                Debug.Log(theMap[i, j].index);
        #endregion
    }

    /// <summary>
    /// When the Wave Function Collapses
    /// </summary>
    private void Fill()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Make objs
    /// </summary>
    private void Create()
    {
        throw new NotImplementedException();
    }

}