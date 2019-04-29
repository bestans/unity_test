using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ClientCore
{
    public class Scene3DMonster : SceneGameObject
    {
        const string NORMAL_ATTACK = "Attack01";

        private Animation m_Animation;

        public Scene3DMonster(string path, Vector3 position) : base(path, position)
        {
        }
        
        public override void InstantiateGameObject(Transform parent)
        {
            base.InstantiateGameObject(parent);
            m_Animation = m_GameObject.GetComponentInChildren<Animation>();
        }

        public override void Update()
        {
            base.Update();
        }

        public override bool IsPlaying()
        {
            return m_Animation.isPlaying;
        }

        public override void NormalAttack()
        {
            PlayAnimation(NORMAL_ATTACK);
        }

        public override void DoIdle()
        {
            PlayAnimation("Idle");
        }

        private void PlayAnimation(string name)
        {
            Debug.Log("PlayAnimation:" + name);
            if (!m_Animation.IsPlaying(name))
            {
                Debug.Log("PlayAnimation success:" + name);
                m_Animation.Play(name);
            }
        }

        public override void BeAttacked(SceneGameObject attacker)
        {
            var my = GetPosition();
            var other = attacker.GetPosition();
            var offset = other - my;
            offset.Normalize();
            int yRotation = 0;
            if (offset.x > 0)
            {
                if (offset.y > 0 && offset.y > offset.x)
                    yRotation = 270;
                else if (offset.y < 0 && -offset.y > offset.x)
                    yRotation = 90;
            }
            else
            {
                if (offset.y > 0 && offset.y > -offset.x)
                    yRotation = 270;
                else if (offset.y < 0 && -offset.y > -offset.x)
                    yRotation = 90;
                else
                    yRotation = 180;
            }
            var original = m_GameObject.transform.localEulerAngles;
            var oldy = original.y;
            original.y = yRotation;

            Debug.Log("BeAttacked:x=" + offset.x + ",y=" + offset.y + ",rotation=" + yRotation + ",oldy=" + oldy);
            m_GameObject.transform.localEulerAngles = original;

            m_IsAttack = true;
        }
    }
}
