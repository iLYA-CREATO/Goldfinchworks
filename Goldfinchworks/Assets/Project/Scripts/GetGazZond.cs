using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGazZond : MonoBehaviour
{
    [SerializeField] private GameObject gasZond;
    [SerializeField] private KeyCode getGasZond;
    [SerializeField] private bool isGetGasZond;

    private void Update()
    {
        if(Input.GetKeyDown(getGasZond))
        {
            isGetGasZond = !isGetGasZond;

            if (isGetGasZond)
            {
                gasZond.SetActive(true);
            }
            else
            {
                gasZond.SetActive(false);
            }
        }
    }
}
