/********************************************************************
	Copyright (C), 2014-2015 QeFun Corporation.  All rights reserved.

	created: 2015-1-2 17:18
	filename: ClientCore\Src\DataPool\PrefabPool.cs
	file base: PrefabPool
	file ext: cs
	author: baoyuetao

	description: 
*********************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClientCore {
    public class PrefabPool {
        public static GameObject m_RootInstance;
        public static GameObject m_UIRootInstance;

        private string m_Name;
        private GameObject m_Prefab;
        private Queue<GameObject> m_Queue = new Queue<GameObject>();
        private bool m_DontDestroy = false;
        private bool m_IsUIPrefab = false;
        private bool m_DespawnIsDestroy = false;
        //private int m_Index;
        public PrefabPool(string path, string name, bool isUIPrefab = false) {
            m_Name = name;
            m_Prefab = RPrefab.Singleton.Load(path, name);
            if (m_Prefab == null) {
                GlobalToolsFunction.LogError(StringUtils.LinkText("Prefab is null! path:", path, name));
            }
            m_IsUIPrefab = isUIPrefab;
        }

        public PrefabPool(string path, string name, int defaultCount, bool isUIPrefab = false) {
            m_Name = name;
            m_Prefab = RPrefab.Singleton.Load(path, name);
            if (m_Prefab == null) {
                GlobalToolsFunction.LogError(StringUtils.LinkText("Prefab is null! path:", path, name));
            }
            m_IsUIPrefab = isUIPrefab;

            for (int i = 0; i < defaultCount; i++)
                Despawn(Create());
        }

        public PrefabPool(GameObject prefab, bool isUIPrefab = false) {
            if (m_Prefab == null) {
                GlobalToolsFunction.LogError("Prefab is null! ");
            }
            m_Name = prefab.name;
            m_Prefab = prefab;
            m_IsUIPrefab = isUIPrefab;
        }

        public PrefabPool(GameObject prefab, int defaultCount, bool isUIPrefab = false) {
            m_Name = prefab.name;
            m_Prefab = prefab;
            m_IsUIPrefab = isUIPrefab;

            for (int i = 0; i < defaultCount; i++)
                Despawn(Create());
        }

        public static void DontDestroy(PrefabPool pool) {
            pool.DontDestroy();
        }

        public PrefabPool DontDestroy() {
            m_DontDestroy = true;
            return this;
        }

        public static void DespawnIsDestroy(PrefabPool pool) {
            pool.DespawnIsDestroy();
        }

        public PrefabPool DespawnIsDestroy() {
            m_DespawnIsDestroy = true;
            return this;
        }

        public bool CanDestroy() {
            return !m_DontDestroy;
        }

        public GameObject DefaultOne() {
            GameObject go = Create();
            Despawn(go);
            return go;
        }

        public static Transform GetPoolRoot(bool bIsUIPrefab = false) {
            if (!bIsUIPrefab) {
                if (m_RootInstance == null) {
                    m_RootInstance = GameObject.Find("_PrefabPoolInstance");
                    if (m_RootInstance == null) {
                        m_RootInstance = new GameObject("_PrefabPoolInstance");
                        m_RootInstance.transform.parent = null;
                        GameObject.DontDestroyOnLoad(m_RootInstance);
                    }
                }
                return m_RootInstance == null ? null : m_RootInstance.transform;
            }
            else {
                //此处暂时没用，有windowRoot设置过来的 
                if (m_UIRootInstance == null) {
                    m_UIRootInstance = GameObject.Find("_UIPrefabPoolInstance");
                    if (m_UIRootInstance == null) {
                        m_UIRootInstance = new GameObject("_UIPrefabPoolInstance");
                        //m_RootInstance.transform.parent = null;
                        GameObject.DontDestroyOnLoad(m_UIRootInstance);
                    }
                }
                return m_UIRootInstance == null ? null : m_UIRootInstance.transform;
            }
        }
        private GameObject Create() {
            if (m_Prefab == null) {
                return null;
            }

            GameObject instance = RPrefab.Singleton.Instantiate(m_Prefab);
            instance.name = m_Name.Replace("/", "_");// +m_Index++;
            return instance;
        }

        public GameObject Spawn() {
            if (m_Queue.Count != 0) {
                GameObject instance = m_Queue.Dequeue();
                //instance.SetActive(true);
                instance.transform.localPosition = Vector3.zero;

                return instance;
            }

            return Create();
        }
        public void Despawn(GameObject item) {
            if (item != null) {
                if (m_DespawnIsDestroy) {
                    GameObject.Destroy(item);
                } else {
                    item.transform.parent = GetPoolRoot(m_IsUIPrefab);
                    HideItem(item);
                    if (m_IsUIPrefab) {
                        GlobalToolsFunction.SetLayer(item, LayerManager.UI);
                    }
                    //item.SetActive(false);
                    m_Queue.Enqueue(item);
                }
            }
        }

        public void HideItem(GameObject item) {
            item.transform.localPosition = new Vector3(short.MaxValue, short.MaxValue, short.MaxValue);
            item.transform.localRotation = m_Prefab.transform.localRotation;
            item.transform.localScale = m_Prefab.transform.localScale;
        }
        public void Destroy(bool unLoadAll = false) {
            if (!unLoadAll && m_DontDestroy)
                return;

            m_Prefab = null;

            foreach (GameObject current in m_Queue) {
                GameObject.Destroy(current);
            }
            //m_Index = 0;
            m_Queue.Clear();
        }

        public static void Clear() {
            GameObject.Destroy(m_RootInstance);
            m_RootInstance = null;
            GameObject.Destroy(m_UIRootInstance);
            m_UIRootInstance = null;
        }
    }
}
