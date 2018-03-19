using UnityEngine;
using System.Collections;
using System;

public class GridNode : SettlersEngine.IPathNode<System.Object>
{
    // Grid position
    public Int32 X { get; set; }
    public Int32 Y { get; set; }

    // World position
    public float XWorld { get; set; }
    public float YWorld { get; set; }

    public Boolean IsWall { get; set; }


    public bool IsWalkable(System.Object unused)
    {
        return !IsWall;
    }
}
