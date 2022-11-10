using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject placementIndicator;

    private ARRaycastManager arOrigin;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    public List<GameObject> objectsToPlace;

    bool hasPlacedObject = false;
    PointAndClickController player;

    public Button homeButton;

    public Button moveButton;
    public Button matthewButton0;
    public Button matthewButton1;
    public Button matthewButton2;
    public Button matthewButton3;
    public Button matthewButton4;
    public Button massigaButton;
    public Button tinyModeButton;
    public GameObject characterButtons;
    public GameObject emotButtons;

    public TextMeshProUGUI hintText;

    bool hasSeenHint2;
    bool hasSeenHint3;
    [HideInInspector] public bool isTinyMode;

    void Start()
    {
        arOrigin = FindObjectOfType<ARRaycastManager>();
        massigaButton.transform.localScale = Vector3.zero;
        characterButtons.transform.localScale = Vector3.zero;
        moveButton.transform.localScale = Vector3.zero;
        emotButtons.transform.localScale = Vector3.zero;
    }

    private void OnEnable()
    {
        homeButton.onClick.AddListener(HandleHomeClicked);
        moveButton.onClick.AddListener(HandleMoveClicked);
        matthewButton0.onClick.AddListener(delegate { HandleCharacterPressed(0);});
        matthewButton1.onClick.AddListener(delegate { HandleCharacterPressed(1);});
        matthewButton2.onClick.AddListener(delegate { HandleCharacterPressed(2);});
        matthewButton3.onClick.AddListener(delegate { HandleCharacterPressed(3);});
        matthewButton4.onClick.AddListener(delegate { HandleCharacterPressed(4);});
        massigaButton.onClick.AddListener(delegate { HandleCharacterPressed(5);});
        tinyModeButton.onClick.AddListener(HandleTinyModeClicked);
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        //if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //{
        //    if (hasPlacedObject)
        //    {
        //        player.SetNewTarget(placementPose.position);
        //    }
        //    else
        //    {
        //        PlaceAvatar();
        //    }
        //}
    }

    void HandleMoveClicked()
    {
        if (placementPoseIsValid)
        {
            if (hasPlacedObject)
            {
                SoundManager.Instance.moveSound.Play();
                player.SetNewTarget(placementPose.position);
                hintText.SetText("");
            }
        }
    }

    void HandleCharacterPressed(int index)
    {
        PlaceAvatar(index);
        SoundManager.Instance.selectSound.Play();
    }

    void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arOrigin.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            if (!hasSeenHint2)
            {
                hasSeenHint2 = true;
                hintText.SetText("Choose Avatar to place on ground");
                massigaButton.transform.localScale = Vector3.one;
                characterButtons.transform.localScale = Vector3.one;
            }
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            placementIndicator.transform.position = placementPose.position;
        }
        else
        {
            placementIndicator.SetActive(true);
        }
    }

    void PlaceAvatar(int objectToPlace)
    {
        if (!hasSeenHint3)
        {
            massigaButton.transform.localScale = Vector3.zero;
            characterButtons.transform.localScale = Vector3.zero;
            moveButton.transform.localScale = Vector3.one;
            emotButtons.transform.localScale = Vector3.one;
            hasSeenHint3 = true;
            hintText.SetText("Point and tap to move avatar");
        }
        GameObject newObject = Instantiate(objectsToPlace[objectToPlace], placementPose.position, placementPose.rotation);
        player = newObject.GetComponent<PointAndClickController>();
        player.SetNewTarget(player.transform.position);
        player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, player.transform.eulerAngles.y + 180, player.transform.eulerAngles.z);
        AnimationManager.Instance.animator = player.GetComponent<Animator>();
        hasPlacedObject = true;

        moveButton.transform.localScale = Vector3.one;
    }

    void PlacePoster()
    {
        GameObject newObject = Instantiate(objectsToPlace[1], placementPose.position, placementPose.rotation);
        hasPlacedObject = true;
    }

    void HandleHomeClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    void HandleTinyModeClicked()
    {
        if (isTinyMode)
        {

            isTinyMode = false;
            player.transform.localScale = Vector3.one;
            player.GetComponent<PointAndClickController>().maxSpeed = 2;
            player.GetComponent<PointAndClickController>().maxDistanceFromCenter = 0.5f;
            tinyModeButton.GetComponent<Image>().color = Color.grey;
            tinyModeButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(1, 1, 1, .5f);
        }
        else
        {
            isTinyMode = true;
            player.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            player.GetComponent<PointAndClickController>().maxSpeed = 1;
            player.GetComponent<PointAndClickController>().maxDistanceFromCenter = 0.15f;
            tinyModeButton.GetComponent<Image>().color = Color.white;
            tinyModeButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        }

        SoundManager.Instance.selectSound.Play();
    }
}
