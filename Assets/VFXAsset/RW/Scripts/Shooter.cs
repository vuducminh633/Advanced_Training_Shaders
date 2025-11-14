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

public class Shooter : MonoBehaviour
{

    public Animator characterAnimator;
    public Blaster blaster;

    [Header("Sound")]
    public AudioClip shotSFX;

    [Header("Muzzle")]
    public int selectedMuzzle = 1;
    public GameObject[] muzzlePrefab;

    [Header("Impact")]
    public int selectedImpact = 0;
    public GameObject[] impactPrefab;

    [Header("Projectile")]
    public int selectedProjectile = 1;
    public GameObject[] projectilePrefab;

    [Header("AutoFire")]
    public bool autoFire = false;
    [Range(0.1f, 10f)] public float fireInterval = 1.0f;       // number of fire per


    private float mFireCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            ToggleAutoFire();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeMuzzle();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeImpact();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeProjectile();
        }




        if (autoFire)
        {
            HandleAutoFire();
        }
    }

    void HandleAutoFire()
    {
        if (autoFire == false)
        {
            return;
        }

        if (mFireCooldown > 0)
        {
            mFireCooldown -= Time.deltaTime;
            return;
        }

        Shoot();
        mFireCooldown = fireInterval;
    }

    void ChangeProjectile()
    {
        setNextSelectionValue(ref selectedProjectile, projectilePrefab.Length);
    }


    void ChangeImpact()
    {
        setNextSelectionValue(ref selectedImpact, impactPrefab.Length);
    }

    void ChangeMuzzle()
    {      
        setNextSelectionValue(ref selectedMuzzle, muzzlePrefab.Length) ;
    }

    void setNextSelectionValue(ref int currentValue, int arrayLength)
    {
        int newValue = currentValue + 1;
        if (newValue >= arrayLength)
        {
            newValue = 0;
        }
        currentValue = newValue;
    } 

    public void ToggleAutoFire()
    {
        autoFire = !autoFire;        
    }

    void PlayShotSound()
    {
        AudioSource source = GetComponent<AudioSource>();
        if (source == null)
        {
            return;
        }
        if (shotSFX == null)
        {
            return;
        }

        source.PlayOneShot(shotSFX);
    }

    void Shoot()
    {
        if (characterAnimator)
        {
            characterAnimator.SetTrigger("Shoot");
        }


        blaster.impactPrefab = getSelectedImpact();
        blaster.muzzlePrefab = getSelectedMuzzle();
        blaster.projectilePrefab = getSelectedProjectile();
        blaster.Fire();

        PlayShotSound();
    }


    GameObject getSelectedMuzzle()
    {
        int idx = selectedMuzzle;
        if (idx < 0 || idx >= muzzlePrefab.Length)
        {
            return null;
        }
        return muzzlePrefab[idx];        
    }

    GameObject getSelectedProjectile()
    {
        int idx = selectedProjectile;
        if(idx < 0 || idx >= projectilePrefab.Length)
        {
            return null;
        }
        return projectilePrefab[idx];
    }

    GameObject getSelectedImpact()
    {
        int idx = selectedImpact;
        if (idx < 0 || idx >= impactPrefab.Length)
        {
            return null;
        }
        return impactPrefab[idx];
    }
}
