using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject placementIndicator;

    private ARRaycastManager arOrigin;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    public List<GameObject> objectsToPlace;

    bool hasPlacedObject = false;
    PointAndClickController player;

    void Start()
    {
        arOrigin = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (hasPlacedObject)
            {
                //player.SetNewTarget(placementPose.position);
                PlacePoster();
            }
            else
            {
                PlaceAvatar();
            }
        }
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
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            placementIndicator.transform.position = placementPose.position;
        }
        else
        {
            placementIndicator.SetActive(true);
        }
    }

    void PlaceAvatar()
    {
        GameObject newObject = Instantiate(objectsToPlace[0], placementPose.position, placementPose.rotation);
        //player = newObject.GetComponent<PointAndClickController>();
        hasPlacedObject = true;
    }

    void PlacePoster()
    {
        GameObject newObject = Instantiate(objectsToPlace[1], placementPose.position, placementPose.rotation);
        //player = newObject.GetComponent<PointAndClickController>();
        hasPlacedObject = true;
    }
}
