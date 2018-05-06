using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {
	public float m_fireRange = 10f;
	public float m_groundHeight = 0.1f;
	private Transform m_parent;
	private Rigidbody m_rigidBody;
	void Start()
	{
		m_parent = transform.parent;
        m_rigidBody = m_parent.GetComponent<Rigidbody>();
    }
	// Update is called once per frame
	void Update () {
        FindControllerAim();
	}
	void FindControllerAim(){

		Vector3 inputVector = new Vector3(Input.GetAxis("RHorizontal"), 0, Input.GetAxis("RVertical"));
		Vector3 expectedPosition = m_parent.position + inputVector * m_fireRange;

		string[] wantedLayers = { "Default" };
		int layerMask = LayerMask.GetMask(wantedLayers);
		RaycastHit hit;

		if (Physics.Linecast(m_parent.position, expectedPosition, out hit, layerMask))
		{
			Debug.Log(hit.transform.name);
			expectedPosition = hit.point;
		}
		if(inputVector.magnitude > 0){
            transform.rotation = Quaternion.LookRotation(expectedPosition - m_parent.position);
        }
		else{
			transform.rotation = Quaternion.identity;
		}

		expectedPosition.y = m_groundHeight;
		transform.position = expectedPosition;
	}
	void FindMouseAim(){
        // Vector3 foundClickPoint;
        // if (FindClickPoint(out foundClickPoint))
        // {
        //     Vector3 pointToPlayer = foundClickPoint - transform.position;

        //     if (pointToPlayer.sqrMagnitude > m_fireRange * m_fireRange)
        //     {
        //         foundClickPoint = transform.position + pointToPlayer.normalized * m_maxRange;
        //         foundClickPoint.y = 0;
        //         pointToPlayer = foundClickPoint - transform.position;
        //     }

        //     
        // }
	}

    bool FindClickPoint(out Vector3 foundVector)
    {
        bool retVal = false;
        foundVector = new Vector3();
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        string[] wantedLayers = { "Ground", "Default" };
        int layerMask = LayerMask.GetMask(wantedLayers);

        if (Physics.Raycast(mouseRay, out hit, Mathf.Infinity, layerMask))
        {
			if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground")){
                foundVector = hit.point;
                retVal = true;
			}
        }
        // else if (mouseRay.direction.y != 0)
        // {
        //     foundVector = mouseRay.origin + mouseRay.direction * (mouseRay.origin.y / -mouseRay.direction.y);
        //     retVal = true;
        // }
        return retVal;
    }
}
