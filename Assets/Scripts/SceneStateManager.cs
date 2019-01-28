using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneStateManager : MonoBehaviour
{
    public static SceneStateManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Call_Replay();
    }

    public void Call_Replay()
    {
        StartCoroutine(Replay());
    }

    IEnumerator Replay()
    {
        CameraFade.instance.FadeOutTime = 1.5f;
        CameraFade.instance.FadeOut();

        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene("Main");
    }
}
