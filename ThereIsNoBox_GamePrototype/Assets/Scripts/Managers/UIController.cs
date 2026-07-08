using System;
using UnityEngine;
    public class UIController: MonoBehaviour
    {

        public PuzzleUI puzzleUI;
        public MapView mapView;
        
        
        void OnEnable() => SubscribeEvents();
        void OnDisable() => UnsubscribeEvents();

        public void TogglePuzzleView()
        {
            puzzleUI.ToggleView();
        }

        public void ToggleMapView()
        {
            Debug.Log("ToggleMapView called");
            Game.HUD.ToggleHUD(mapView.IsActive);
            mapView.ToggleView();
        }

       


        private void SubscribeEvents()
        {
            //EventBroker<SelectUnitEvent>.OnEvent += UnitSelectedListener;
        }

        private void UnsubscribeEvents()
        {
            //EventBroker<SelectUnitEvent>.OnEvent -= UnitSelectedListener;
        }
    }
