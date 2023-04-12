using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LogicManager : MonoBehaviour
{
    public GameObject LossPanel;
    public GameObject WinPanel;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGameWin()
    {
        if (!WinPanel.activeSelf && !LossPanel.activeSelf)
            WinPanel.SetActive(true);
    }

    public void EndGameLoss()
    {
        if (!WinPanel.activeSelf && !LossPanel.activeSelf)
            LossPanel.SetActive(true);


    }

    public void RestartGame(float delay)
    {
        WinPanel.SetActive(false);
        LossPanel.SetActive(false);

        StartCoroutine(StartGame(delay));
    }

    public IEnumerator StartGame(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
