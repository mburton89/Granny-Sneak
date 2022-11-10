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
    [SerializeField] Button ThinkButton;

    public float flySpeed;
    bool isFlying;
    Vector3 initialPosition;

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
        ThinkButton.onClick.AddListener(Think);
    }

    void Update()
    {
        if (isFlying)
        {
            animator.transform.localPosition += Vector3.up * flySpeed;
        } 
    }

    void Wave()
    {
        animator.SetTrigger("wave");
        SoundManager.Instance.selectSound.Play();
    }

    void Flex()
    {
        animator.SetTrigger("flex");
        SoundManager.Instance.selectSound.Play();
    }

    void Dribble()
    {
        animator.SetTrigger("dribble");
        SoundManager.Instance.selectSound.Play();
    }

    void Computer()
    {
        animator.SetTrigger("computer");
        SoundManager.Instance.selectSound.Play();
    }

    void Fly()
    {
        animator.SetTrigger("fly");
        SoundManager.Instance.selectSound.Play();
        StartCoroutine(StartFlying());
    }

    void Think()
    {
        animator.SetTrigger("think");
        SoundManager.Instance.selectSound.Play();
    }

    private IEnumerator StartFlying()
    {
        animator.GetComponent<PointAndClickController>().enabled = false;
        initialPosition = animator.transform.localPosition;
        yield return new WaitForSeconds(.5f);
        isFlying = true;
        yield return new WaitForSeconds(5f);
        isFlying = false;
        animator.GetComponent<PointAndClickController>().enabled = true;
        animator.transform.localPosition = initialPosition;
    }
}
