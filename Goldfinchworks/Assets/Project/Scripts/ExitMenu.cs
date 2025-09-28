using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMenu : MonoBehaviour
{
    [Header("������� ��� ������")]
    [SerializeField] private KeyCode keyCodeExit;
    private void Update()
    {
        if (Input.GetKeyDown(keyCodeExit))
        {
            SceneManager.LoadScene(0);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
