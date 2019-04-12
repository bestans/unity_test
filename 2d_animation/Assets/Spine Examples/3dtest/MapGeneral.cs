using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneral : MonoBehaviour
{
    public GameObject p;
    // Start is called before the first frame update
    void Start()
    {
        int n = 10;
        for (int i = -n; i <  n; i+=1)
        {
            for (int j = -n; j < n; j+=1)
            {
                var tt = GameObject.Instantiate(p);
                tt.transform.position = new Vector3(i, j, 0);

                if (i == n-1 || j == n - 1 || i == -n || j == -n)
                {
                    var tt1 = GameObject.Instantiate(p);
                    tt1.transform.position = new Vector3(i, j, -1);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
