﻿using UnityEngine;
using System.Collections;

public class DragPiece : MonoBehaviour
{
    public float dampingRatio = 5.0f;
    public float frequency = 2.5f;
    public float drag = 10.0f;
    public float angularDrag = 5.0f;

    private SpringJoint springJoint;

    void Update()
    {
        //
        // If the player did not press the mouse button down, do not run
        // through Update().
        //
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        Camera camera = FindCamera();
        RaycastHit hit = Physics.Raycast(
                camera.ScreenToWorldPoint(Input.mousePosition),
                Vector3.zero);

        //
        // Prerequisites for dragging a GameObject. Should be
        // self-explanatory, I hope!
        //
        if (hit.collider == null || !hit.rigidbody || hit.rigidbody.isKinematic)
        {
            return;
        }

        //
        // SpringJoint2D creation.
        //
        if (!springJoint)
        {
            GameObject obj = new GameObject("Rigidbody dragger");
            Rigidbody body = obj.AddComponent("Rigidbody") as Rigidbody;
            this.springJoint = obj.AddComponent("SpringJoint") as SpringJoint;
            body.isKinematic = true;
        }

        //
        // SpringJoint2D property setting.
        //
        springJoint.transform.position = hit.point;
        // Spring endpoint, set to the position of the hit object:
        springJoint.anchor = Vector3.zero;
        // Initially, both spring endpoints are the same point:
        springJoint.connectedAnchor = hit.transform.InverseTransformPoint(hit.point);
        springJoint.dampingRatio = this.dampingRatio;
        springJoint.frequency = this.frequency;
        // Don't want our invisible "Rigidbody2D dragger" to collide!
        springJoint.collideConnected = false;
        springJoint.connectedBody = hit.rigidbody;

        //
        // Keep in mind that the if statement at the beginning of this Update()
        // only runs through if the player presses the mouse button down.
        //
        StartCoroutine(DragObject());
    }

    IEnumerator DragObject()
    {
        //
        // Save the drag and angular drag of the hit rigidbody, since this
        // script has a drag and angular drag of its own. We don't want the
        // rigidbody to fly to our position too quickly!
        //
        float oldDrag = this.springJoint.connectedBody.drag;
        float oldAngularDrag = this.springJoint.connectedBody.angularDrag;

        springJoint.connectedBody.drag = drag;
        springJoint.connectedBody.angularDrag = angularDrag;

        //
        // The spring joint's position becomes 
        //
        Camera camera = FindCamera();
        while (Input.GetMouseButton(0))
        {
            Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            springJoint.transform.position = mousePos;
            yield return null;
        }

        //
        // The player released the mouse button, so the spring joint is now
        // detached. The spring joint can be used again later.
        //
        if (springJoint.connectedBody)
        {
            springJoint.connectedBody.drag = oldDrag;
            springJoint.connectedBody.angularDrag = oldAngularDrag;
            springJoint.connectedBody = null;
        }
    }

    Camera FindCamera()
    {
        if (camera)
        {
            return camera;
        }
        else
        {
            return Camera.main;
        }
    }
}