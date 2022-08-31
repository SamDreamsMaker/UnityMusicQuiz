using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicQuiz;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;

public class QuizMainUI : MonoBehaviour
{
    private QuizManagerComponent quizManagerComponent;
    [SerializeField]
    private GameObject choosePanel, questionPanel, resultPanel;

    [Header("Question Panel")][Space(10)]
    [SerializeField]
    private GameObject questionTitle;
    [SerializeField]
    private GameObject[] buttonAnswers;
    [SerializeField]
    private GameObject[] textAnswers;
    [SerializeField]
    private GameObject resultText, resultBackground;
    [SerializeField]
    private GameObject nextStepButton, nextStepText;

    [Header("Web Content Containers")]
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private GameObject songImage;

    [Header("Result Panel")][Space(10)]
    [SerializeField][Tooltip("The texts where result are displayed at the end of the quiz")]
    private GameObject[] textResultAnswers;
    [SerializeField][Tooltip("The score displayed at the end of the quiz")]
    private GameObject scoreText;

    private Quiz currentQuiz;
    private int currentQuestionId;
    private Question currentQuestion;
    private PlayerScore playerScore;

    private const string PREFIX_QUESTION = "Question n°";
    private const string SHOW_MY_SCORE = "Show my score";
    private const string NEXT_QUESTION = "Next question";
    private const string SCORE = "Score : ";
    private const string RIGHT = "Right";
    private const string WRONG = "Wrong";
    private const int MAX_QUESTION_ID = 4;

    /**** INITIALIZATION ****/
    public void setQuizManagerComponent(QuizManagerComponent aQuizManagerComponent) {
        quizManagerComponent = aQuizManagerComponent;
    }

    public void startQuiz(int id) {
        currentQuiz = quizManagerComponent.getQuiz(id);
        resetQuiz();
        playerScore = new PlayerScore();
        goToQuestions();
        updateQuestion(false);
    }
    /**** DOWNLOAD THE WEB CONTENT ****/
    IEnumerator DownloadImage(string MediaUrl) {
        activeSongImage(false);
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl)) {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.DataProcessingError)
                Debug.Log(request.error);
            else {
                activeSongImage(true);
                songImage.GetComponent<RawImage>().texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            }
        }
    }
    IEnumerator DownloadMusic(string MediaUrl) {
        activeSongClip(false);
        using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(MediaUrl, AudioType.WAV)) {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.DataProcessingError)
                Debug.Log(request.error);
            else {
                audioSource.clip = DownloadHandlerAudioClip.GetContent(request);
                activeSongClip(true);
            }
        }
    }
    /**** MANAGE THE WEB CONTENT VISIBILITY ****/
    public void activeSongImage(bool active) {
        songImage.SetActive(active);
    }
    public void activeSongClip(bool active) {
        if (active) {
            audioSource.Play();
        } else {
            audioSource.Stop();
            audioSource.clip = null;
        }
    }
    /**** RESET VARIABLES ***/
    public void resetQuiz() {
        resetQuestionId();
        updateQuestionTitleId();
        displayNextButton(false);
        disableQuestionResult(false);
        activeSongImage(false);
        activeSongClip(false);
        playerScore = null;
    }
    public void resetQuestionId() {
        currentQuestionId = 0;
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
        displayResult();
    }

    public void goNextStep() {
        activeSongImage(false);
        activeSongClip(false);
        if (currentQuestionId == MAX_QUESTION_ID) goToResult();
        else updateQuestion(true);
    }
    /**** UPDATE QUESTION ****/
    public void updateQuestion(bool increase) {
        if (increase) currentQuestionId++;
        updateQuestionTitleId();
        currentQuestion = currentQuiz.questions[currentQuestionId];
        updateQuestionButtonText();
        displayNextButton(false);
        disableQuestionResult(false);
        ActiveAnswerButtons(true);
        StartCoroutine(DownloadImage(currentQuestion.song.picture));
        StartCoroutine(DownloadMusic(currentQuestion.song.sample));
    }
    public void updateQuestionButtonText() {
        int count = 0;
        foreach (GameObject textAnswer in textAnswers) {
            textAnswer.GetComponent<Text>().text = currentQuestion.choices[count].getTextData();
            count++;
        }
    }
    public void updateQuestionTitleId() {
        questionTitle.GetComponent<Text>().text = PREFIX_QUESTION + (currentQuestionId+1).ToString();
    }
    /**** ANSWER BUTTONS : COLOR MANAGEMENT AND ACTIVATION ***/
    public void ColorForSelectedButton(int id) {
        buttonAnswers[id].GetComponent<Image>().color = Color.grey;
    }
    public void ResetAnswerButtonColor() {
        foreach (GameObject button in buttonAnswers) {
            button.GetComponent<Image>().color = Color.white;
        }
    }
    public void ActiveAnswerButtons(bool active) {
        foreach (GameObject button in buttonAnswers) {
            button.GetComponent<Button>().interactable = active;
        }
        if (active) ResetAnswerButtonColor();
    }
    /**** ANSWER THEN UPDATE UI AND VARIABLES ***/
    public void answer(int id) {
        ColorForSelectedButton(id);
        bool win = currentQuestion.answerIndex == id;
        if (win) {
            playerScore.increaseScore();
            updateQuestionResult(true);
        } else {
            updateQuestionResult(false);
        }
        playerScore.addAnswer(currentQuestionId, id, win);
        displayNextButton(true);
        if(currentQuestionId == MAX_QUESTION_ID) {
            nextStepText.GetComponent<Text>().text = SHOW_MY_SCORE;
        } else {
            nextStepText.GetComponent<Text>().text = NEXT_QUESTION;
        }
        ActiveAnswerButtons(false);
    }
    public void updateQuestionResult(bool win) {
        resultBackground.SetActive(true);
        string result;
        if (win) {
            result = RIGHT;
            resultBackground.GetComponent<Image>().color = Color.green;
        } else {
            result = WRONG;
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
    /**** SHOW THE SCORE & RESULT ****/
    public void displayResult() {
        int count = 0;
        foreach (GameObject text in textResultAnswers) {
            bool win = playerScore.hasWin(count);
            string winText;
            if (win) {
                winText = RIGHT;
            } else {
                winText = WRONG;
            }
            text.GetComponent<Text>().text = currentQuiz.questions[count].song.getTextData() + Environment.NewLine + winText;
            count++;
        }
        scoreText.GetComponent<Text>().text = SCORE + playerScore.getScore().ToString();
    }
}