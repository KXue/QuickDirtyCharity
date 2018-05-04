using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour {
    void LateUpdate()
    {
        // cameraPosition.y = transform.position.y;
        float cameraY = Camera.main.transform.rotation.eulerAngles.y;
        float cameraX = Camera.main.transform.rotation.eulerAngles.x;
        Vector3 newRotation = transform.rotation.eulerAngles;
        newRotation.y = cameraY;
        newRotation.x = cameraX;
        transform.rotation = Quaternion.Euler(newRotation);
    }
}
