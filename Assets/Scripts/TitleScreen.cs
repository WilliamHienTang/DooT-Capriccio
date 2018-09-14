﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleScreen : MonoBehaviour {

    public Image background;
    public Text text;
    public string loadLevel;
    int touches;

    IEnumerator Start()
    {
        enabled = false;
        background.canvasRenderer.SetAlpha(0.0f);
        text.canvasRenderer.SetAlpha(0.0f);
        FadeInImage();
        yield return new WaitForSeconds(1.0f);
        FadeInText();
        yield return new WaitForSeconds(1.0f);
        touches = 0;
        enabled = true;
    }

    void FadeInImage()
    {
        background.CrossFadeAlpha(1.0f, 1.0f, false);
    }
	
    void FadeInText()
    {
        text.CrossFadeAlpha(1.0f, 1.0f, false);
    }

    // Start game after n text blinks
    IEnumerator BlinkText(int n)
    {
        for (int i = 0; i < n; i++)
        {
            Color c = text.color;
            text.color = new Color(c.r, c.g, c.b, 0);
            yield return new WaitForSeconds(0.05f);
            text.color = new Color(c.r, c.g, c.b, 1);
            yield return new WaitForSeconds(0.05f);
        }
        StartGame();
    }

    void StartGame()
    {
        SceneManager.LoadScene(loadLevel);
    }

    // Update is called once per frame
    void Update () {
        touches = Input.touchCount;
        if (touches > 0 || Input.GetMouseButtonDown(0))
        {
            StartCoroutine(BlinkText(4));
        }
	}
}