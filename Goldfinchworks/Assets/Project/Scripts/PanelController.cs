using System;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public static event Action<bool> OnChangeCursor;
    [SerializeField, Header("������ �������� �������")]
    private List<GameObject> panelsIsOpen;
    [SerializeField, Header("������ �������������� �������� �������")]
    private List<GameObject> dopPanelOpen;


    [SerializeField, Header("��������� ����")]
    private StatusGame statusGame;
    // public List<GameObject> basePanelTab; // ����� ���� ������� ����� �������� ����� ����� ������ �����������
    public GameObject basePanelPlayer;
    public GameObject basePanelPlayerInventory;

    [SerializeField, Header("���������� ��������")]
    private MoveCharacter playerController;

    [SerializeField, Header("���������� ������")]
    private MoveCamera cameraController;
    private void OnEnable()
    {
 
    }

    private void OnDisable()
    {
       
    }

    private void Start()
    {
        LockerControllers();
    }
    private void Update()
    {
        // ��� �������� ����� ��� ���� ����� ��������� ������� �� ���� ������� ���� ������ � ��������
        if(basePanelPlayerInventory.activeSelf == false)
        {
            if(dopPanelOpen.Count != 0)
            {
                OnClouseDopPanel();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="panel"></param>
    /// <param name="cursorState">false - ��������, true - ��������� ������</param>
    public void OpenPanel(GameObject panel, bool cursorState)
    {
        panel.SetActive(true);
        panelsIsOpen.Add(panel);
        LockerControllers();
        OnChangeCursor?.Invoke(cursorState);
    }

    #region ������ ������ �� ������
    /// <summary>
    /// ��������� ��� ����
    /// </summary>
    public void OnClouseAllPanel()
    {
        OnClouseBasePanel();
        OnClouseDopPanel();

        OnChangeCursor?.Invoke(true);
    }
    /// <summary>
    /// ��������� ��� ����
    /// </summary>
    public void ButtonClouseDopPanel(GameObject panel)
    {
        panel.SetActive(false);
        return;
    }
    #endregion

    /// <summary>
    /// ��������� ��� ����
    /// </summary>
    public void ButtonOpenDopPanel(GameObject panel)
    {
        panel.SetActive(true);
        return;
    }

    // ����� ��������� ��� ������� ����
    private void OnClouseBasePanel()
    {
        for (int i = 0; i < panelsIsOpen.Count; i++)
        {
            panelsIsOpen[i].SetActive(false);
        }

        panelsIsOpen.Clear();
    }

    // ����� ��������� ��� �������������� ����
    private void OnClouseDopPanel()
    {
        for (int i = 0; i < dopPanelOpen.Count; i++)
        {
            dopPanelOpen[i].SetActive(false);
        }

        dopPanelOpen.Clear();
    }
    private void OpenBuildPanel(GameObject panelBuild, bool state)
    {
        if (panelsIsOpen.Count > 0)
        {
            if (panelsIsOpen[0].name == panelBuild.name)
            {
                panelBuild.SetActive(false);
                panelsIsOpen.Clear();
                OnChangeCursor?.Invoke(true);
                return;
            }
        }

        if (panelsIsOpen.Count > 0) return;

        panelBuild.SetActive(state);
        panelsIsOpen.Add(panelBuild);
        OnChangeCursor?.Invoke(false);
    }
    private void IsInventory(GameObject panelInventory, bool state)
    {
        CheckOpenPanel();

        if (state == true)
        {
            OnChangeCursor?.Invoke(false);

            basePanelPlayer.SetActive(state);
            basePanelPlayerInventory.SetActive(state);

            panelsIsOpen.Add(basePanelPlayer);
            panelsIsOpen.Add(basePanelPlayerInventory);
        }

        if (state == false)
        {
            OnChangeCursor?.Invoke(true);

            basePanelPlayer.SetActive(state);
            basePanelPlayerInventory.SetActive(state);

            for (int i = 0; i < panelsIsOpen.Count; i++)
            {
                panelsIsOpen[i].SetActive(false);
            }

            // ��������� ��� ����
            OnClouseAllPanel();
        }
    }

    public void IsOppenNewChildPanel(GameObject panelInventory)
    {
        CheckOpenPanel();

        for (int i = 0; i < panelsIsOpen.Count; i++)
        {
            panelsIsOpen[i].SetActive(false);
        }
        panelsIsOpen.Add(basePanelPlayer);
        panelsIsOpen.Add(panelInventory);
        panelInventory.SetActive(true);
        basePanelPlayer.SetActive(true);
    }

    public void IsOppenNewPanel(GameObject panelInventory, bool state)
    {
        CheckOpenPanel();
        OnChangeCursor?.Invoke(false);
        LockerControllers();
        for (int i = 0; i < panelsIsOpen.Count; i++)
        {
            panelsIsOpen[i].SetActive(false);
        }
        panelInventory.SetActive(state);
        basePanelPlayer.SetActive(state);

        panelsIsOpen.Add(basePanelPlayer);
        panelsIsOpen.Add(panelInventory);
    }

    private void IsInventoryChild(GameObject panelChild, bool state)
    {
        CheckOpenPanel();

        if (state == true)
        {
            dopPanelOpen.Add(panelChild);
            panelsIsOpen.Add(basePanelPlayer);
            panelsIsOpen.Add(basePanelPlayerInventory);

            OnChangeCursor?.Invoke(false);

            basePanelPlayer.SetActive(state);
            basePanelPlayerInventory.SetActive(state);
            panelChild.SetActive(state);
        }

        if (state == false)
        {
            OnClouseAllPanel();
            OnChangeCursor?.Invoke(true);
            basePanelPlayerInventory.SetActive(state);
            basePanelPlayer.SetActive(state);
        }
    }

    /// <summary>
    /// ����� ��������� ����� ��������� ���� ���-�� ������� �� �� ��������� 
    /// </summary>
    private void CheckOpenPanel()
    {
        if (panelsIsOpen.Count > 0)
        {
            for (int i = 0; i < panelsIsOpen.Count; i++)
            {
                ClousePanel(i);
            }
            panelsIsOpen.Clear();
        }
    }

    #region
    /// <summary>
    /// ��� ������� ESC
    /// </summary>
    /// <param name="panelESC"></param>
    private void OpenPanelESC(GameObject panelESC)
    {
        if (panelsIsOpen.Count > 0)
        {
            for (int i = 0; i < panelsIsOpen.Count; i++)
            {
                ClousePanel(i);
            }
            panelsIsOpen.Clear();

            OnChangeCursor?.Invoke(true);
            
            return;
        }
        else if(panelsIsOpen.Count == 0)
        {
            panelESC.SetActive(true);
            panelsIsOpen.Add(panelESC);

            OnChangeCursor?.Invoke(false);
            
            return;
        }
    }

    private void OpenDopPanelESC(GameObject panelESC)
    {
        panelESC.SetActive(true);
        panelsIsOpen.Add(panelESC);
    }

    private void ClouseDopPanelESC(GameObject panelESC)
    {
        panelESC.SetActive(true);
        for(int i = 0; i < panelsIsOpen.Count; i++)
        {
            if (panelsIsOpen[i] == panelESC)
            {
                panelsIsOpen[i].SetActive(false);
                panelsIsOpen.Remove(panelsIsOpen[i]); 
            }
        }
    }
    #endregion

    #region ����������� ������ �� �������� �������� �������
    private void OpenDopPanel(GameObject panel, bool cursorState)
    {
        if(dopPanelOpen.Count == 0)
        {
            panel.SetActive(true);
            dopPanelOpen.Add(panel);
            OnChangeCursor?.Invoke(cursorState);
        }
    }
    private void ClouseDopPanel(GameObject panel, bool cursorState)
    {
        for (int i = 0; i < dopPanelOpen.Count;i++)
        {
            if (dopPanelOpen[i] == panel)
            {
                dopPanelOpen[i].SetActive(false);
                dopPanelOpen.RemoveAt(i);
                OnChangeCursor?.Invoke(cursorState);
                return;
            }
        }
    }
    #endregion

    private void ClousePanel(int i)
    {
        panelsIsOpen[i].SetActive(false);
    }

    public void ClousePanel(GameObject panel)
    {
        for(int i = 0; i < panelsIsOpen.Count; i++)
        {
            if(panelsIsOpen[i] == panel)
            {
                panelsIsOpen[i].SetActive(false);
                panelsIsOpen.RemoveAt(i);
                LockerControllers();
                OnChangeCursor?.Invoke(true);
                return;
            }
        }
    }
    /// <summary>
    /// ����� ��������� ���������� ���������� ���� � ����� ������ ���������
    /// </summary>
    private void LockerControllers()
    {
        // �������� ���� �� �������� ���� ��� ������� ������� ����
        if (panelsIsOpen.Count > 0)
        {
            statusGame.isPlayGame = false;

            playerController.LockController(true);
            cameraController.LockController(true);
        }
        else
        {
            statusGame.isPlayGame = true;

            playerController.LockController(false);
            cameraController.LockController(false);
        }
    }
}
