using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class NodeSuperposition
{
    private Map map;

    public List<MapNode> states { get; private set; }

    public Vector2Int index { get; private set; }

    public NodeSuperposition(Map map, Vector2Int index, List<MapNode> states)
    {
        this.map = map;
        this.index = index;
        this.states = states;
    }
}