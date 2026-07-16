using UnityEngine;


    public class Game: MonoBehaviour
    {
        public static Game instance {get; private set;}
        public static FacilityManager Facility {get; private set;}
        public static GameDirector Director {get; private set;}
        public static PlayerHUD PlayerHUD {get; private set;}
        public static UIController UI {get; private set;}
        
        private void Awake()
        {
            FindManagerObjects();
            instance = this;
        }
        
        private void FindManagerObjects()
        {
            PlayerHUD = FindAnyObjectByType<PlayerHUD>();
            Director = FindAnyObjectByType<GameDirector>();
            Facility = FindAnyObjectByType<FacilityManager>();
            //Control = FindAnyObjectByType<PlayerController>();
            //Turn = FindAnyObjectByType<TurnManager>();
            UI = FindAnyObjectByType<UIController>();
            
        }
        
    }
