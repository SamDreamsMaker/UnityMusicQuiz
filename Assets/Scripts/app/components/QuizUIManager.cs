using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizUIManager : MonoBehaviour
{
    private const string QUIZ_MAIN_UI_LOCATION = "UI/QuizMainUI";
    private GameObject quizMainUIGO;
    private QuizManagerComponent quizManagerComponent;

    void Start()
    {
        quizManagerComponent = gameObject.GetComponent<QuizManagerComponent>();
        createQuizMainUI();
    }

    
    void createQuizMainUI()
    {
        GameObject quizMainUIGO = Resources.Load<GameObject>(QUIZ_MAIN_UI_LOCATION);
        GameObject result = GameObject.Instantiate(quizMainUIGO, Vector3.zero, Quaternion.identity);
        result.GetComponent<QuizMainUI>().setQuizManagerComponent(quizManagerComponent);
    }
}
