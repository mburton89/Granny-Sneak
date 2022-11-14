using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bob : MonoBehaviour
{ 
    float initialZPos;
    public float distanceToBob;
    public float secondsToBob = 1;

    void Start()
    {
        initialZPos = transform.localPosition.z;
        StartCoroutine(BobCo());
    }

    private IEnumerator BobCo()
    {
        transform.DOLocalMoveZ(initialZPos + distanceToBob, secondsToBob).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(secondsToBob);
        transform.DOLocalMoveZ(initialZPos, secondsToBob).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(secondsToBob);
        StartCoroutine(BobCo());
    }
}