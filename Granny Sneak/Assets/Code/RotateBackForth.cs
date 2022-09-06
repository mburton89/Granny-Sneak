using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateBackForth : MonoBehaviour
{
    public float secondsToRotate;
    public float degreesToRotate;

    void Start()
    {
        StartCoroutine(RotateBackForthCo());    
    }

    private IEnumerator RotateBackForthCo()
    {
        transform.DORotate(new Vector3(0, 0, degreesToRotate), secondsToRotate).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(secondsToRotate);
        transform.DORotate(new Vector3(0, 0, -degreesToRotate), secondsToRotate).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(secondsToRotate);
        StartCoroutine(RotateBackForthCo());
    }
}
