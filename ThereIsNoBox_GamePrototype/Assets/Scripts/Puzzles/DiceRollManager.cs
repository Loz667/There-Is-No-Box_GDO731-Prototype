using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollManager : MonoBehaviour
{
    
    public static DiceRollManager Instance { get; private set; }
    
    public DieDefinition standardDie;
    public int initialCount = 6;

    public Transform dicePool;
    public GameObject diePrefab;
    public Button rollButton;

    private List<DieController> activeDice = new List<DieController>();

    void Awake()
    {
        if (Instance == null){Instance = this;}
        else{Destroy(gameObject);}
    }
    
    void Start()
    {
        if (rollButton != null)
        {
            rollButton.onClick.AddListener(RollDicePool);
        }

        InitializeDicePool(initialCount);
    }

    public void InitializeDicePool(int count)
    {
        foreach (Transform child in dicePool)
        {
            Destroy(child.gameObject);
        }
        activeDice.Clear();

        // Spawn the requested number of dice
        for (int i = 0; i < count; i++)
        {
            GameObject newDieObj = Instantiate(diePrefab, dicePool);
            DieController dieUI = newDieObj.GetComponent<DieController>();
            
            if (dieUI != null)
            {
                activeDice.Add(dieUI);
            }
        }
        Debug.Log($"Spawned {activeDice.Count} dice into the UI.");
    }

    public void RollDicePool()
    {
        if (activeDice.Count == 0)
        {
            Debug.Log("No dice left to roll!");
            return;
        }

        // Roll each active die in the pool
        foreach (DieController die in activeDice)
        {
            // NOTE: Change 'faces' to whatever you named the array in your DieDefinition SO!
            int randomIndex = Random.Range(0, standardDie.Faces.Length);
            DieFace rolledFace = standardDie.Faces[randomIndex];
            
            die.SetFace(rolledFace);
        }
    }
    

    public void RemoveDieFromPool(DieController die)
    {
        if (activeDice.Contains(die))
        {
            activeDice.Remove(die);
            Debug.Log($"Die removed from active pool. Remaining dice: {activeDice.Count}");
        }
    }
}
