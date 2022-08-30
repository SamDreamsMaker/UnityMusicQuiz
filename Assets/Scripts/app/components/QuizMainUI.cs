using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicQuiz;
using UnityEngine.UI;

public class QuizMainUI : MonoBehaviour
{
    private QuizManagerComponent quizManagerComponent;
    [SerializeField]
    private GameObject choosePanel, questionPanel, resultPanel;
    [SerializeField]
    private GameObject questionTitle;
    [SerializeField]
    private GameObject buttonAnswer1, buttonAnswer2, buttonAnswer3, buttonAnswer4;
    [SerializeField]
    private GameObject resultText, resultBackground;
    [SerializeField]
    private GameObject nextStepButton, nextStepText;

    private Quiz currentQuiz;
    private int currentQuestionId;
    private Question currentQuestion;
    private int currentScore;

    private const string PREFIX_QUESTION = "Question n°";
    private const string SHOW_MY_SCORE = "Show my score";
    private const string NEXT_QUESTION = "Next question";

    /**** INITIALIZATION ****/
    public void setQuizManagerComponent(QuizManagerComponent aQuizManagerComponent) {
        quizManagerComponent = aQuizManagerComponent;
    }

    public void startQuiz(int id) {
        currentQuiz = quizManagerComponent.getQuiz(id);
        resetQuiz();
        goToQuestions();
    }

    /**** RESET VARIABLES ***/
    public void resetQuiz() {
        resetQuestionId();
        resetScore();
        updateQuestionTitleId();
        displayNextButton(false);
        disableQuestionResult(false);
    }
    public void resetQuestionId() {
        currentQuestionId = 0;
    }
    public void resetScore() {
        currentScore = 0;
    }

    /**** NAVIGATION ****/
    public void closeChooseMenu() {
        choosePanel.SetActive(false);
    }
    public void openChooseMenu() {
        choosePanel.SetActive(true);
    }

    public void closeQuestionMenu() {
        questionPanel.SetActive(false);
    }
    public void openQuestionMenu() {
        questionPanel.SetActive(true);
    }

    public void closeResultMenu() {
        resultPanel.SetActive(false);
    }
    public void openResultMenu() {
        resultPanel.SetActive(true);
    }

    public void goToQuestions() {
        closeChooseMenu();
        closeResultMenu();
        openQuestionMenu();
    }
    public void goToChooseMenu() {
        closeQuestionMenu();
        closeResultMenu();
        openChooseMenu();
    }
    public void goToResult() {
        closeChooseMenu();
        closeQuestionMenu();
        openResultMenu();
    }

    public void goNextStep() {
        if (currentQuestionId == 3) goToResult();
        else updateQuestion();
    }

    /**** UPDATE QUESTION ****/
    public void updateQuestion() {
        currentQuestionId++;
        updateQuestionTitleId();
        currentQuestion = currentQuiz.questions[currentQuestionId];
        updateQuestionButtonText();
        displayNextButton(false);
        disableQuestionResult(false);
    }
    public void updateQuestionButtonText() {
        buttonAnswer1.GetComponent<Text>().text = currentQuestion.choices[0].artist + " - " + currentQuestion.choices[0].title;
        buttonAnswer2.GetComponent<Text>().text = currentQuestion.choices[1].artist + " - " + currentQuestion.choices[1].title;
        buttonAnswer3.GetComponent<Text>().text = currentQuestion.choices[2].artist + " - " + currentQuestion.choices[2].title;
        buttonAnswer4.GetComponent<Text>().text = currentQuestion.choices[3].artist + " - " + currentQuestion.choices[3].title;
    }
    public void updateQuestionTitleId() {
        questionTitle.GetComponent<Text>().text = PREFIX_QUESTION + (currentQuestionId+1).ToString();
    }

    /**** ANSWER THEN UPDATE UI AND VARIABLES ***/
    public void answer(int id) {
        Debug.Log("anwser " + id);
        if (currentQuestion.answerIndex == id) {
            currentScore++;
            updateQuestionResult(true);
        } else {
            updateQuestionResult(false);
        }
        displayNextButton(true);
        if(currentQuestionId == 3) {
            nextStepText.GetComponent<Text>().text = SHOW_MY_SCORE;
        } else {
            nextStepText.GetComponent<Text>().text = NEXT_QUESTION;
        }
    }
    public void updateQuestionResult(bool win) {
        string result;
        if (win) {
            result = "right";
            resultBackground.GetComponent<Image>().color = Color.green;
        } else {
            result = "wrong";
            resultBackground.GetComponent<Image>().color = Color.red;
        }
        resultText.GetComponent<Text>().text = result;
    }
    public void displayNextButton(bool visible) {
        nextStepButton.SetActive(visible);
    }
    public void disableQuestionResult(bool visible) {
        resultBackground.SetActive(visible);
    }
}