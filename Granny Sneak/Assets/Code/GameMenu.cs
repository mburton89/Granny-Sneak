using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameMenu : MonoBehaviour
{
    public static GameMenu Instance;
    public RectTransform menuPanel;
    public Button menuButton;
    public Button containerButton;
    public float secondsToMovePanel;

    public float panelVisibleXPosition = -5;
    public float panelHiddenXPosition = 210;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        menuPanel.anchoredPosition = new Vector2(panelHiddenXPosition, 0);
    }

    private void OnEnable()
    {
        menuButton.onClick.AddListener(OpenMenu);
        containerButton.onClick.AddListener(CloseMenu);
    }

    public void OpenMenu()
    {
        menuPanel.DOAnchorPosX(panelVisibleXPosition, secondsToMovePanel).SetEase(Ease.OutBack);
        containerButton.gameObject.SetActive(true);
        containerButton.image.DOFade(.25f, secondsToMovePanel);
    }

    public void CloseMenu()
    {
        menuPanel.DOAnchorPosX(panelHiddenXPosition, secondsToMovePanel).SetEase(Ease.OutBack);
        containerButton.gameObject.SetActive(false);
        containerButton.image.DOFade(0f, secondsToMovePanel);
    }
}
