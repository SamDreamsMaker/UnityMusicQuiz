using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject quizMainUIGO;
    private const string QUIZ_MAIN_UI_LOCATION = "UI/QuizMainUI";
    void Start()
    {
        createQuizMainUI();
    }

    
    void createQuizMainUI()
    {
        GameObject quizMainUIGO = Resources.Load<GameObject>(QUIZ_MAIN_UI_LOCATION);
        GameObject.Instantiate(quizMainUIGO, Vector3.zero, Quaternion.identity);
    }
}
