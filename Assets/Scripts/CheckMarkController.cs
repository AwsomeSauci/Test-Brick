using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMarkController : MonoBehaviour
{
    #region singleton
    private static CheckMarkController instance;

    private CheckMarkController()
    { }

    public static CheckMarkController getInstance()
    {
        if (instance == null)
            instance = new CheckMarkController();
        return instance;
    }
    #endregion

    private GameObject CurrentCheckmark;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SetCurrentCheckmark(GameObject checkmark)
    {
        if (CurrentCheckmark)
        {
            CurrentCheckmark.SetActive(false);
        }
        CurrentCheckmark = checkmark;
        CurrentCheckmark.SetActive(true);
    }
}
