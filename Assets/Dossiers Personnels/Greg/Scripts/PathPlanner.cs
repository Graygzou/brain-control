using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPlanner
{
    // Current path accepted
    private Vector2 m_DestinationPos;

    private GenerateGrid m_grid;

    private Enemy m_owner;

    // A* instance
    private AStarAlgorithm<GridNode, System.Object> aStar;

    // Current path
    IEnumerable<GridNode> path;

    // TODO REMOVE
    private Color myColor;
    public GridNode nextNode;

    public PathPlanner(GenerateGrid grid, Enemy owner)
    {
        m_grid = grid;
        m_owner = owner;
        myColor = getRandomColor();

        Start();
    }

    Color getRandomColor()
    {
        Color tmpCol = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
        return tmpCol;

    }

    // Use this for initialization
    public void Start()
    {
        // Create the script that will contains the definition of the A* heuristic
        aStar = new AStarAlgorithm<GridNode, System.Object>(m_grid.GridPathFinding);

        // get the path which is correspond to the closest player
        path = GetPathClosestPlayer(aStar);

        UpdatePath();
    }

    public GridNode UpdatePath()
    {
        int currentX = m_owner.GetCurrentGridPosition().X;
        int currentY = m_owner.GetCurrentGridPosition().Y;

        // Check if the player has moved
        if (path == null) { 
            // get the path which is correspond to the closest player
            IEnumerable<GridNode> path = GetPathClosestPlayer(aStar);
        }

        if (path != null)
        {
            foreach (GridNode current_node in path)
            {
                //Vector2 node_position = GetPosInWorld(current_node);

                // Test if the node is in the field of view of the player
                if (InFieldOfView(new Vector2(current_node.XWorld, current_node.YWorld), m_owner.transform.position))
                {
                    // Update the next node.
                    nextNode = current_node;
                }
            }

            foreach (GameObject g in GameObject.FindGameObjectsWithTag("GridBox"))
            {
                if (g.GetComponent<Renderer>().material.color != Color.red && g.GetComponent<Renderer>().material.color == myColor)
                    g.GetComponent<Renderer>().material.color = Color.white;
            }

            foreach (GridNode node in path)
            {
                if (node == nextNode)
                {
                    // Highlight the next node
                    GameObject.Find(nextNode.X + "," + nextNode.Y).GetComponent<Renderer>().material.color = Color.cyan;
                }
                else
                {
                    GameObject.Find(node.X + "," + node.Y).GetComponent<Renderer>().material.color = myColor;
                }
            }

            return nextNode;
        }

        return null;
    }

    private Vector2 GetPosInWorld(GridNode current_node)
    {
        Vector2 position = Vector2.zero;



        return position;
    }

    private bool InFieldOfView(Vector2 node_position, Vector2 staticPosition)
    {
        // Compute the real position of the node based of the staticPosition
        Vector2 endPoint = staticPosition - node_position;

        // Compute the direction between the position and the node.
        Vector2 direction = (node_position - staticPosition);
        // We have to filter the ennemy layer
        int ennemyMask = LayerMask.GetMask("Wall", "MapLimit");

        // Launch a raycast
        RaycastHit2D hit = Physics2D.CircleCast(staticPosition, 0.5f, direction.normalized, direction.magnitude, ennemyMask);

        if (hit.collider != null)
        {
            if (hit.collider.tag != "Wall" || hit.collider.tag != "MapLimit")
            {
                //Debug.DrawLine(staticPosition, staticPosition + direction, Color.blue);
                return false;
            }
            else
            {
                //Debug.DrawLine(staticPosition, staticPosition + direction, Color.red);
                return true;
            }
        }
        else
        {
            //Debug.DrawLine(staticPosition, staticPosition + direction, Color.red);
            return true;
        }
    }

    private IEnumerable<GridNode> GetPathClosestPlayer(AStarAlgorithm<GridNode, System.Object> aStar)
    {
        IEnumerable<GridNode> closestPlayerPath = null;

        // We need to find the player who is the closest to the bot current position
        foreach (MovingObject player in m_owner.Players)
        {
            // Find the current shortest path between the bot and the current tested player.
            IEnumerable<GridNode> path = aStar.Search(new Vector2(m_owner.GetCurrentGridPosition().X, m_owner.GetCurrentGridPosition().Y),
                new Vector2(player.GetGridPosition().X, player.GetGridPosition().Y), null);

            if(closestPlayerPath == null)
            {
                closestPlayerPath = path;
            }
        }
        return closestPlayerPath;
    }
}
