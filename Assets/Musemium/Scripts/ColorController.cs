using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorController {
	private static Color[] colors_ = {
		Color.blue,
		Color.green,
		Color.yellow,
		Color.red,
		Color.magenta,
		Color.black,
		Color.white
	};
		
	private static List<Color> paintingColors = new List<Color>(colors_);
	private static int currentPaintingColor = 0;
	private static Color mainColor = Color.gray;

	public ColorController (){
		paintingColors = new List<Color>(colors_);
	}

	public static void NextColor(){
		currentPaintingColor = (currentPaintingColor + 1) % paintingColors.Count;
	}

	public static void PreviousColor(){
		currentPaintingColor = (currentPaintingColor + paintingColors.Count - 1) % paintingColors.Count;
	}

	public static Color CurrentColor(){
		Color currentColor;
		if (SceneManager.GetActiveScene ().name == "paintingScene") {
			
			currentColor = paintingColors [currentPaintingColor];
		}
		else
			currentColor = mainColor;

		return currentColor;
	}
}