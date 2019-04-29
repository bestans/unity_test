using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationPlay : MonoBehaviour {
    public string[] m_NormalAnimationList;
    public string m_SpecialAnimation;
    public int m_SpecailAnimationRate = 0;

    private Animation m_Animation = null;
    private float m_EndTime = 0.0f;
    private int m_RealNormalAnimationCount = 0;

    void Start() {
        m_Animation = gameObject.GetComponentInChildren<Animation>();
        if (m_NormalAnimationList == null) {
            m_RealNormalAnimationCount = 0;
            return;
        }
        m_RealNormalAnimationCount = m_NormalAnimationList.Length;
        for (int i = 0; i < m_NormalAnimationList.Length; ++i) {
            if (m_NormalAnimationList[i] == null || m_NormalAnimationList[i] == string.Empty) {
                m_RealNormalAnimationCount = i;
                break;
            }
        }
    }

    void Update()
    {
        if (!m_Animation.isPlaying)
        {
            Debug.Log("play idle");
            m_Animation.Play("Attack01");
        }
    }
    void RightUpdate() {
        if (!IsAnimationEnd())
            return;
        if (m_Animation == null)
            return;
        string validName = GetAnimationName();
        if (validName == null || validName == string.Empty)
            return;
        m_Animation.CrossFade(validName);
        m_EndTime = Time.time + m_Animation[validName].length;
    }

    bool IsAnimationEnd() {
        return Time.time > m_EndTime;
    }

    string GetAnimationName() {
        if (Random.Range(0, 100) < m_SpecailAnimationRate)
            return m_SpecialAnimation;

        if (m_RealNormalAnimationCount <= 0)
            return m_SpecialAnimation;

        int index = Random.Range(0, m_RealNormalAnimationCount);
        return m_NormalAnimationList[index];
    }
}