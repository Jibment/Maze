using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlAction : MonoBehaviour
{
    // MapGenerator mapGenerator;
    // Start is called before the first frame update
    public List<int> actions = new List<int>();

    void Start()
    {
        //mapGenerator = transform.parent.GetComponent<MapGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Debug.Log(mousePosition);
        }
    }
}
