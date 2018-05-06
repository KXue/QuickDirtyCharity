using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public HappinessCalculator m_happinessCalculator;
	public GameObject m_winScreen;
	public GameObject m_lossScreen;
	public GameObject m_optionsDisplay;
	private bool m_isPaused = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PauseGame()
    {
        m_isPaused = true;
        Time.timeScale = 0;
    }
	public void GameOver(){
		if(m_happinessCalculator.happinessPercentage < 0.5f){
			m_lossScreen.SetActive(true);
			m_winScreen.SetActive(false);

		}
		else{
            m_lossScreen.SetActive(false);
            m_winScreen.SetActive(true);
		}
		m_optionsDisplay.SetActive(true);
	}
	public void PlayAgain(){
        m_isPaused = false;
        Time.timeScale = 1;
        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.buildIndex);
	}
	public void Quit(){
		Application.Quit();
	}
}
