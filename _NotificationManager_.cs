using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.SimpleAndroidNotifications;

public class _NotificationManager_ : MonoBehaviour
{

	float EveryXSeconds;
	float TimeRemaining;
	float tempValue;
	public Text TimeRemainingText;
	public Text InputFieldText;
	public Text PauseUnpause;
	public Button PauseButtonButton;
	ColorBlock theColors;

	bool startTimer = false;
	bool onPauseClick = true;
	bool switchState = false;

	private void FixedUpdate()
	{
		if (onPauseClick)
		{
			if (startTimer)
			{
				TimeRemaining -= Time.deltaTime;
				TimeRemainingText.text = TimeRemaining.ToString("0");
				if (TimeRemaining <= 0)
				{
					TimeRemaining = EveryXSeconds;
					CreateNotification(TimeRemaining);
				}
			}
			
		}
	}

	public void PauseButton()
	{
		theColors = PauseButtonButton.GetComponent<Button>().colors;
		if (onPauseClick)
		{
			StopNotification();
			PauseUnpause.text = "Unpause";
			

			tempValue = float.Parse(TimeRemainingText.text);
			onPauseClick = false;
		}
		else
		{
			

			PauseUnpause.text = "Pause";
			CreateNotification(tempValue);
			onPauseClick = true;
		}

	}

	public void StartButton()
	{
		if (InputFieldText.text == "")
		{
			EveryXSeconds = 30;
		}
		else
		{
			EveryXSeconds = float.Parse(InputFieldText.text);
		}

		TimeRemaining = EveryXSeconds;

		PauseUnpause.text = "Pause";
		CreateNotification(TimeRemaining);
		onPauseClick = true;
		startTimer = true;

		InputFieldText.text = "";
	}

	public void StopButton()
	{
		onPauseClick = false;
		startTimer = false;
		TimeRemainingText.text = "";
		InputFieldText.text = null;
		PauseUnpause.text = "Pause";
		StopNotification();
	}

	public void ExitButton()
	{
		StopNotification();
		Application.Quit();
	}

	public void CreateNotification(float TimeRemaining)
	{
		string Message;
		Message = TimeRemaining + " seconds passed!";
		NotificationManager.SendWithAppIcon(TimeSpan.FromSeconds(TimeRemaining), "Hey Wake Up!", Message, new Color(1, 0.3f, 0.15f), NotificationIcon.Clock);
	}
	public void StopNotification()
	{
		NotificationManager.CancelAll();
	}
}
