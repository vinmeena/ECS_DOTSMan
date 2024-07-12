using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Entities;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    public static GameManager instance;
    AnimationManager m_animationManager;
    public GameObject _gameUI;
    public GameObject _titleUI;
    public GameObject _winUI;
    public GameObject _loseUI;

    public TMP_Text _pelletText;
    public TMP_Text _scoreText;

    public int score = 0;

    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

        score = 0;
        SwitchUI(_titleUI);
        AudioManager.instance.PlayMusic("title");
        m_animationManager = GetComponent<AnimationManager>();

        //Time.timeScale = 0f;


    }

    public void Reset()
    {
        score = 0;
        SwitchUI(_titleUI);
        AudioManager.instance.PlayMusic("title");

    }


    public void InGame()
    {
        SwitchUI(_gameUI);
        AudioManager.instance.PlayMusic("game");

    }

    public void Win()
    {
        m_animationManager.WinAnimation();
        SwitchUI(_winUI);
        AudioManager.instance.PlayMusic("win");

    }

    public void Lose()
    {
        m_animationManager.LoseAnimation();
        SwitchUI(_loseUI);
        AudioManager.instance.PlayMusic("lose");

    }


    public void SwitchUI(GameObject newSwitchUI)
    {
        _titleUI.SetActive(false);
        _gameUI.SetActive(false);
        _winUI.SetActive(false);
        _loseUI.SetActive(false);

        newSwitchUI.SetActive(true);
    }
    
    public void AddPoints(int points)
    {
        score += points;

        _scoreText.text = "Score : " + score;
    }

    public void UpdatePellet(int pelletCount)
    {
        _pelletText.text = "Pellet : " + pelletCount;
    }


    public void StartGame()
    {
        SwitchUI(_gameUI);
        AudioManager.instance.PlayMusic("game");
        Time.timeScale = 1f;
    }

    //public void RestartLevel()
    //{
    //    EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

    //    foreach (Entity e in entityManager.GetAllEntities())
    //        entityManager.DestroyEntity(e);

    //    SceneManager.LoadSceneAsync("DOTSman");
    //}
}
