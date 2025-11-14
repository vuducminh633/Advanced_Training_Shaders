/*
Copyright (c) 2020 Razeware LLC

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or
sell copies of the Software, and to permit persons to whom
the Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

Notwithstanding the foregoing, you may not use, copy, modify,
merge, publish, distribute, sublicense, create a derivative work,
and/or sell copies of the Software in any work that is designed,
intended, or marketed for pedagogical or instructional purposes
related to programming, coding, application development, or
information technology. Permission for such use, copying,
modification, merger, publication, distribution, sublicensing,
creation of derivative works, or sale is expressly withheld.

This project and source code may use libraries or frameworks
that are released under various Open-Source licenses. Use of
those libraries and frameworks are governed by their own
individual licenses.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
DEALINGS IN THE SOFTWARE.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
	public float speed = 14;


	public GameObject impactPrefab;

	
	protected bool myCollided = false;
	
	void Awake()
	{
		gameObject.tag = "Bullet";  // ken: Make sure the Tag is added,
									// the tag is used to prevent collision

	}



	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		MoveProjectile();
	}

	void MoveProjectile()
	{
		transform.position += transform.forward * (speed * Time.deltaTime);
	}


    void CreateImpactVFX(ContactPoint contact)
	{
		if(impactPrefab == null)
		{
			return;
		}

		Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
		Vector3 pos = contact.point;
		//Debug.Log("hitPrefab=" + hitPrefab);
		var hitVFX = Instantiate(impactPrefab, pos, rot) as GameObject;

		DestroyParticle(hitVFX);
	}

	void DestroyParticle(GameObject obj)
	{
		var ps = obj.GetComponent<ParticleSystem>();
		if (ps == null)
		{
			ps = obj.transform.GetComponentInChildren<ParticleSystem>();
		}

		if (ps != null)
		{
			Destroy(obj, ps.main.duration);
		}
	}

	

	void OnCollisionEnter(Collision co)
	{
		if (co.gameObject.tag == "Bullet")
		{
			return;     // Bullet not collide with other bullet
		}

        if(myCollided)
		{
			return;
		}

		HandleCollision(co);

		ClearProjectile();
	}

   
    void HandleCollision(Collision co)
	{
		myCollided = true;

		if (co.contactCount > 0)
		{
			ContactPoint cp = co.GetContact(0);

			CreateImpactVFX(cp);
		}

		
	}


	void ClearProjectile()
	{
		Destroy(gameObject);
	}

}
