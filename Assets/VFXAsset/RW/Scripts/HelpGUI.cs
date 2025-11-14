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

public class HelpGUI : MonoBehaviour
{
    public Shooter shooter;

    private void OnGUI()
    {
        // INFO
        float x;
        float width;

        float infoY = Screen.height - 100;
        x = 15;
        width = Screen.width - x * 2;
        GUI.Label(new Rect(x, infoY, width, 40), GetInfo());

        // HELP 
        float topY = Screen.height - 70;
        width = Screen.width - 20;
        // Make a background box
        GUI.Box(new Rect(10, topY, width, 60), "");
        float y = topY + 5;

        // Line 1
        x = 20;
        width = 90;
        GUI.Label(new Rect(x, y, width, 40), "HELP");

        x = 100;
        width = 120;
        GUI.Label(new Rect(x, y, width, 40), "1 : Change Muzzle");

        x += width + 5;
        width = 120;
        GUI.Label(new Rect(x, y, width, 40), "2 : Change Impact");

        x += width + 5;
        width = 200;
        GUI.Label(new Rect(x, y, width, 40), "3 : Change Projectile");

        y = topY + 30;
        // Line 2
        x = 100;
        width = 90;
        GUI.Label(new Rect(x, y, width, 40),  "Up : Zoom In");

        x += width + 5;
        width = 120;
        GUI.Label(new Rect(x, y, width, 40), "Down : Zoom Out");

        x += width + 5;
        width = 180;
        GUI.Label(new Rect(x, y, width, 40), "Left / Right : Rotate Camera");

        x += width + 5;
        width = 180;
        GUI.Label(new Rect(x, y, width, 40), "Space / Mouse Click: Fire");

        x += width + 5;
        width = 180;
        GUI.Label(new Rect(x, y, width, 40), "A: Toggle Auto Fire");
    }

    string GetInfo()
    {
        if(shooter == null) {
            return "Shooter object not defined";
        }

        string result = "";

        result += "AutoFire: " + (shooter.autoFire ? "ON" : "OFF");
        result += "  ";
        result += "Selected Muzzle: " + shooter.selectedMuzzle;
        result += "  ";
        result += "Selected Impact: " + shooter.selectedImpact;
        result += "  ";
        result += "Selected Projectile: " + shooter.selectedProjectile;

        return result;
    }
}
