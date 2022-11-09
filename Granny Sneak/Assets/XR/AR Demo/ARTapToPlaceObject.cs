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

    public Button moveButton;
    public Button matthewButton;
    public Button massigaButton;
    public GameObject emotButtons;

    public TextMeshProUGUI hintText;

    bool hasSeenHint2;
    bool hasSeenHint3;

    void Start()
    {
        arOrigin = FindObjectOfType<ARRaycastManager>();
        massigaButton.transform.localScale = Vector3.zero;
        matthewButton.transform.localScale = Vector3.zero;
        moveButton.transform.localScale = Vector3.zero;
        emotButtons.transform.localScale = Vector3.zero;
    }

    private void OnEnable()
    {
        moveButton.onClick.AddListener(HandleMoveClicked);
        matthewButton.onClick.AddListener(HandleMatthewPressed);
        massigaButton.onClick.AddListener(HandleMassigaPressed);
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
                player.SetNewTarget(placementPose.position);
                hintText.SetText("");
            }
        }
    }

    void HandleMassigaPressed()
    {
        PlaceAvatar(0);
    }

    void HandleMatthewPressed()
    {
        PlaceAvatar(1);
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
                matthewButton.transform.localScale = Vector3.one;
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
            matthewButton.transform.localScale = Vector3.zero;
            moveButton.transform.localScale = Vector3.one;
            emotButtons.transform.localScale = Vector3.one;
            hasSeenHint3 = true;
            hintText.SetText("Point and tap to move avatar");
        }
        GameObject newObject = Instantiate(objectsToPlace[objectToPlace], placementPose.position, placementPose.rotation);
        player = newObject.GetComponent<PointAndClickController>();
        player.SetNewTarget(player.transform.position);
        AnimationManager.Instance.animator = player.GetComponent<Animator>();
        hasPlacedObject = true;

        moveButton.transform.localScale = Vector3.one;
    }

    void PlacePoster()
    {
        GameObject newObject = Instantiate(objectsToPlace[1], placementPose.position, placementPose.rotation);
        hasPlacedObject = true;
    }
}
