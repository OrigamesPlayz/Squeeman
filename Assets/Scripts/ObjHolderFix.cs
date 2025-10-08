using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjHolderFix : MonoBehaviour
{
    public Transform object1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.SetParent(object1);
        transform.localPosition = new Vector3(0.334f, -0.4529999f, 0);
    }
}
