using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager Instance;

    [HideInInspector] public Animator animator;

    [SerializeField] Button WaveButton;
    [SerializeField] Button FlexButton;
    [SerializeField] Button DribbleButton;
    [SerializeField] Button ComputerButton;
    [SerializeField] Button FlyButton;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        WaveButton.onClick.AddListener(Wave);
        FlexButton.onClick.AddListener(Flex);
        DribbleButton.onClick.AddListener(Dribble);
        ComputerButton.onClick.AddListener(Computer);
        FlyButton.onClick.AddListener(Fly);
    }

    void Wave()
    {
        animator.SetTrigger("wave");
    }

    void Flex()
    {
        animator.SetTrigger("flex");
    }

    void Dribble()
    {
        animator.SetTrigger("dribble");
    }

    void Computer()
    {
        animator.SetTrigger("computer");
    }

    void Fly()
    {
        animator.SetTrigger("fly");
    }
}
