using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GraphItem : MonoBehaviour
{
    [SerializeField] private GameObject SelectImage;
    [SerializeField] private GameObject LearnedImage;
    [SerializeField] private GraphItem[] connectedPoints;
    [SerializeField] private int ItemID;
    [SerializeField] private bool learned = false;
    [SerializeField] private GraphController graphController;
    [SerializeField] private int Cost = 0;
    [SerializeField] EventSetCost SetCost;

    private string exp = "EXP";
    private List<int> LearnedConnectedItems = new List<int>();
    private bool canLearned = false;
    private bool canUnLearned = false;

    public List<int> GetLearnedItems()
    {
        LearnedConnectedItems.Clear();
        for (int i = 0; i < connectedPoints.Length; i++)
        {
            if (connectedPoints[i].CheckLearn()) LearnedConnectedItems.Add(connectedPoints[i].GetID());
        }
        return LearnedConnectedItems;
    }

    public bool CheckCanBeLearned()
    {
        if(learned)return false;
        if (PlayerPrefs.GetInt(exp) < Cost) return false;
        canLearned = false;
        for (int i = 0; i < connectedPoints.Length && !canLearned; i++)
        {
            if (connectedPoints[i].CheckLearn())
            {
                canLearned = true;
            }
        }        
        return canLearned;
    }

    public bool CheckCanBeUnLearned()
    {
        if (!learned) return false;     
        
        learned = false; //"забыть" навык перед тем как проверить, есть ли у соседних активных навыков путь к стартовому
        canUnLearned = true;
        for (int i = 0; i < connectedPoints.Length && canUnLearned; i++)
        {
            if (connectedPoints[i].CheckLearn() && connectedPoints[i].GetID() != 0)
            {
                if (!graphController.CheckUnlearnPossibility(connectedPoints[i]))
                {
                    canUnLearned = false;
                }
            }
        }
        learned = true;//"изучить" навык заного, т.к. этот проверка
        return canUnLearned;
    }

    public bool CheckLearn()
    {
        return learned;
    }

    public int GetCost()
    {
        return Cost;
    }

    public int GetID()
    {
        return ItemID;
    }

    public void UnLearnItem()
    {
        PlayerPrefs.SetInt(exp, PlayerPrefs.GetInt(exp) + Cost);
        learned =false;
        LearnedImage.SetActive(false);
    }

    public void LearnItem()
    {
        PlayerPrefs.SetInt(exp, PlayerPrefs.GetInt(exp) - Cost);
        LearnedImage.SetActive(true);
        learned = true;

    }

    public void SelectItem()
    {
        SetCost.Invoke(Cost);
        CheckMarkController.getInstance().SetCurrentCheckmark(SelectImage);
    }
}
[System.Serializable]
public class EventSetCost : UnityEvent<int> { }