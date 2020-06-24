using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using System;
using UnityEngine.EventSystems;


public class ARTapToPlace : MonoBehaviour
{
    public GameObject placementIndicator;
    public ARRaycastManager raycastMan;
    private ARSessionOrigin ARorigin;
    private Pose placementPose;
    private bool placementPoseIsvalid = false;

  //  private ARCameraManager arCamMan;

    public GameObject objecToPlace;
    //UI detection
    private bool IsPointerOverUIObject()
    {
        // Referencing this code for GraphicRaycaster https://gist.github.com/stramit/ead7ca1f432f3c0f181f
        // the ray cast appears to require only eventData.position.
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        if (results.Count > 0) return results[0].gameObject.tag == "excludeUiTouch"; else return false;
    }


    // Start is called before the first frame update
    void Start()
    {
        ARorigin = FindObjectOfType<ARSessionOrigin>();
        raycastMan = FindObjectOfType<ARRaycastManager>();
     //   arCamMan = FindObjectOfType<ARCameraManager>();
    }

    // Update is called once per  frame
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if(!IsPointerOverUIObject() && placementPoseIsvalid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
        }
    }

    private void PlaceObject()
    {
    GameObject snape = Instantiate(objecToPlace, placementIndicator.transform.position, placementIndicator.transform.rotation);
        snape.SetActive(true);
        FoodSpawnScript.Instance.InstantiateFood();
    }

    private void UpdatePlacementPose()
    {

        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(.5f, .5f));
        var hits = new List<ARRaycastHit>();
        raycastMan.Raycast(screenCenter, hits, TrackableType.FeaturePoint); //planes recomended hmm 

        placementPoseIsvalid = hits.Count > 0;

        if (placementPoseIsvalid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;

            placementPose.rotation = Quaternion.LookRotation(cameraBearing);

        }

      //  ARorigin.Raycast(screenCenter, hits, TrackableType.Planes);
    }

    private void  UpdatePlacementIndicator()
    {
        if(placementPoseIsvalid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
             
        }else
        {
            placementIndicator.SetActive(false); 
        }
    }

}
