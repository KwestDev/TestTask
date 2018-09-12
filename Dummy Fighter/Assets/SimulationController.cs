using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class SimulationController : MonoBehaviour {

    // Use this for initialization
    private List<LinkData> PlayerChain;
    private List<LinkData> EnemyChain;
	void Start () {
        PlayerChain = JsonMapper.ToObject<List<LinkData>>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Player.json"));
        EnemyChain  = JsonMapper.ToObject<List<LinkData>>(File.ReadAllText(Application.dataPath + "/StreamingAssets/Enemy.json"));

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
