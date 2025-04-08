using UnityEngine;

public class MainController : MonoBehaviour
{
    public static MainController Instance;

    public SoundManager SoundManager { get; private set; }
    public UIManager UIManager { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SoundManager = GetComponentInChildren<SoundManager>();
            UIManager = GetComponentInChildren<UIManager>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

