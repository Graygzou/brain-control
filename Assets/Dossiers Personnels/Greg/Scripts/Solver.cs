using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that contains the definition of the heuristic
/// </summary>
/// <typeparam name="TPathNode"></typeparam>
/// <typeparam name="TUserContext"></typeparam>
public class AStarAlgorithm<TPathNode, TUserContext> : SettlersEngine.AStarAlgorithm<TPathNode,
    TUserContext> where TPathNode : SettlersEngine.IPathNode<TUserContext>
    {

    public AStarAlgorithm(TPathNode[,] inGrid)
        : base(inGrid)
    { }

    protected override double Heuristic(PathNode inStart, PathNode inEnd)
    {
        int formula = GameManager.distance;
        int dx = Mathf.Abs(inStart.X - inEnd.X);
        int dy = Mathf.Abs(inStart.Y - inEnd.Y);

        if (formula == 0)
            return Mathf.Sqrt(dx * dx + dy * dy); //Euclidean distance

        else if (formula == 1)
            return (dx * dx + dy * dy); //Euclidean distance squared

        else if (formula == 2)
            return Mathf.Min(dx, dy); //Diagonal distance

        else if (formula == 3)
            return (dx * dy) + (dx + dy); //Manhatten distance
        else
            return Mathf.Abs(inStart.X - inEnd.X) + Mathf.Abs(inStart.Y - inEnd.Y);

        //return 1*(Math.Abs(inStart.X - inEnd.X) + Math.Abs(inStart.Y - inEnd.Y) - 1); //optimized tile based Manhatten
        //return ((dx * dx) + (dy * dy)); //Khawaja distance
    }

    protected override double NeighborDistance(PathNode inStart, PathNode inEnd)
    {
        return Heuristic(inStart, inEnd);
    }
}
