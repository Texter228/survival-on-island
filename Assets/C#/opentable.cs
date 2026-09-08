using UnityEngine;

public class opentable : MonoBehaviour
{
    public GameObject table;

    private bool isopen;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Change();
        }
    }

    public void Change()
    {
        table.active = !table.active;
    }
}
