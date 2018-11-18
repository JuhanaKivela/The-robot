using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMainGunScript : MonoBehaviour {

    public GameObject bulletPrefab;
    public GameObject bulletSpawnLocationObject;
    public GameObject bulletSpawnRotationObject; //This is camera

    private readonly float delayTimeBetweenShots = 0.04f;
    private bool delayBetweenShots;

    private readonly int magazineSize = 35;
    private int bulletsLeft;
    private bool reloading = false;
    private readonly float reloadTime = 1.0f;
    public Text bulletCounterText;

	// Use this for initialization
	void Start () {
        bulletsLeft = magazineSize;
        bulletCounterText.text = bulletsLeft.ToString();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetButton("Fire1"))
        {
            if(reloading==false)
            {
                if (delayBetweenShots == false)
                {
                    delayBetweenShots = true;
                    Instantiate(bulletPrefab, bulletSpawnLocationObject.transform.position, bulletSpawnRotationObject.transform.rotation);
                    StartCoroutine(WaitBetweenShots());

                    bulletsLeft = bulletsLeft - 1;
                    if(bulletsLeft<10)
                    {
                        bulletCounterText.text = "0" + bulletsLeft.ToString();
                    }
                    else
                    {
                        bulletCounterText.text = bulletsLeft.ToString();
                    }
                    
                    if (bulletsLeft<=0)
                    {
                        reloading = true;
                        bulletCounterText.text = "--";
                        StartCoroutine(WaitForReloading());
                    }
                    else if(bulletsLeft<(magazineSize/3))
                    {
                        bulletCounterText.color = Color.red;
                    }
                    else if(bulletsLeft<(magazineSize/1.9f))
                    {
                        bulletCounterText.color =  new Color(1,0.6f,0,1);
                    }
                }
            }
        }

        if(Input.GetButtonDown("Reload"))
        {
            reloading = true;
            bulletCounterText.color = Color.red;
            bulletCounterText.text = "--";
            StartCoroutine(WaitForReloading());
        }
	}

    IEnumerator WaitBetweenShots()
    {
        yield return new WaitForSeconds(delayTimeBetweenShots);
        delayBetweenShots = false;
    }

    IEnumerator WaitForReloading()
    {
        yield return new WaitForSeconds(reloadTime);
        bulletsLeft = magazineSize;
        bulletCounterText.color = new Color(0.28f, 0.8f, 1, 1);
        bulletCounterText.text = bulletsLeft.ToString();
        reloading = false;
    }
}
