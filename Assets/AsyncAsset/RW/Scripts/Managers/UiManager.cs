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

using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RayWenderlich.WenderlichTopia
{
    public class UiManager : MonoBehaviour
    {
        public TMP_Text totalCostText;
        public Canvas worldSpaceCanvas;
        public GameObject structureCostPrefab;
        public float textMoveSpeed;
        public float textFadeTime;
        public GameObject cashRegisterAudioPrefab;
        public Transform AudioManagerTransform;
        public GameObject WelcomePanel;
        
        private int totalCost;
        private CultureInfo usFormat;
        private WaitForSeconds timeBeforeFade;
        private WaitForSeconds timeBeforeDestroyAudio;
        
        private void Start()
        {
            totalCost = 0;
            usFormat = new CultureInfo("en-us");
            totalCostText.text = $"Total City Cost: <color=#00CD00>{totalCost.ToString("C", usFormat)}</color>";

            timeBeforeFade = new WaitForSeconds(2f);
            timeBeforeDestroyAudio = new WaitForSeconds(2.5f);
        }

        public void NewStructureComplete(int structureCost, Vector3 structurePosition)
        {
            IncrementTotalCost(structureCost);
            DisplayStructureCost(structureCost, structurePosition);
        }
        
        private void IncrementTotalCost(int newCost)
        {
            totalCost += newCost;
            totalCostText.text = $"Total City Cost: <color=#00CD00>{totalCost.ToString("C", usFormat)}</color>";
        }

        private void DisplayStructureCost(int newCost, Vector3 position)
        {
            var cashRegisterAudio = Instantiate(cashRegisterAudioPrefab, AudioManagerTransform);
            
            var structureCostGO = Instantiate(structureCostPrefab, position, Quaternion.Euler(0, 0, 10),
                worldSpaceCanvas.transform);

            var structureCostText = structureCostGO.GetComponent<TMP_Text>();

            structureCostText.text = newCost.ToString("C", usFormat);

            StartCoroutine(MoveTextUp(structureCostText.rectTransform));
            StartCoroutine(FadeText(structureCostText));
            StartCoroutine(DestroyCashRegisterAudio(cashRegisterAudio));
        }

        private IEnumerator MoveTextUp(RectTransform textTransform)
        {
            while (textTransform != null)
            {
                var textPosition = textTransform.position;
                textPosition.y += (textMoveSpeed * Time.deltaTime);
                textTransform.position = textPosition;
                yield return null;
            }
        }

        private IEnumerator FadeText(TMP_Text text)
        {
            yield return timeBeforeFade;
            var endFadeTime = Time.time + textFadeTime;
            while (Time.time <= endFadeTime)
            {
                var curTime = endFadeTime - Time.time;
                var curPercentage = curTime / textFadeTime;
                text.alpha = curPercentage;
                yield return null;
            }
            Destroy(text.gameObject);
        }

        private IEnumerator DestroyCashRegisterAudio(GameObject cashRegisterAudio)
        {
            yield return timeBeforeDestroyAudio;
            Destroy(cashRegisterAudio);
        }
        
        public void OnButtonCloseWelcomePanel()
        {
            WelcomePanel.SetActive(false);
        }
        
        public void OnButtonReset()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}