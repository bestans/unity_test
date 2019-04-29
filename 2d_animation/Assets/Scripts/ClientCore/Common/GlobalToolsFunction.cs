using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ClientCore
{
    public class GlobalToolsFunction
    {
        public static void LogError(string s)
        {
            Debug.LogError(s);
        }

        /// <summary>
        /// 设置对象的层级，默认包含所有的子，不可见的子一样设置
        /// </summary>
        /// <param name="go">目标对象</param>
        /// <param name="layer">目标层级</param>
        /// <param name="includeChild">是否含子</param>
        public static void SetLayer(GameObject go, int layer, bool includeChild = true)
        {
            go.layer = layer;

            if (includeChild)
            {
                Transform t = go.transform;
                for (int i = 0, imax = t.childCount; i < imax; ++i)
                {
                    Transform child = t.GetChild(i);
                    SetLayer(child.gameObject, layer);
                }
            }
        }
    }
}
