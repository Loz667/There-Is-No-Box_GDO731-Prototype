
    using UnityEngine;

    [CreateAssetMenu(fileName = "_Char", menuName = "Back In Your Box/New Character", order = 0)]
    public class CharacterDef: ScriptableObject
    {
        [Header("Details")]
        public string CharacterName;
        public GameObject CharacterPrefab;
        public Sprite Portrait;

        [Header("Stats")] 
        public int MaxHealth;
        public int MaxMorale;
        
        //Special Action and other details here

    }
