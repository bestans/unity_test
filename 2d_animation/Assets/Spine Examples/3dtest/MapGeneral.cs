using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClientCore;

public class MapGeneral : MonoBehaviour
{
    public GameObject p;
    public Transform parent;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        var obj = SceneGameObject.InitOne(new Vector3(0, -3, 0), parent);
        MonsterManager.Singleton.Add(0, -3, obj);
        //obj.m_GameObject.transform.parent = parent;
        int n = 10;
        for (int i = -n; i <  n; i+=1)
        {
            for (int j = -n; j < n; j+=1)
            {
                var tt = GameObject.Instantiate(p);
                tt.transform.position = new Vector3(i, j, 0.5f);

                if (i == n-1 || j == n - 1 || i == -n || j == -n)
                {
                    var tt1 = GameObject.Instantiate(p);
                    tt1.transform.position = new Vector3(i, j, -0.5f);
                }
            }
        }
        //var obj2 = Resources.Load("Prefabs/Roles/Heros/aAirElement");
        //Instantiate(obj2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
