using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager Instance;

    [HideInInspector] public Animator animator;

    [SerializeField] Button WaveButton;
    [SerializeField] Button ThumbButton;
    [SerializeField] Button PointButton;
    [SerializeField] Button FlexButton;
    [SerializeField] Button BaseballButton;
    [SerializeField] Button DribbleButton;
    [SerializeField] Button ComputerButton;
    [SerializeField] Button ThinkButton;
    [SerializeField] Button FlyButton;
    [SerializeField] Button FlipButton;
    [SerializeField] Button DanceButton;
    [SerializeField] Button TalkButton;


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
        ThumbButton.onClick.AddListener(Thumb);
        PointButton.onClick.AddListener(Point);
        FlexButton.onClick.AddListener(Flex);
        BaseballButton.onClick.AddListener(Baseball);
        DribbleButton.onClick.AddListener(Dribble);
        ComputerButton.onClick.AddListener(Computer);
        ThinkButton.onClick.AddListener(Think);
        FlyButton.onClick.AddListener(Fly);
        FlipButton.onClick.AddListener(Flip);
        DanceButton.onClick.AddListener(Dance);
        TalkButton.onClick.AddListener(Talk);
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

    void Thumb()
    {
        animator.SetTrigger("thumb");
        SoundManager.Instance.selectSound.Play();
    }

    void Point()
    {
        animator.SetTrigger("point");
        SoundManager.Instance.selectSound.Play();
    }

    void Flex()
    {
        animator.SetTrigger("flex");
        SoundManager.Instance.selectSound.Play();
    }

    void Baseball()
    {
        animator.SetTrigger("baseball");
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

    void Think()
    {
        animator.SetTrigger("think");
        SoundManager.Instance.selectSound.Play();
    }

    void Fly()
    {
        animator.SetTrigger("fly");
        SoundManager.Instance.selectSound.Play();
        StartCoroutine(StartFlying());
    }

    void Flip()
    {
        animator.SetTrigger("flip");
        SoundManager.Instance.selectSound.Play();
    }

    void Dance()
    {
        animator.SetTrigger("dance");
        SoundManager.Instance.selectSound.Play();
    }

    void Talk()
    {
        animator.SetTrigger("talk");
        SoundManager.Instance.PlayRandomQuote();
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
