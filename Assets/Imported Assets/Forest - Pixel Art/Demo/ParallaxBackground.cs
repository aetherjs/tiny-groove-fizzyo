using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour {

    [SerializeField] private bool   isParallax = true;
    [SerializeField] private bool   isScrollable = true;
    [SerializeField] private float  backgroundWidth;
    [SerializeField] private float  parallaxSpeed;

    private float                   viewZone;
    private Transform               cameraTransform;
    private Transform[]             layers;
    private int                     leftIndex;
    private int                     rightIndex;
    private float                   lastCameraX;

	// Use this for initialization
	void Start () {
        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;
        layers = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }

        leftIndex = 0;
        rightIndex = layers.Length - 1;

        viewZone = Camera.main.orthographicSize * Camera.main.aspect;

        //Prevents stuttering
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isParallax)
        {
            float deltaX = cameraTransform.position.x - lastCameraX;
            float newX = transform.position.x + deltaX * parallaxSpeed;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            lastCameraX = cameraTransform.position.x;
        }
        
        if(isScrollable)
        {
            if (cameraTransform.position.x < (layers[leftIndex].position.x + viewZone - (backgroundWidth/2.0f)))
            {
                ScrollLeft();
            }
                
            else if (cameraTransform.position.x > (layers[rightIndex].position.x - viewZone + (backgroundWidth / 2.0f)))
            {
                ScrollRight();
            }
            
        }
    }

    private void ScrollLeft()
    {
        float newX = layers[leftIndex].localPosition.x - backgroundWidth;
        layers[rightIndex].localPosition = new Vector3(newX, layers[rightIndex].localPosition.y, layers[rightIndex].localPosition.z);
        leftIndex = rightIndex;
        rightIndex--;
        if(rightIndex < 0)
            rightIndex = layers.Length - 1;
    }

    private void ScrollRight()
    {
        float newX = layers[rightIndex].localPosition.x + backgroundWidth;
        layers[leftIndex].localPosition = new Vector3(newX, layers[leftIndex].localPosition.y, layers[leftIndex].localPosition.z);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex  == layers.Length)
            leftIndex = 0;
    }
	

}
