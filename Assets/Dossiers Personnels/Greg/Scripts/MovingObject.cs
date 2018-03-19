using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    // Vector used to represent the bot in the grid.
    protected GridNode m_currentGridPosition;
    // Get an instance of the grid
    protected GenerateGrid m_grid;
    protected Rigidbody2D rb2d;
    protected Animator m_ownAnimator;
    protected bool isDead = false;

    #region Tweakable variables

    [SerializeField]
    protected GameObject deadBodySprite;
    [SerializeField]
    protected float baseSpeed;

    #endregion

    public void InitializePosition()
    {
        // Get the grid manually
        //m_grid = GameObject.Find("PathFindingGrid").GetComponent<GenerateGrid>();

        // We assume the transform of this object is set to the spawner position
        //SetGridPosition();
    }

    public virtual void Death() {
        isDead = true;
    }

    public bool IsAlive()
    {
        return isDead;
    }

    public virtual void Revive(int team)
    {
        isDead = false;
    }

    #region Accessors

    public Animator GetCurrentAnimator()
    {
        return m_ownAnimator;
    }

    public GridNode GetGridPosition()
    {
        return m_currentGridPosition;
    }

    public void SetGridPosition()
    {
        /*
        // Approximate the current position of the bot on the grid.
        int x = (int)((transform.position.x - m_grid.GetTransformOrigin().position.x) / m_grid.GetSpaceBetweenNodes());
        int y = (int)((transform.position.y - m_grid.GetTransformOrigin().position.y) / m_grid.GetSpaceBetweenNodes());

        // Set the final position
        m_currentGridPosition = new GridNode
        {
            X = x,
            Y = y,
            XWorld = transform.position.x,
            YWorld = transform.position.y
        };*/
    }

    #endregion
}
