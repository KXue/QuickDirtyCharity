using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform m_playerTransform;
    public float m_smoothSpeed;
    public float m_viewDistance;
    private Vector3 m_idealOffset;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        float offsetAngle = Mathf.Deg2Rad * transform.eulerAngles.x;
        m_idealOffset = new Vector3();
        m_idealOffset.z = -(m_viewDistance * Mathf.Cos(offsetAngle));
        m_idealOffset.y = (m_viewDistance * Mathf.Sin(offsetAngle));
    }
	
	void LateUpdate () {
        Vector3 idealPosition = m_playerTransform.position + m_idealOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.localPosition, idealPosition, m_smoothSpeed);
        transform.localPosition = smoothedPosition;
        transform.LookAt(m_playerTransform);
	}
}
