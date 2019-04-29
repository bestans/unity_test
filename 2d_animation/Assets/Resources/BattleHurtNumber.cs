using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BattleHurtNumber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //transform.DOShakePosition(1, new Vector3(3, 3, 0), 10, 90);
        //transform.DOMove(new Vector3(3, 0, 0), 3.0f);
        //transform.DOLocalMove(new Vector3(10, 0, 0), 3.0f);
        //transform.DOShakePosition(1, new Vector3(3, 3, 0), 10, 90);   //震动
        //transform.DOPunchPosition(Vector3.up, 10, 10, 0.5f); //抖动
        //Animation3();
    }


    //渐变同时上漂
    private void Animation1()
    {
        transform.DOLocalMoveY(10, 3);
        var text = GetComponent<Text>();
        text.DOFade(0.0001f, 3);
    }

    private void Animation2()
    {

        var sequence = DOTween.Sequence();
        var rect = GetComponent<RectTransform>();
        sequence.Append(rect.DOScale(new Vector3(2, 2, 2), 2));
        sequence.Append(transform.DOLocalMoveY(100, 3));
        sequence.Join(GetComponent<Text>().DOFade(0, 3));
    }

    public void Animation3()
    {
        var sequence = DOTween.Sequence();
        var rect = GetComponent<RectTransform>();
        sequence.Append(rect.DOLocalMove(new Vector3(-41, 49, 0), 0.3f));
        sequence.Join(rect.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f));
        sequence.Append(rect.DOLocalMove(new Vector3(-82, 98, 0), 0.5f));
        sequence.Insert(0.1f, rect.DOScale(new Vector3(0.4f, 0.4f, 1), 0.5f));
        sequence.Append(rect.DOLocalMove(new Vector3(-268, -20, 0), 0.5f));
        sequence.Join(GetComponent<Text>().DOFade(0, 0.5f));
        sequence.AppendCallback(OnComplete);
    }
    private void OnComplete()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
