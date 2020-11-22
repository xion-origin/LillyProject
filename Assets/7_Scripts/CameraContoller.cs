using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject TargetObject;
    Vector3 CamTransform;
    Transform test;

    void Start()
    {
        test = TargetObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(test.position.x, this.transform.position.y, this.transform.position.z);
    }
}
