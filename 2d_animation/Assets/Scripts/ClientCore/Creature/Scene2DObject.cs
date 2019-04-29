using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ClientCore
{
    public class Scene2DObject : SceneGameObject
    {
        TestController m_Controller;
        public Scene2DObject(TestController controller) : base(string.Empty, new Vector3())
        {
            m_Controller = controller;
        }

        public override Vector3 GetPosition()
        {
            return m_Controller.controller.transform.position;
        }
    }
}
