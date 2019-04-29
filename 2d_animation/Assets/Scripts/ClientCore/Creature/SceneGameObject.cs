using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ClientCore
{
    public class SceneGameObject
    {
        public GameObject m_GameObject;

        protected string m_Path;
        protected Vector3 m_Position;
        protected bool m_IsAttack;
        protected SceneGameObjectScript m_Script;

        static String[] prefabs =
        {
            "Roles/Heros/aAirElement",
        };
       
        public static SceneGameObject InitOne(Vector3 position, Transform parent)
        {
            var index = UnityEngine.Random.Range(0, prefabs.Length);
            var obj = new Scene3DMonster(prefabs[index], position);
            obj.InstantiateGameObject(parent);
            obj.m_GameObject.SetActive(true);
            return obj;
        }

        public SceneGameObject(string path, Vector3 position)
        {
            m_Path = path;
            m_Position = position;
        }

        public virtual void InstantiateGameObject(Transform parent)
        {
            m_GameObject = RPrefab.Singleton.Load(m_Path, parent);
            m_Script = m_GameObject.AddComponent<SceneGameObjectScript>();
            m_Script.m_Logic = this;
            m_GameObject.transform.position = m_Position;
        }
        
        public virtual bool IsPlaying()
        {
            return false;
        }

        public virtual void Update()
        {
            Debug.Log("Update");
            {
                if (!m_IsAttack)
                {
                    Debug.Log("do idle");
                    DoIdle();
                }
                else
                {
                    Debug.Log("do attack");
                    NormalAttack();
                }
            }
        }

        public virtual void NormalAttack()
        {

        }

        public virtual void DoIdle()
        {

        }

        public virtual void Attack(SceneGameObject target)
        {
            target.BeAttacked(this);
        }

        public virtual Vector3 GetPosition()
        {
            return m_GameObject.transform.position;
        }

        public virtual void BeAttacked(SceneGameObject attacker)
        {
        }
    }
}
