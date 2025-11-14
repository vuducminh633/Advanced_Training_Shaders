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

public class Blaster : MonoBehaviour
{
    
    public Transform firePosition;
    public GameObject projectilePrefab;
    public GameObject muzzlePrefab;
    public GameObject impactPrefab;
    public float muzzleScale = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

  
    public void Fire()
    {
        SpawnMuzzle();
        SpawnProjectile();
    }

    public void SpawnProjectile()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("SpawnProjectile: missing projectilePrefab");
            return;
        }


        GameObject o = Instantiate(projectilePrefab,
                        firePosition.transform.position, Quaternion.identity);

        o.transform.rotation = firePosition.rotation;
        o.transform.SetParent(null);

        Projectile projectile = o.GetComponent<Projectile>();
        if(projectile != null)
        {
            projectile.impactPrefab = impactPrefab;
        } else
        {
            Debug.LogError("Projectile component is null");
        }
    }

    public void SpawnMuzzle()
    {
        if(muzzlePrefab == null)
        {
            Debug.LogError("SpawnMuzzle: missing muzzlePrefab");
            return;
        }
        if(firePosition == null) {
            Debug.LogError("SpawnMuzzle: missing firePosition");
            return;
        }
        

        GameObject o = Instantiate(muzzlePrefab,
                            firePosition.transform.position,
                            Quaternion.identity);

        o.transform.rotation = firePosition.rotation;
        o.transform.localScale *= muzzleScale;
    }
}
