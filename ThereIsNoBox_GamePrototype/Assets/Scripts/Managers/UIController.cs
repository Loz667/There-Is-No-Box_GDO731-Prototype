using System;
using UnityEngine;
    public class UIController: MonoBehaviour
    {
        public PlayerHUD playerHUD;
        public MapView mapView;
        public MapView mapView2;
        public PuzzleUI puzzleUI;
        public LabPuzzle labPuzzle;
        
        
        private UIPanel currentPanel;
        private UIPanel prevPanel;
        
        void OnEnable() => SubscribeEvents();
        void OnDisable() => UnsubscribeEvents();

        private void Start()
        {
            playerHUD.Show();
            mapView.Hide();
            mapView2.Hide();
            puzzleUI.Hide();
            labPuzzle.Hide();
            currentPanel = playerHUD;
        }
        
        /*
        //TODO Figure out the logic behind this
        public void OpenPanel(UIPanel panel)
        {
            if (currentPanel == panel || panel.IsOpen) return;
            prevPanel = currentPanel;
            currentPanel = panel;
            prevPanel.Hide();
            currentPanel.Show();
        }
        
        
        //TODO Do we ever not want to go back to the PlayerHUD?
        public void ClosePanel(UIPanel panel)
        {
            if (currentPanel != panel || !panel.IsOpen) return;
            currentPanel.Hide();
            if (prevPanel != null)
            {
                prevPanel.Show();
                currentPanel = prevPanel;
            }
            else
            {
                playerHUD.Show();
                currentPanel = playerHUD;
            }

        }
        */

        public void OpenPuzzleView(Puzzle puzzle)
        {
            Debug.Log("UIController.OpenPuzzleView");
            playerHUD.Hide();
            puzzleUI.LoadPuzzle(puzzle);
        }

        public void ClosePuzzleView()
        {
            puzzleUI.Hide();
            playerHUD.Show();
        }

        public void OpenLabPuzzle()
        {
            playerHUD.Hide();
            labPuzzle.Show();
        }

        public void CloseLabPuzzle()
        {
            labPuzzle.Hide();
            playerHUD.Show();
        }
        
        
        public void ToggleMapView()
        {
            if (mapView.IsOpen)
            {
                mapView.Hide();
                playerHUD.Show();
            }
            else
            {
                playerHUD.Hide();
                mapView.Show();
            }
        }
        
        public void ToggleMapView2()
        {
            if (mapView2.IsOpen)
            {
                mapView2.Hide();
                playerHUD.Show();
            }
            else
            {
                playerHUD.Hide();
                mapView2.Show();
            }
        }
        
        public void ToggleHUD(bool state)
        {
        
            //HUD.SetActive(state);
        }
        
        /*
        public void TogglePuzzleView()
        {
            Debug.Log("Puzzle view toggled");
            //puzzleUI.ToggleView();
            if (!puzzleUI.IsOpen)  {OpenPuzzleView();} else  {ClosePuzzleView();}
        }
        */


        private void SubscribeEvents()
        {
            //EventBroker<SelectUnitEvent>.OnEvent += UnitSelectedListener;
        }

        private void UnsubscribeEvents()
        {
            //EventBroker<SelectUnitEvent>.OnEvent -= UnitSelectedListener;
        }
    }
