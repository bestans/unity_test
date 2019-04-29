using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHurtNumber : MonoBehaviour
{
    public GameObject t;
    public Transform p;

    private float last_time = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - last_time >= 0.5)
        {
            var newt = GameObject.Instantiate(t, p);
            newt.GetComponent<BattleHurtNumber>().Animation3();
            last_time = Time.time;
        }
    }
}
