using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// 
/// </summary>
public class NodeSuperposition
{
    private Map map;

    public List<MapNode> states { get; private set; }

    public MapNode selectedState = null;

    public Vector2Int index { get; private set; }

    public NodeSuperposition(Map map, Vector2Int index, List<MapNode> states)
    {
        this.map = map;
        this.index = index;
        this.states = states;
    }

    /// <returns>List of adjacent nodes positions. Excludes positions out of bounds</returns>
    List<Vector2Int> GetAdjacentNodesPositions()
    {
        List<Vector2Int> cells = new List<Vector2Int>();

        if (index.x - 1 >= 0) 
            cells.Add(new Vector2Int(index.x - 1, index.y));

        if (index.x + 1 < map.size.x) 
            cells.Add(new Vector2Int(index.x + 1, index.y));

        if (index.y - 1 >= 0) 
            cells.Add(new Vector2Int(index.x, index.y - 1));

        if (index.y + 1 < map.size.y) 
            cells.Add(new Vector2Int(index.x, index.y + 1));

        return cells;
    }

    /// <returns>List of adjacent nodes. Excludes positions out of bounds</returns>
    List<NodeSuperposition> GetAdjacentNodes()
    {
        List<NodeSuperposition> nodes = new List<NodeSuperposition>();
        List<Vector2Int> adjacencies = GetAdjacentNodesPositions();

        foreach (Vector2Int coord in adjacencies)
            nodes.Add(map.superPosMatrix[coord.x, coord.y]);

        return nodes;
    }

    public bool TrySelectState()
    {
        while (states.Count > 0)
        {
            // This is where you can add biasing
            selectedState = states[UnityEngine.Random.Range(0, states.Count)];

            if (!TryUpdateAdjacentNodes())
                states.Remove(selectedState);
            else 
                return true;
        }
        return false;
    }

    private bool TryUpdateAdjacentNodes()
    {
        List<NodeSuperposition> neighbors = GetAdjacentNodes();
        List<List<MapNode>> newStates = new List<List<MapNode>>();

        foreach(NodeSuperposition neighbor in neighbors)
        {
            List<MapNode> adjStates = new List<MapNode>();

            Vector2Int dir = (index - neighbor.index);

            foreach (MapNode adjNode in neighbor.states)
            {
                if (NodeSuperposition.CheckValidPair(selectedState, adjNode, dir))
                    adjStates.Add(adjNode);
            }

            // No valid states in a neighbor, therefore this isnt a valid state to collapse to
            if (adjStates.Count < 1)
                return false;

            newStates.Add(adjStates);
        }

        // Neighbors have proper states
        UpdateAdjacentNodes(neighbors, newStates);

        return true;
    }

    /// <summary>
    /// Called in Try Update Adjacent Cells
    /// </summary>
    private void UpdateAdjacentNodes(List<NodeSuperposition> neighbors, List<List<MapNode>> newStates)
    {
        int i = 0;
        foreach(NodeSuperposition neighbor in neighbors)
        {
            neighbor.states = newStates[i];
            i++;
        }
    }

    // TODO Update to account for rotations
    private static bool CheckValidPair(MapNode node1, MapNode node2, Vector2Int dir)
    {
        // Going Right
        if (dir.x == 1)
            return (node1.X_Pos_Contact == node2.X_Neg_Contact);

        // Left
        if (dir.x == -1)
            return (node1.X_Neg_Contact == node2.X_Pos_Contact);

        // Going Up
        if (dir.y == 1)
            return (node1.Z_Pos_Contact == node2.Z_Neg_Contact);

        // Down
        if (dir.y == -1)
            return (node1.Z_Neg_Contact == node2.Z_Pos_Contact);

        throw new Exception();
    }

    public override string ToString()
    {
        String _index = index.ToString();

        String _selectedState = "None";
        if (selectedState != null)
            _selectedState = selectedState.name;

        String _states = ""; // TODO Display the elements
        foreach(MapNode state in states)
        {
            _states += state.name + ", \t";
        }

        String _message = $"{_index}\nSelected State: {_selectedState}\nStates:\n{_states}\n";
        
        return _message;
    }
}