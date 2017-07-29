using System;
using UnityEngine;


public interface ThemeableScene {
	GameObject getBackground1();
	GameObject getBackground2();
	GameObject getHills1();
	GameObject getHills2();
	GameObject getForeground1();
	GameObject getForeground2();

	GameObject getBtnBack();
	GameObject getBtnForward();
	GameObject getBtnInfo();
	GameObject getBtnSettings();
	GameObject getBtnChecked();
	GameObject getBtnUnChecked();
	GameObject getJumbotron();
}