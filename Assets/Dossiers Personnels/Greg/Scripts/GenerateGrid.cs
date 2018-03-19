using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour {

    [Header("Grid configuration")]
    [Tooltip("Maximum width of the grid")]
    [SerializeField]
    private int gridWidth;

    [Tooltip("Maximum width of the grid")]
    [SerializeField]
    private int gridHeight;

    [Tooltip("Space between nodes of the grid")]
    [SerializeField]
    private float spaceBetweenNodes;

    public GameObject gridBox;

    // Structure that will contains the grid
    public GridNode[,] GridPathFinding { get; set; }

    private Collider2D[] limitMapColliders;

    private Collider2D[] wallMapColliders;

    // Use this for initialization
    void Start () {
        // Retrieve all the collider from the LimitMap
        limitMapColliders = GameObject.FindWithTag("MapLimit").GetComponents<Collider2D>();

        // Retrieve all the collider from the wallMap
        wallMapColliders = GameObject.FindWithTag("Wall").GetComponents<Collider2D>();

        // Generate the grid here
        generateGrid(limitMapColliders, wallMapColliders);
    }

    private void generateGrid(Collider2D[] limitMapColliders, Collider2D[] wallMapColliders)
    {
        // Create a 2D grid based on defined size
        GridPathFinding = new GridNode[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                // Create the structure that will be hold in the grid
                GridPathFinding[x, y] = new GridNode()
                {
                    //IsWall = isWall,
                    X = x,
                    Y = y,
                    XWorld = transform.position.x + (x * spaceBetweenNodes),
                    YWorld = transform.position.y + (y * spaceBetweenNodes)
                };

                // Now detect here if the square is reachable or not.
                // Iterate over all the limitaMap colliders and evaluate if the node is into one.
                foreach (Collider2D current_collider in limitMapColliders)
                {
                    if (current_collider.OverlapPoint(new Vector2(GridPathFinding[x, y].XWorld, GridPathFinding[x, y].YWorld)))
                    {
                        this.gameObject.layer = 8;
                        this.tag = "Wall";
                        AddWall(x, y);
                    }
                }

                // Iterate over all the wall colliders and evaluate if the node is into one.
                foreach (Collider2D current_collider in wallMapColliders)
                {
                    if (current_collider.OverlapPoint(new Vector2(GridPathFinding[x, y].XWorld, GridPathFinding[x, y].YWorld)))
                    {
                        AddWall(x, y);
                    }
                }
            }
        }

        //instantiate grid gameobjects to display on the scene
        PrintGrid();
    }

    /*
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Retrieve all the collider from the LimitMap
        limitMapColliders = GameObject.FindWithTag("MapLimit").GetComponents<Collider2D>();

        // Retrieve all the collider from the wallMap
        wallMapColliders = GameObject.FindWithTag("Wall").GetComponents<Collider2D>();

        foreach (Collider2D current_collider in limitMapColliders)
        {
            if (current_collider is PolygonCollider2D)
            {
                PolygonCollider2D poly_collider = current_collider as PolygonCollider2D;
                Vector2[] points = poly_collider.GetPath(0);
                // For all the points that compose the collider check if the current node cut a line segment.
                for (int i = 0; i < points.Length; i++)
                {
                    // Compute the line between the current point and the next one.
                    Gizmos.DrawSphere(current_collider.transform.TransformPoint(new Vector2(points[i].x, points[i].y)), 0.1f);

                    Vector2 line = current_collider.transform.TransformPoint(points[i+1]) - current_collider.transform.TransformPoint(points[i]);

                    //Vector2 line = transform.TransformPoint(points[i]) - transform.TransformPoint(points[i + 1]);
                    Debug.DrawLine(current_collider.transform.TransformPoint(points[i]), current_collider.transform.TransformPoint(points[i+1]), Color.blue, Mathf.Infinity, false);
                }
            }
        }
    }*/

    void PrintGrid()
    {
        //Generate Gameobjects of GridBox to show on the Screen
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                GameObject nobj = (GameObject)GameObject.Instantiate(gridBox);
                // Draw the current node
                nobj.transform.position = new Vector2(
                    GridPathFinding[x, y].XWorld,
                    GridPathFinding[x, y].YWorld
                );
                nobj.name = x + "," + y;

                nobj.gameObject.transform.parent = transform;
                nobj.SetActive(true);

                if(GridPathFinding[x, y].IsWall)
                {
                    nobj.GetComponent<Renderer>().material.color = Color.red;
                    this.gameObject.layer = 8;
                    this.tag = "Wall";
                    //this.GetComponent<Collider2D>().isTrigger = false;
                }
                if(GridPathFinding[x, y].IsWall)
                {
                    nobj.GetComponent<Renderer>().material.color = Color.blue;
                    this.gameObject.layer = 8;
                    this.tag = "Wall";
                    //this.GetComponent<Collider2D>().isTrigger = false;
                }

            }
        }
    }

    public void AddWall(int x, int y)
    {
        GridPathFinding[x, y].IsWall = true;
    }

    public void RemoveWall(int x, int y)
    {
        GridPathFinding[x, y].IsWall = false;
    }

    #region Accessors 

    public float getGridWidth() { return gridWidth; }

    public float getGridHeight() { return gridHeight; }

    public float GetSpaceBetweenNodes() { return spaceBetweenNodes; }

    public Transform GetTransformOrigin() { return transform; }

    #endregion

}
