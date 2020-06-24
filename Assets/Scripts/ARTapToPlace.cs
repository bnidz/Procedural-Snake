using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class ARTapToPlace : MonoBehaviour
{
    public GameObject placementIndicator;
    public ARRaycastManager raycastMan;
    private ARSessionOrigin ARorigin;
    private Pose placementPose;
    private bool placementPoseIsvalid = false;
    private ARPlaneManager planeManager;



    public SnekControls snekCTRL;

    private String planeName;
    private GameObject copyPlane;

    //object already in scene
    public bool snakeInGame = false;

  //  private ARCameraManager arCamMan;

    private GameObject SnekHead;
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
        planeManager = FindObjectOfType<ARPlaneManager>();
        SnekHead = SnekManager.Instance.snekHed;
        ARorigin = FindObjectOfType<ARSessionOrigin>();
        raycastMan = FindObjectOfType<ARRaycastManager>();
     //   arCamMan = FindObjectOfType<ARCameraManager>();
    }

    // Update is called once per  frame
    void Update()
    {

        if(!snakeInGame)
        {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

            if(!IsPointerOverUIObject() && placementPoseIsvalid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                PlaceObject();

            }
        }
    }

    private void PlaceObject()
    {
        snakeInGame = true;
        GameObject snape = Instantiate(SnekHead, placementIndicator.transform.position, placementIndicator.transform.rotation);
        SnekManager.Instance.snekHed = snape;

      //  copyPlane = GameObject.Find(planeName);
      //  Instantiate(copyPlane, new Vector3(copyPlane.transform.position.x, copyPlane.transform.position.y -2f, copyPlane.transform.position.x), placementIndicator.transform.rotation);
        //init right snake to UI controls
        //  snekCTRL.sm = snape.GetComponent<snekmove>();
        snape.SetActive(true);

        //foreach (var plane in planeManager.trackables)
        //{
        //    plane.gameObject.SetActive(false);
        //}

      //  SnekManager.Instance.InstantiateFood();
        SnekManager.Instance.ShowControls();
    }

    private void UpdatePlacementPose()
    {

        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(.5f, .5f));
        var hits = new List<ARRaycastHit>();
        raycastMan.Raycast(screenCenter, hits, TrackableType.Planes); //planes recomended hmm 

        placementPoseIsvalid = hits.Count > 0;

        if (placementPoseIsvalid)
        {
            placementPose = hits[0].pose;
           // planeName = hits[0].ToString();



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
