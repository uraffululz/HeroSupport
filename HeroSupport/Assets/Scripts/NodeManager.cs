using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NodeManager : MonoBehaviour {

	DayNightCycle DNCycle;

	public GameObject[] neighborhoods;
	public List<GameObject> nodeList = new List<GameObject>();

//TODO This variable seems kind of unstable. Maybe see if there's a better way to keep the values consistent,
//Maybe using PlayerPrefs (which I'll be using eventually anyway)
	public static string[] nodeGangs = new string[10] {"", "", "", "", "", "", "", "", "", ""};

	bool gangWarStarted = false;

	void Awake() {
		DNCycle = GameObject.FindGameObjectWithTag("DayNightLight").GetComponent<DayNightCycle>();
		
	}


	void Start () {
		
	}


	void Update () {
		if (DNCycle.dayTime && !gangWarStarted) {
			GangWarOnNode();
			gangWarStarted = true;
		}
		else if (!DNCycle.dayTime && DNCycle.isDusk) {
			gangWarStarted = false;
		}
	}


	void GangWarOnNode() {
//TODO Set this as part of the listener for the "DaylightCome" event method when it is triggered on the DayNightCycle script (Not yet implemented)
		//Here, the gangs of the city fight for control of the map's nodes
		foreach (GameObject node in nodeList) {
			NodeDetails nodeDeets = node.GetComponent<NodeDetails>();
			if (!nodeDeets.battlingForControl) {
				GameObject neighborToFight = nodeDeets.neighborObjects[Random.Range(0, nodeDeets.neighborObjects.Count)];
				NodeDetails neighborDeets = neighborToFight.GetComponent<NodeDetails>();

				if (!neighborDeets.battlingForControl && nodeDeets.gangName != neighborDeets.gangName) {
					Debug.Log(node.name + " fighting with " + neighborToFight.name);
					nodeDeets.battlingForControl = true;
					neighborDeets.battlingForControl = true;
					int isKeepingControl = Random.Range(0, 2);
					if (isKeepingControl == 0) {
						Debug.Log(node.name + ": I kept control of my territory!");
					}
					else {
						if (!nodeDeets.isGangHQ) {
							nodeGangs[nodeDeets.nodeNum] = neighborDeets.gangName;
							nodeDeets.SetMyGangInfluence();
							Debug.Log(node.name + ": I lost control of my territory!");
						}
						else {
							Debug.Log("This is my gang's home base! I can't lose!");
							int isTakingControl = Random.Range(0, 2);
							if (isTakingControl == 0) {
								Debug.Log(node.name + ": I was unable to take control!");
							}
							else {
								nodeGangs[neighborDeets.nodeNum] = nodeDeets.gangName;
								neighborDeets.SetMyGangInfluence();
								Debug.Log(node.name + ": I was able to take control of " + neighborToFight.name);
							}
						}
					}
				}
				else {
					//Debug.Log(node.name + ": My neighbor, " + neighborToFight.name + " is busy/friendly. No gang war here tonight");
					//No battling with the chosen neighbor
					//Choose another?
				}
			}
		}

		//Reset everything for next time
		foreach (GameObject node in nodeList) {
			node.GetComponent<NodeDetails>().battlingForControl = false;
		}
	}
}
