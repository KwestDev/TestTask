using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class SceneIdentifier : MonoBehaviour {

    public static bool enemyscene = false ;

	// Use this for initialization
    void Awake()
    {

        DontDestroyOnLoad(gameObject);
    }
	void Start () {

        var listObjects = GameObject.FindGameObjectsWithTag("SceneIdentifier");

        if (listObjects.Length > 1)
            EditorController.enemyScene = true;
        else
            EditorController.enemyScene = false;
            

	}
	
   
	// Update is called once per frame
	void Update () {
		
	}
}
