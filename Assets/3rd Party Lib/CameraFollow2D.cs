using System;
using UnityEngine;
using Prime31;
using System.Collections.Generic;

public class CameraFollow2D : MonoBehaviour
{
	public Transform target;

	public float damping = 0.1f;
	public float lookAheadFactor = 0.5f;
	public float lookAheadReturnSpeed = 0.5f;
	public float lookAheadMoveThreshold = 0.1f;

	public bool platformSnap = true;

    private float m_OffsetZ;
    private Vector3 m_LastTargetPosition;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_LookAheadPos;

	private Transform currentTarget;

    /// <summary>
    /// Holds the set of nodes that make up the area in which the camera can move freely
    /// </summary>
    [Header("Area Elements")]
    [SerializeField]
    private int currentArea = 0;
    [SerializeField]
    private GameObject AreaNodes;

    private Rect areaOfCameraMovement;

    // Use this for initialization
    private void Start()
    {
		if (AreaNodes == null)
        {
            Debug.LogError("AreaNodes array for " + this.gameObject.name + " camera is null" );
        }
        else
        {
            Transform firstPoint = AreaNodes.transform.GetChild(0);
            Transform secondPoint = AreaNodes.transform.GetChild(1);

            Vector2 sizeOfRect = new Vector2(secondPoint.position.x - firstPoint.position.x, secondPoint.position.y - firstPoint.position.y);

            areaOfCameraMovement = new Rect(firstPoint.position, sizeOfRect);
        }
        
    }

    // Update is called once per frame
    private void Update()
    {
        // only update lookahead pos if accelerating or changed direction
		float xMoveDelta = (currentTarget.position - m_LastTargetPosition).x;

        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

        if (updateLookAheadTarget)
        {
            m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
        }
        else
        {
            m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
        }

		Vector3 aheadTargetPos = currentTarget.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;

        if (AreaNodes != null)
        {
            aheadTargetPos.x = Mathf.Clamp(target.position.x,
                AreaNodes.transform.GetChild(0).position.x + getCameraDimensions(),
                AreaNodes.transform.GetChild(1).position.x + getCameraDimensions());

            aheadTargetPos.y = Mathf.Clamp(target.position.y,
                AreaNodes.transform.GetChild(1).position.y + getCameraDimensions(),
                AreaNodes.transform.GetChild(0).position.y + getCameraDimensions());
        }

        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

        transform.position = newPos;

		m_LastTargetPosition = currentTarget.position;
    }

	public void startCameraFollow (GameObject newTarget){
		currentTarget = newTarget.transform;
		m_LastTargetPosition = currentTarget.position;
        m_OffsetZ = transform.position.z;
        transform.parent = null;
	}
	
	
	public void stopCameraFollow (){
		currentTarget = this.transform;
		m_LastTargetPosition = currentTarget.position;
		m_OffsetZ = 0;
	}

	public void setDamping (float value){
		damping = value;
	}

	public void setTarget (GameObject newTarget){
		currentTarget = newTarget.transform;
	}

    private float getCameraDimensions()
    {
        return GetComponent<Camera>().orthographicSize * GetComponent<Camera>().aspect;
    }

    // Draws area of movement for the camera
    private void OnDrawGizmos()
    {
        if (AreaNodes == null)
            return;

        // Draw the current selected area's bounding box
        Transform i = AreaNodes.transform.GetChild(0);
        Transform j = AreaNodes.transform.GetChild(1);

        Gizmos.color = Color.green;

        Gizmos.DrawLine(new Vector2(i.position.x, i.position.y), new Vector2(j.position.x, i.position.y));
        Gizmos.DrawLine(new Vector2(i.position.x, j.position.y), new Vector2(j.position.x, j.position.y));
        Gizmos.DrawLine(new Vector2(i.position.x, i.position.y), new Vector2(i.position.x, j.position.y));
        Gizmos.DrawLine(new Vector2(j.position.x, i.position.y), new Vector2(j.position.x, j.position.y));
}
}

