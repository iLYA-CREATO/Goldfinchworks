using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpeedSlot : MonoBehaviour
{
    [Header("Список предметов")]
    [SerializeField] private List<SlotData> slot;

    public const KeyCode keyCode1 = KeyCode.Alpha1;
    public const KeyCode keyCode2 = KeyCode.Alpha2; 
    private void Update()
    {
        for(int i = 0; i < slot.Count; i++)
        {
            if (Input.GetKeyDown(slot[i].key))
            {
                for(int j = 0; j < slot.Count; j++)
                {
                    slot[j].itemSlot.gameObject.SetActive(false);
                }

                switch (slot[i].key)
                {
                    case keyCode1:
                        Debug.Log($"Предмет {slot[i].itemSlot}");
                        slot[i].itemSlot.gameObject.SetActive(true);
                        break;
                    case keyCode2:
                        Debug.Log($"Предмет {slot[i].itemSlot}");
                        slot[i].itemSlot.gameObject.SetActive(true);
                        break;
                }
            }
        }
    }
}

[Serializable]
public class SlotData
{
    public GameObject itemSlot;
    public KeyCode key;
}
