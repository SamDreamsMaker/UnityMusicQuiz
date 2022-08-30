using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicQuiz;

public class QuizManagerComponent : MonoBehaviour
{
    [SerializeField]
    private TextAsset textJSON;
    [SerializeField]
    private QuizData quizData;

    private void Awake() {
        quizData = MusicQuiz.QuizFactory.createQuizData(textJSON.text);
    }

    public Quiz getQuiz(int id) {
        return quizData.quizs[id];
    }

}
