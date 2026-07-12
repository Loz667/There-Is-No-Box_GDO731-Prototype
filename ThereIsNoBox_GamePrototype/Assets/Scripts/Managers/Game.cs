using UnityEngine;


    public class Game: MonoBehaviour
    {
        public static Game instance {get; private set;}
        public static FacilityManager Facility {get; private set;}
        public static GameDirector Director {get; private set;}
        public static HUDController HUD {get; private set;}
        public static UIController UI {get; private set;}
        
        private void Awake()
        {
            FindManagerObjects();
            instance = this;
        }
        
        private void FindManagerObjects()
        {
            HUD = FindAnyObjectByType<HUDController>();
            Director = FindAnyObjectByType<GameDirector>();
            Facility = FindAnyObjectByType<FacilityManager>();
            //Control = FindAnyObjectByType<PlayerController>();
            //Turn = FindAnyObjectByType<TurnManager>();
            UI = FindAnyObjectByType<UIController>();
            
        }
        
    }
