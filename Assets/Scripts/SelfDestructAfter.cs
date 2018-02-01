using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructAfter : MonoBehaviour {

    public float time;

	
	void Start ()
    {
        StartCoroutine(DestroyAfterSeconds(time));
	}

    private IEnumerator DestroyAfterSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

}
