using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneral : MonoBehaviour
{
    public GameObject p;
    // Start is called before the first frame update
    void Start()
    {   
        for (int i = -5; i <  5; ++i)
        {
            for (int j = -5; j < 5; ++j)
            {
                var tt = GameObject.Instantiate(p);
                tt.transform.position = new Vector3(i, j, 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
