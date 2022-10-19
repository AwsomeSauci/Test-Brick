using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExpControl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ExpCnt;
    [SerializeField] private TextMeshProUGUI Cost;
    private string exp = "EXP";

    public void UpdateExp()
    {
        ExpCnt.text = PlayerPrefs.GetInt(exp).ToString();
    }

    public void UpdateCost(int cost)
    {
        Cost.text = cost.ToString();
    }

    public void AddExp()
    {
        PlayerPrefs.SetInt(exp, PlayerPrefs.GetInt(exp)+1);
        UpdateExp();
    }
}
