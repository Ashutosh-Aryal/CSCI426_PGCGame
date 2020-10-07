using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject;

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = gameObject.transform.position;
        currentPosition.y = playerObject.transform.position.y;
        gameObject.transform.position = currentPosition;
    }
}
