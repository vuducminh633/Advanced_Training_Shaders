/*
 * Copyright (c) 2021 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using UnityEngine.Tilemaps;

namespace RayWenderlich.WenderlichTopia
{
    public class StructurePlacementManager : MonoBehaviour
    {
        public GameObject[] straightRoadPlacementPrefabs;
        public GameObject[] curvedRoadPlacementPrefabs;
        public GameObject[] housePlacementPrefabs;
        public Tilemap gridTilemap;
        public ConstructionManager constructionManager;
        
        private bool isPlacingStructure;
        private GameObject[] currentPlacementPrefabs;
        private GameObject currentPlacementPrefab;
        private int prefabIndex;
        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (!isPlacingStructure) return;
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                BeginBuildingStructure();
                return;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CancelPlacingStructure();
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                RotatePlacementStructure();
            }

            currentPlacementPrefab.transform.position = GetGridPositionOfMouse();
        }

        private void BeginBuildingStructure()
        {
            constructionManager.BuildStructure(currentPlacementPrefab, GetGridPositionOfMouse());
            currentPlacementPrefab = null;
            currentPlacementPrefabs = null;
            isPlacingStructure = false;
        }

        private void CancelPlacingStructure()
        {
            Destroy(currentPlacementPrefab);
            currentPlacementPrefabs = null;
            isPlacingStructure = false;
        }

        private void RotatePlacementStructure()
        {
            Destroy(currentPlacementPrefab);
            prefabIndex += 1;
            prefabIndex %= currentPlacementPrefabs.Length;
            currentPlacementPrefab = Instantiate(currentPlacementPrefabs[prefabIndex]);
        }

        private Vector3 GetGridPositionOfMouse()
        {
            var mousePosition = Input.mousePosition;
            var worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0;
            var cellPosition = gridTilemap.WorldToCell(worldPosition);
            var gridPosition = gridTilemap.CellToWorld(cellPosition);
            return gridPosition;
        }

        private void BeginPlacingStructure()
        {
            prefabIndex = 0;
            isPlacingStructure = true;
            currentPlacementPrefab = Instantiate(currentPlacementPrefabs[prefabIndex]);
            currentPlacementPrefab.transform.position = GetGridPositionOfMouse();
        }

        public void OnButtonBuildStraightRoad()
        {
            currentPlacementPrefabs = straightRoadPlacementPrefabs;
            BeginPlacingStructure();
        }

        public void OnButtonBuildCurvedRoad()
        {
            currentPlacementPrefabs = curvedRoadPlacementPrefabs;
            BeginPlacingStructure();
        }
        
        public void OnButtonBuildHouse()
        {
            currentPlacementPrefabs = housePlacementPrefabs;
            BeginPlacingStructure();
        }
    }
}
