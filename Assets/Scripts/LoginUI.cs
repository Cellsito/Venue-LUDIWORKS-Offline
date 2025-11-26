using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    public Button loginButton;

    void Awake()
    {
        loginButton.onClick.RemoveAllListeners();
    }

    void Start()
    {
        loginButton.onClick.AddListener(() =>
        {
            if (NetworkLauncher.Instance != null)
                NetworkLauncher.Instance.OnLoginButtonPressed();
            else
                Debug.LogError("NetworkLauncher.Instance está NULO!");
        });
    }
}
