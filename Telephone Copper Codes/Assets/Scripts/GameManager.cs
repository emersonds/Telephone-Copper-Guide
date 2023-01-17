using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        // Singleton pattern, assign GameManager instance if it doesn't exist.
        if (instance == null)
        {
            instance = this;
        }
        // Destroy new GameManager object if one already exists.
        else
        {
            Destroy(gameObject);
        }

        // GameManager persists through all scenes.
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
