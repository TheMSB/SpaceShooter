using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public Menu CurrentMenu;

	public void Start() {
		ShowMenu (CurrentMenu);
	}

	public void ShowMenu(Menu menu) {
		if (CurrentMenu != null) {
			CurrentMenu.isOpen = false;
		}

		CurrentMenu = menu;
		CurrentMenu.isOpen = true;
	}

	/** Load the specified scene.*/
	public void LoadScene(int level) {
		SceneManager.LoadScene (level);
	}
}
