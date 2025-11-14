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

public class CameraControl : MonoBehaviour
{
    public float rotateSpeed = 10;
    

    public float minDistance = 5;
    public float maxDistance = 20;

    public float zoomSpeed = 10;

    protected Transform mCameraTransform;

    void Awake()
    {
        mCameraTransform = GetComponentInChildren<Camera>().transform;    
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotateAntiClockWise(Time.deltaTime * rotateSpeed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            RotateClockWise(Time.deltaTime * rotateSpeed);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            ZoomIn(Time.deltaTime * zoomSpeed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            ZoomOut(Time.deltaTime * zoomSpeed);
        }
    }

    void ZoomIn(float delta)
    {
        Vector3 pos = mCameraTransform.localPosition;
        pos.z += delta;
        if(pos.z > -minDistance)
        {
            pos.z = -minDistance;
        }
        mCameraTransform.localPosition = pos;
    }

    void ZoomOut(float delta)
    {
        Vector3 pos = mCameraTransform.localPosition;
        pos.z -= delta;
        if (pos.z < -maxDistance)
        {
            pos.z = -maxDistance;
        }
        mCameraTransform.localPosition = pos;
    }

    void RotateClockWise(float angle)
    {
        transform.Rotate(Vector3.up, angle);
    }

    void RotateAntiClockWise(float angle)
    {
        transform.Rotate(Vector3.up, -angle);
    }
}
