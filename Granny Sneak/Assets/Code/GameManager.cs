using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Image sceneTransition;
    public float secondsToFade = 0.25f;
    public Transform gameOverText;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        sceneTransition.DOFade(1, 0);
        sceneTransition.DOFade(0, secondsToFade);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCo());
    }

    private IEnumerator GameOverCo()
    {
        gameOverText.DOScale(1, 1).SetEase(Ease.OutElastic);
        yield return new WaitForSeconds(2f);
        sceneTransition.DOFade(1, secondsToFade);
        yield return new WaitForSeconds(secondsToFade * 2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
