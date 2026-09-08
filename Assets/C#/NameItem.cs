using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameItem : MonoBehaviour
{
    [SerializeField] private string nameiteminfo;

    public string GetName()
    {
        return nameiteminfo;
    }

    public void SetName(string Name)
    {
        nameiteminfo = Name;
    }
}
