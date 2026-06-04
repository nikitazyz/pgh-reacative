using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace RRR
{
    public class RRR_TutorialManager : MonoBehaviour
    {
        public event Action OnClose;
        
        public GameObject tutorialCanvas;
        public GameObject closeButtonHolder;
        public GameObject previousButtonHolder;
        public GameObject nextButtonHolder;
        public GameObject nextButtonCloseVariantHolder;
        public GameObject[] tutorialImages;
        public bool enableAtStart;
        public bool playOneTime = true;
        private int currentIndex = 0;
        private bool tutorialActive = false;

        private bool wasPlayed;

        public bool WasPlayed => wasPlayed;
        void Awake()
        {
            // Disable all tutorial images and button holders at the start
            DisableTutorial();
            if (enableAtStart)
            {
                StartTutorial();
            }
            
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                StartTutorial();
            }
        }

        public void StartTutorial()
        {
            if (playOneTime && wasPlayed)
            {
                return;
            }
            wasPlayed = true;
            tutorialActive = true;
            gameObject.SetActive(true);
            ShowImage(currentIndex);
        }

        void DisableTutorial()
        {
            if (tutorialCanvas != null)
            {
                tutorialCanvas.SetActive(false);
            }
            foreach (var image in tutorialImages)
            {
                image.SetActive(false);
            }
            if (closeButtonHolder != null)
            {
                closeButtonHolder.SetActive(false);
            }
            if (previousButtonHolder != null)
            {
                previousButtonHolder.SetActive(false);
            }
            if (nextButtonHolder != null)
            {
                nextButtonHolder.SetActive(false);
            }
            if (nextButtonCloseVariantHolder != null)
            {
                nextButtonCloseVariantHolder.SetActive(false);
            }
        }

        public void NextImage()
        {
            if (currentIndex < tutorialImages.Length - 1)
            {
                currentIndex++;
                ShowImage(currentIndex);
            }
            else
            {
                CloseTutorial();
            }
        }

        public void PreviousImage()
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                ShowImage(currentIndex);
            }
        }

        public void CloseTutorial()
        {
            // Reset active state of tutorial images and button holders
            DisableTutorial();

            // Reset currentIndex
            currentIndex = 0;

            // Disable tutorial canvas
            tutorialActive = false;
            tutorialCanvas.SetActive(false);
            
        }

        void ShowImage(int index)
        {
            for (int i = 0; i < tutorialImages.Length; i++)
            {
                if (i == index)
                {
                    tutorialImages[i].SetActive(true);
                }
                else
                {
                    tutorialImages[i].SetActive(false);
                }
            }

            // Activate button holders only when there are tutorial images visible
            if (closeButtonHolder != null)
            {
                closeButtonHolder.SetActive(tutorialImages[index].activeSelf);
            }
            if (previousButtonHolder != null)
            {
                // Enable the Previous Button Holder only if the current index is not the first tutorial image
                previousButtonHolder.SetActive(index > 0 && tutorialImages[index].activeSelf);
            }
            if (nextButtonHolder != null)
            {
                // Enable the Next Button Holder if the current index is not the last tutorial image
                nextButtonHolder.SetActive(index < tutorialImages.Length - 1);
            }
            if (nextButtonCloseVariantHolder != null)
            {
                // Enable the Next Button Close Variant Holder only if the Next Button Holder is disabled
                nextButtonCloseVariantHolder.SetActive(!nextButtonHolder.activeSelf);
            }
        }
    }
}