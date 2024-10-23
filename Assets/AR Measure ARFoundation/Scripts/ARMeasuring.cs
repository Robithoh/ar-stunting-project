using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ARMeasuring : MonoBehaviour
{
    public bool debugMode = false;

    ARRaycastManager rayCastManager;
    List<ARRaycastHit> rayHit;
    Touch touch;
    Actions actions;

    // Start is called before the first frame update
    void Start()
    {
        //intialize the variables
        rayCastManager = FindObjectOfType<ARRaycastManager>();
        actions = FindObjectOfType<Actions>();
        rayHit = new List<ARRaycastHit>() ;

    }

    // Update is called once per frame
    void Update()
    {
        if (debugMode == false)
        {
            // if there is no touch, do not continue with the update.        
            if (Input.touchCount < 1)
            {
                return;
            }

            touch = Input.GetTouch(0);

            if (touch.phase != TouchPhase.Began)
            {
                return;
            }

            // use raycast using the touch
            rayCastManager.Raycast(new Vector2(touch.position.x, touch.position.y), rayHit, TrackableType.Planes);


            //condition with AR plane
            if (rayHit.Count > 0)
            {
                //used to know if the player has selected an object
                GameObject outHitGo = null;

                RaycastHit physicHit;
                Ray ray2 = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                if (Physics.Raycast(ray2, out physicHit))
                {
                    outHitGo = physicHit.transform.gameObject;
                }

                actions.PerfromTouch(Input.GetTouch(0).position, rayHit[0].pose, outHitGo);
            }
        }
        else
        {
            // use raycast using the mouse inpunt
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            bool impact=Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity);
           
             //condition with debug plane
            if (impact)
            {
                //used to know if the player has selected an object
                GameObject outHitGo = null;

                RaycastHit physicHit;
                Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray2, out physicHit))
                {
                    outHitGo = physicHit.transform.gameObject;

                    if (Input.GetMouseButtonDown(0))
                    {
                        actions.PerfromTouch(Input.mousePosition, physicHit.point, physicHit.normal, outHitGo);
                    }
                }

              
            }
        }
        
    }
}
