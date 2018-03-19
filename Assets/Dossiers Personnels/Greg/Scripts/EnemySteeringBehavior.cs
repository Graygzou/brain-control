using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySteeringBehavior {

    // Bot calling the script
    private Enemy m_owner;

    // Variable that will contains the reference to the target
    private MovingObject m_target;

    // PathFinding that IA will use
    private PathPlanner aStar;

    private GenerateGrid m_grid;

    // DEBUG
    float x;

    public EnemySteeringBehavior(Enemy owner, GenerateGrid grid) {
        m_owner = owner;
        m_grid = grid;
    }

    // Update is called once per frame.
    public void FixedUpdate (List<Transform> neighboursIA) {

        // Create ONE pathfinder if needed
        if (m_owner.OnFollowPath() && aStar == null)
        {
            // Create the instance that take care of the pathfinding
            aStar = new PathPlanner(m_grid, m_owner);
        }

        // Closest distance
        float closest_distance;
        // Find the player that is our target
        if (m_owner.GetCurrentAnimator().name == "StandardZombie")
        {
            closest_distance = FindClosestPlayer(m_owner.Players);
        }
        else
        {
            List<MovingObject> playerAvailable = new List<MovingObject>();
            // Find the player that is not our leader
            foreach (PlayerCon2d player in m_owner.Players)
            {
                // If the current player is the one we need to aim
                if (player.GetnJoueur() != m_owner.GetnTeam())
                {
                    playerAvailable.Add(player);
                }
            }
            closest_distance = FindClosestPlayer(playerAvailable);
        }

        // Update the target to be the closest player
        if (m_target != null)
        {

            if (closest_distance > m_owner.GetHarmDistance())
            {
                // Compute the activate forces based on the target and the neighbourhood.
                ComputeSteeringForces(neighboursIA);
            }
            else
            {
                float horizontal = m_target.transform.position.x - m_owner.transform.position.x;
                float vertical = m_target.transform.position.y - m_owner.transform.position.y;

                // Look at the player itself
                //m_owner.transform.rotation = Quaternion.Euler(m_owner.transform.rotation.x, m_owner.transform.rotation.y, Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg);
                m_owner.transform.rotation = Quaternion.Euler(m_owner.transform.position.x, m_owner.transform.position.y, Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg);

                m_owner.GetRigidbody().AddForce(-m_owner.GetRigidbody().velocity);
                // deals damage to the player
                m_owner.AttackPlayer(m_target);
            }
        }
    }

    private float FindClosestPlayer(List<MovingObject> availablePlayers)
    {
        // Find the closest player
        MovingObject closest_player = null;
        float closest_distance = 0f;
        foreach (MovingObject player in availablePlayers)
        {
            // Compute the euclidien distance between the bot and the current player
            float current_distance = (player.transform.position - m_owner.transform.position).magnitude;
            if (closest_player == null || current_distance < closest_distance)
            {
                closest_player = player;
                closest_distance = current_distance;
            }
        }
        // Update the target
        m_target = closest_player;
        return closest_distance;
    }



    /// <summary>
    /// Applied Steering force to the rigidbody to make the bot moves
    /// </summary>
    private void ComputeSteeringForces(List<Transform> neighboursIA)
    {
        Vector2 finalVector = CalculatePrioritized(m_target.transform.position, neighboursIA);

        // Maybe clamp the force.
        //Debug.Log(finalVector);

        float horizontal = finalVector.x;
        float vertical = finalVector.y;

        m_owner.transform.rotation = Quaternion.Euler(m_owner.transform.position.x, m_owner.transform.position.y, Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg);

        //DEBUG
        //Debug.DrawLine(m_owner.transform.position, new Vector2(m_owner.transform.position.x, m_owner.transform.position.y) + finalVector, Color.green);

        // Apply the force to the IA
        m_owner.GetRigidbody().AddForce(finalVector);

        // Clamp the rigidbody velocity
        m_owner.GetRigidbody().velocity = Vector2.ClampMagnitude(m_owner.GetRigidbody().velocity, m_owner.GetMaxSpeed());

        // Update the position on the grid of the player
        m_owner.SetGridPosition();

        // This works
        //ownRigidbody.position = Vector3.MoveTowards(ownRigidbody.position, c.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10)), maxSpeed * Time.deltaTime);
    }


    private Vector2 CalculatePrioritized(Vector2 current_target, List<Transform> neighboursIA)
    { 
        Vector2 m_vSteeringForce = Vector2.zero;

        if (m_owner.OnWallAvoidance())
        {
            m_vSteeringForce += WallAvoidance(current_target) * 5.0f;

            //sif (!AccumulateForce(m_vSteeringForce, force)) return m_vSteeringForce;
        }


        //these next three can be combined for flocking behavior (wander is
        //also a good behavior to add into this mix)
        if (m_owner.OnFlocking())
        {
            Debug.Log(neighboursIA.Capacity);

            m_vSteeringForce += Separation(neighboursIA) * 0.6f ;

            m_vSteeringForce += Cohesion(neighboursIA) * 0.6f;

            m_vSteeringForce += Alignment(neighboursIA) * 0.6f;

            //if (!AccumulateForce(m_vSteeringForce, force)) return m_vSteeringForce;
        }
        
        if (m_owner.OnFollowPath())
        {
            GridNode nextNode = aStar.UpdatePath();
            if (nextNode != null)
            {
                // Compute direction
                Vector2 toDir = new Vector2(nextNode.XWorld, nextNode.YWorld) - new Vector2(m_owner.transform.position.x, m_owner.transform.position.y);

                // Create a vector based on the origin of the grid
                m_vSteeringForce += ArriveToPosition(new Vector2(nextNode.XWorld, nextNode.YWorld), 3f) * 1.0f;
            }

            //if (!AccumulateForce(m_vSteeringForce, force)) return m_vSteeringForce;
        }


        if (m_owner.OnSeek())
        {
            m_vSteeringForce += SeekToPosition(current_target) * 0.8f;

            //if (!AccumulateForce(m_vSteeringForce, force)) return m_vSteeringForce;
        }


        if (m_owner.OnArrive())
        {
            m_vSteeringForce += ArriveToPosition(current_target, 2f) * 0.8f;

            //if (!AccumulateForce(m_vSteeringForce, force)) return m_vSteeringForce;
        }

        return m_vSteeringForce;


    }

    /// <summary>
    /// Return the force needed to seek to a position
    /// </summary>
    private Vector2 SeekToPosition(Vector2 target)
    {
        Vector2 m_DesiredVelocity = Vector2.zero;
        m_DesiredVelocity = (target - m_owner.GetRigidbody().position).normalized * m_owner.GetMaxSpeed();

        // Return the force wanted
        return m_DesiredVelocity - m_owner.GetRigidbody().velocity;
    }

    /// <summary>
    /// Return the force needed to arrive to a position
    /// </summary>
    private Vector2 ArriveToPosition(Vector2 target, float deceleration)
    {
        Vector2 m_DesiredVelocity = Vector2.zero;
        Vector2 toTarget = (target - m_owner.GetRigidbody().position).normalized;

        // Compute distance to target
        float distanceToTarget = toTarget.magnitude;

        // Compute a deceleration speed
        float current_Deceleration = distanceToTarget / (deceleration * 0.3f);

        m_DesiredVelocity = toTarget * current_Deceleration / distanceToTarget;

        // Return the force wanted
        return  m_DesiredVelocity - m_owner.GetRigidbody().velocity;
    }

    private Vector2 WallAvoidance(Vector2 target)
    {
        Vector3 m_DesiredVelocity = Vector2.zero;

        float DistToClosestIPFront = 1.5f;
        float DistToClosestIPSide = 0.9f;

        // Compute the direction between the position and the node.
        Vector2 toTarget = (target - m_owner.GetRigidbody().position).normalized;
        // We have to filter the ennemy layer
        int ennemyMask = LayerMask.GetMask("Wall", "MapLim");

        // Create raycast to detect front obstacles
        RaycastHit2D hitFront = Physics2D.Raycast(m_owner.GetRigidbody().position, toTarget.normalized, DistToClosestIPFront, ennemyMask);
        if (hitFront.collider != null)
        {
            if (hitFront.collider.tag != "Wall" || hitFront.collider.tag != "MapLimit")
            {
                // Compute a force that will make the zombie avoid the wall
                m_DesiredVelocity = Vector3.Cross(toTarget, m_owner.transform.forward) * hitFront.distance;

            }
        }
        /*
        // Create raycast to detect left side obstacles
        Vector2 toTargetLeft = Quaternion.Euler(0, 0, 45) * toTarget;
        RaycastHit2D hitLeft = Physics2D.Raycast(m_owner.GetRigidbody().position, toTargetLeft.normalized, DistToClosestIPSide, ennemyMask);
        if (hitLeft.collider != null)
        {
            if (hitLeft.collider.tag != "Wall" || hitLeft.collider.tag != "MapLimit")
            {
                // Compute a force that will make the zombie avoid the wall
                m_DesiredVelocity += Vector3.Cross(toTarget, m_owner.transform.forward) * hitLeft.distance;

            }
        }
        // Create raycast to detect right side obstacles
        Vector2 toTargetRight = Quaternion.Euler(0, 0, -45) * toTarget;
        RaycastHit2D hitRight = Physics2D.Raycast(m_owner.GetRigidbody().position, toTargetRight.normalized, DistToClosestIPSide, ennemyMask);
        if (hitRight.collider != null)
        {
            if (hitRight.collider.tag != "Wall" || hitRight.collider.tag != "MapLimit")
            {
                // Compute a force that will make the zombie avoid the wall
                m_DesiredVelocity += Vector3.Cross(toTarget, m_owner.transform.forward) * hitRight.distance;

            }
        }**/

        return m_DesiredVelocity;
    }

    // ---------------------------------------------
    // Flocking forces
    // ---------------------------------------------

    /// <summary>
    /// This force make the current bot run away from his neighbours.
    /// </summary>
    public Vector2 Separation(List<Transform> neighbours)
    {
        Vector3 desiredVelocity = Vector3.zero;

        // Look if we have a neighbourhood
        if (neighbours.Count > 0)
        {
            foreach (Transform t in neighbours)
            {
                Vector3 directionSeparation = m_owner.transform.position - t.position;
                if (!directionSeparation.Equals(Vector3.zero))
                {
                    Vector3 separationForce = directionSeparation.normalized / directionSeparation.magnitude;
                    desiredVelocity += Vector3.ClampMagnitude(separationForce, m_owner.GetMaxSeparation());
                }
            }
        }
        return desiredVelocity;
    }

    /// <summary>
    /// This force make the current bot run come closer to his neighbours.
    /// </summary>
    public Vector2 Cohesion(List<Transform> neighbours)
    {
        Vector3 desiredVelocity = Vector3.zero;
        Vector3 centerOfMass = Vector3.zero;

        // Compute the center of mass of the group
        foreach(Transform current_transform in neighbours)
        {
            centerOfMass += current_transform.position;
        }

        if (neighbours.Capacity > 0)
        {
            //the center of mass is the average of the sum of positions
            centerOfMass /= neighbours.Capacity;

            //now seek towards that position
            desiredVelocity = SeekToPosition(centerOfMass);
        }

        return desiredVelocity.normalized;
    }

    /// <summary>
    /// This force make the current bot align with his neighbours.
    /// </summary>
    public Vector2 Alignment(List<Transform> neighbours)
    {
        Vector3 AverageHeading = Vector3.zero;

        // Compute average heading of the group
        foreach (Transform current_transform in neighbours)
        {
            AverageHeading += current_transform.right;
        }

        //if the neighborhood contained one or more vehicles, average their
        //heading vectors.
        if (neighbours.Capacity > 0)
        {
            AverageHeading /= neighbours.Capacity;

            AverageHeading -= m_owner.transform.right;
        }

        return Vector3.zero;
    }
}
