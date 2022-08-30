using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicQuiz;

public class QuizComponent : MonoBehaviour
{
    [SerializeField]
    private TextAsset textJSON;
    [SerializeField]
    private QuizData quizData;

    private void Awake() {
        quizData = MusicQuiz.QuizFactory.createQuizData(textJSON.text);
    }

    void Start()
    {
        Debug.Log(quizData.quizs.Length);
        Debug.Log(quizData.quizs[0].id);
    }
}
