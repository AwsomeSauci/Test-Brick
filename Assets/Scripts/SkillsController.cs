using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillsController : MonoBehaviour
{    
    [SerializeField] private UnityEvent CanBeLearned;
    [SerializeField] private UnityEvent CanBeUnlearned;
    [SerializeField] private UnityEvent CantBeLearned;
    [SerializeField] private UnityEvent CantBeUnlearned;
    [SerializeField] private UnityEvent UnLearnAll;
    [SerializeField] private UnityEvent UpdateExpInfo;

    private GraphItem SelectedItem;
    private string exp = "EXP";

    public void SetSelectedItem(GraphItem Item)
    {
        SelectedItem = Item;
        CheckItemStatus();
    }

    private void Start()
    {
        PlayerPrefs.SetInt(exp, 0);
    }

    public void CheckItemStatus()
    {
        if (SelectedItem != null)
        {
            if (SelectedItem.CheckCanBeLearned()) CanBeLearned.Invoke();
            else CantBeLearned.Invoke();
            if (SelectedItem.CheckCanBeUnLearned())
            {
                CanBeUnlearned.Invoke();
            }
            else CantBeUnlearned.Invoke();
        }
    }

    public void LearnItem()
    {
        SelectedItem.LearnItem();
        UpdateExpInfo.Invoke();
    }

    public void UnLearnItem()
    {
        SelectedItem.UnLearnItem();
        UpdateExpInfo.Invoke();
    }

    public void UnLearnAllItems()
    {
        UnLearnAll.Invoke();
        UpdateExpInfo.Invoke();
    }
}
