using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ClientCore;

public class SceneGameObjectScript : MonoBehaviour
{
    public SceneGameObject m_Logic;

    void Update()
    {
        if (m_Logic != null)
        {
            m_Logic.Update();
        }
    }
}
