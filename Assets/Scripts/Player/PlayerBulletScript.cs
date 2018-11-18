using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour {

    private readonly float startSpeed = 300;
    private readonly float moveSpeed = 100;

	void Start () {
        gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * startSpeed;
        StartCoroutine(DelayAtStart());
        StartCoroutine(DelayForDestroy());
	}

    IEnumerator DelayAtStart()
    {
        yield return new WaitForSeconds(0.02f);
        gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * moveSpeed;
    }

    IEnumerator DelayForDestroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}