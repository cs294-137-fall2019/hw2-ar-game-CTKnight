using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARButtonManager : MonoBehaviour
{
    private static readonly string[] interactList = { "Interactable", "Mole" };
    private Camera arCamera;
    private PlaceGameBoard placeGameBoard;

    void Start()
    {
        // Here we will grab the camera from the AR Session Origin.
        // This camera acts like any other camera in Unity.
        arCamera = GetComponent<ARSessionOrigin>().camera;
        // We will also need the PlaceGameBoard script to know if
        // the game board exists or not.
        placeGameBoard = GetComponent<PlaceGameBoard>();
    }

    void Update()
    {
        var count = Input.touchCount;
        if (placeGameBoard.Placed() && count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                Vector2 touchPosition = Input.GetTouch(i).position;
                // Convert the 2d screen point into a ray.
                Ray ray = arCamera.ScreenPointToRay(touchPosition);
                // Check if this hits an object within 100m of the user.
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    // Check that the object is interactable.
                    if (interactList.Any(tag => hit.transform.tag == tag))
                        // Call the OnTouch function.
                        // Note the use of OnTouch3D here lets us
                        // call any class inheriting from OnTouch3D.
                        hit.transform.GetComponent<OnTouch3D>().OnTouch();
                }
            }
        }
    }
}
