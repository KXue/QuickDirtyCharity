using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BillBoard : MonoBehaviour {
    public float m_parentHeightFromGround;
    void LateUpdate()
    {
        // cameraPosition.y = transform.position.y;
        float cameraY = Camera.main.transform.rotation.eulerAngles.y;
        float cameraX = Camera.main.transform.rotation.eulerAngles.x;
        Vector3 newRotation = transform.rotation.eulerAngles;
        newRotation.y = cameraY;
        newRotation.x = cameraX;
        transform.rotation = Quaternion.Euler(newRotation);
        TouchGround();
    }
    void TouchGround(){
        Vector3 newPosition = new Vector3();
        newPosition.y = -(m_parentHeightFromGround - transform.localScale.y * 0.5f * Mathf.Cos(Mathf.Deg2Rad * transform.localEulerAngles.x));
        newPosition.z = transform.localScale.y * 0.5f * Mathf.Sin(Mathf.Deg2Rad * transform.localEulerAngles.x);
        transform.position = transform.parent.position + newPosition;
    }
}
