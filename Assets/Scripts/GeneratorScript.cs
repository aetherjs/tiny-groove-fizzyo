using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour {

	public Transform character;

	public GameObject[] availableSceneries;
	public List<GameObject> currentSceneries;
    private float screenWidthInPoints;

	public GameObject[] availableObjects; 
	public GameObject[] bigBatches;   
	public List<GameObject> objects;
	public bool generateEmpty = false;

	public float objectsMinDistance = 5.0f;    
	public float objectsMaxDistance = 10.0f;

	public float objectsMinY = -1.4f;
	public float objectsMaxY = 1.4f;

	private uint counter = 0;

	// Use this for initialization
	void Start () {
		float height = 2.0f * Camera.main.orthographicSize;
		screenWidthInPoints = height * Camera.main.aspect;
	}
	
	void AddScenery(float farhtestSceneryEndX) {
		int randomSceneryIndex = Random.Range(0, availableSceneries.Length - 1);
    	GameObject Scenery = (GameObject)Instantiate(availableSceneries[randomSceneryIndex]);
    	float SceneryWidth = Camera.main.transform.Find("Floor").localScale.x;
    	float SceneryCenter = farhtestSceneryEndX + SceneryWidth * 0.5f - 2;
    	Scenery.transform.position = new Vector3(SceneryCenter, 0, 0);
    	currentSceneries.Add(Scenery);         
	} 

	void AddObject(float lastObjectX, int type)
	{
		GameObject[] actualObjects;
		if (type == 0) {
    		actualObjects = availableObjects;
		} else {
    		actualObjects = bigBatches;
		}

    	int randomIndex = Random.Range(0, actualObjects.Length);
		GameObject obj = (GameObject)Instantiate(actualObjects[randomIndex]);

		// If an object is a batch of coins then give all of the coins magnet property
		Magnet[] allChildren = obj.GetComponentsInChildren<Magnet>();
        foreach (Magnet child in allChildren) {
            child.player = character;
        }

    	float objectPositionX = lastObjectX + Random.Range(objectsMinDistance, objectsMaxDistance);
    	float randomY = Random.Range(objectsMinY, objectsMaxY);
		if (type == 1)
			randomY = 0;
		if (obj.CompareTag("Obstacle"))
			randomY = randomY + 0.5f;
    	obj.transform.position = new Vector3(objectPositionX,randomY,0); 
    	objects.Add(obj);            
	}

	 void GenerateSceneryIfRequired()
	{
    	List<GameObject> SceneriesToRemove = new List<GameObject>();
    	bool addSceneries = true;        
    	float playerX = transform.position.x;
    	float removeSceneryX = playerX - screenWidthInPoints;    
    	float addSceneryX = playerX + screenWidthInPoints;
    	float farthestSceneryEndX = 0;

    	foreach(var Scenery in currentSceneries)
    	{
        	float SceneryWidth = Camera.main.transform.Find("Floor").localScale.x;
        	float SceneryStartX = Scenery.transform.position.x - (SceneryWidth * 0.5f);    
        	float SceneryEndX = SceneryStartX + SceneryWidth; 
        	if (SceneryStartX > addSceneryX)
            	addSceneries = false;
        	if (SceneryEndX < removeSceneryX)
            	SceneriesToRemove.Add(Scenery);
        	farthestSceneryEndX = Mathf.Max(farthestSceneryEndX, SceneryEndX);
    	}

    	foreach(var Scenery in SceneriesToRemove)
    	{
        	currentSceneries.Remove(Scenery);
        	Destroy(Scenery);            
    	}
    
    	if (addSceneries) 
        	AddScenery(farthestSceneryEndX);
	}

	void GenerateObjectsIfRequired()
	{
    	float playerX = transform.position.x;        
    	float removeObjectsX = playerX - screenWidthInPoints;
    	float addObjectX = playerX + screenWidthInPoints;
    	float farthestObjectX = 0;
    
    	List<GameObject> objectsToRemove = new List<GameObject>();

    	foreach (var obj in objects) {
    	    float objX = obj.transform.position.x;
    	    farthestObjectX = Mathf.Max(farthestObjectX, objX);
    	    if (objX < removeObjectsX)            
    	        objectsToRemove.Add(obj);
    	}

    	foreach (var obj in objectsToRemove) {
        	objects.Remove(obj);
        	Destroy(obj);
    	}

    	if (farthestObjectX < addObjectX && counter < 5) {
        	AddObject(farthestObjectX, 0);
			counter++;
		} else if (farthestObjectX < addObjectX && counter >= 5) {
			AddObject(farthestObjectX, 1);
			counter = 0;
		}
	}

	public void SetGenerateEmpty(bool value) {
		generateEmpty = value;
	}

	// Update is called once per frame
	void Update () {
		GenerateSceneryIfRequired();
		if (!generateEmpty)	{
			GenerateObjectsIfRequired(); 
		}
	}
}
