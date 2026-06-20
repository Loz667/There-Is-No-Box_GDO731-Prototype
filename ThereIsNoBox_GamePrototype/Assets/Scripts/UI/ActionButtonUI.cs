using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TMP_Text buttonText;
    [SerializeField] GameObject selectedVisual;
    [SerializeField] GameObject busyVisual;

    private BaseAction baseAction;

    void OnEnable()
    {
        UnitActionSystem.Instance.OnBusyChanged += OnBusyChanged;

        busyVisual.SetActive(false);
    }

    void OnDestroy()
    {
        if (UnitActionSystem.Instance != null)
        {
            UnitActionSystem.Instance.OnBusyChanged -= OnBusyChanged;
        }
    }

    public void SetBaseAction(BaseAction baseAction)
    {
        this.baseAction = baseAction;
        buttonText.text = baseAction.GetActionName().ToUpper();

        button.onClick.AddListener(() =>
        {
            UnitActionSystem.Instance.SetSelectedAction(baseAction);
        });
    }

    public void ShowSelectedVisual()
    {
        BaseAction selectedAction = UnitActionSystem.Instance.GetSelectedAction();
        selectedVisual.SetActive(selectedAction == baseAction);
    }

    void OnBusyChanged(object sender, bool isBusy)
    {
        busyVisual.SetActive(isBusy);
    }
}
