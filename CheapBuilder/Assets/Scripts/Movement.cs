using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public KeyCode forwards = KeyCode.W;
    public KeyCode backwards = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode up = KeyCode.Q;
    public KeyCode down = KeyCode.E;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(forwards))
            gameObject.transform.position += Vector3.forward * 5.0f;
        if (Input.GetKeyDown(backwards))
            gameObject.transform.position -= Vector3.forward * 5.0f;
        if (Input.GetKeyDown(left))
            gameObject.transform.position -= Vector3.right * 5.0f;
        if (Input.GetKeyDown(right))
            gameObject.transform.position += Vector3.right * 5.0f;
        if (Input.GetKeyDown(up))
            gameObject.transform.position += Vector3.up * 5.0f;
        if (Input.GetKeyDown(down))
            gameObject.transform.position -= Vector3.up * 5.0f;



    }
}
