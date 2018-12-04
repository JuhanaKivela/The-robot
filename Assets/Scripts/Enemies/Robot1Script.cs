using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Robot1Script : MonoBehaviour {
    NavMeshAgent navAgent;
    public GameObject playerObject;
    private Vector3 lastPlayerLocation;

    // Use this for initialization
    void Start () {
        navAgent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Physics.Linecast(gameObject.transform.position, playerObject.transform.position,25))
        {
            Debug.DrawLine(gameObject.transform.position,playerObject.transform.position, Color.black,25);
            navAgent.destination = playerObject.transform.position;
            lastPlayerLocation = playerObject.transform.position;
        }
        else
        {
            navAgent.destination = lastPlayerLocation;
        }
        
	}
}
