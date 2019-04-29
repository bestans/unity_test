
namespace ClientCore {
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
    //
    public class RPrefab : RResource<RPrefab> {
        private Dictionary<string, GameObject> m_Pool = new Dictionary<string, GameObject>();
        //
        public const string kPrefabPath = "Prefabs/";
        private RPrefab() {

        }
        public GameObject Find(string tagname) {
            GameObject[] result = GameObject.FindGameObjectsWithTag(tagname);
            if (result.Length != 1) {
                return null;
            }
            return result[0];
        }
        public GameObject Find(string tagname, string name) {
            GameObject[] result = GameObject.FindGameObjectsWithTag(tagname);
            if (result.Length <= 0) {
                return null;
            }
            for (int i = 0; i < result.Length; ++i) {
                if (result[i].gameObject.name == name) {
                    return result[i];
                }
            }
            return null;
        }

        public GameObject Load(string name)
        {
            return Load(kPrefabPath, name, null);
        }
        // 加载Prefab到内存 
        public GameObject Load(string name, Transform parent) {
            return Load(kPrefabPath, name, parent);
        }
        public GameObject Load(string path, string name)
        {
            return Load(path, name, null);
        }
        public GameObject Load(string path, string name, Transform parent) {
            string fullname = Fullname(path, name);
            if (!m_Pool.TryGetValue(fullname, out GameObject value))
            {
                value = base.Load(fullname, typeof(GameObject)) as GameObject;
                m_Pool.Add(fullname, value);
            }
            
            var newOne = Instantiate(value, parent) as GameObject;
            return newOne;
        }

        /// <summary>
        /// 实例一个load好的gameobjcet
        /// </summary>
        /// <param name="prefab">load好的对象</param>
        /// <param name="parent">父对象</param>
        /// <returns>实例</returns>
        public GameObject Instantiate(GameObject prefab, Transform parent) {
            if (prefab == null) return null;

            GameObject go = GameObject.Instantiate(prefab) as GameObject;

            go.transform.parent = parent;

            go.transform.localPosition = prefab.transform.localPosition;
            go.transform.localRotation = prefab.transform.localRotation;
            go.transform.localScale = prefab.transform.localScale;

            return go;
        }

        // 实例Prefab到场景 
        public GameObject Instantiate(string name) {
            return Instantiate(Load(name));
        }

        public GameObject Instantiate(string path, string name) {
            return Instantiate(Load(path, name));
        }

        public GameObject Instantiate(string name, Transform parent) {
            return Instantiate(Load(name), parent);
        }

        public GameObject Instantiate(string path, string name, Transform parent) {
            return Instantiate(Load(path, name), parent);
        }

        public GameObject Instantiate(Object original) {
            if (original != null) {
                return GameObject.Instantiate(original) as GameObject;
            }

            return null;
        }

        public GameObject Instantiate(Object original, Vector3 position, Quaternion rotation) {
            if (original != null) {
                return GameObject.Instantiate(original, position, rotation) as GameObject;
            }

            return null;
        }

        public GameObject Instantiate(Object original, Transform parent) {
            if (original != null) {
                GameObject obj = GameObject.Instantiate(original) as GameObject;
                //GameObject obj = GameObject.Instantiate(original, parent.position, parent.rotation) as GameObject;

                obj.transform.parent = parent;
                obj.transform.localScale = new Vector3(1f, 1f, 1f);

                return obj;
            }

            return null;
        }

        // 消耗对象 
        public void Destroy(Object obj) {
            GameObject.Destroy(obj);
            //GameObject.DestroyObject(obj);
            //GameObject.DestroyImmediate(obj);
        }
    }
}