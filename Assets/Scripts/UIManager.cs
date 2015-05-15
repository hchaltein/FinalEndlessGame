using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class stores references to key pieces of UI so that they do not need to be looked up multiple times.
/// </summary>
public class UIManager : Singleton<UIManager>
{
	public GameObject ScreenSpaceOverlayPrefab;
	public GameObject AbilityIconPrefab;
	public Canvas UIROOT;
	public List<Ability> Abilities;

	public Canvas pause_Menu;

	public Sprite[] Icons;

    public bool canUse;
	public bool paused;

	public override void Awake()
	{
		base.Awake();
	}

	public void Init()
	{
		// List of Abilitys
        Abilities = new List<Ability>();

		//Load the UI element
		AbilityIconPrefab = Resources.Load<GameObject>("UI/AbilityIcon");
		ScreenSpaceOverlayPrefab = Resources.Load<GameObject>("UI/SS - Overlay");

		// Find Canvas Objects to load Ability Icons to:
        GameObject go = GameObject.Find("SS - Overlay");
		if (go != null)
		{
			// Get Canvas component from SS Overlay
            UIROOT = go.GetComponent<Canvas>();
		}
		if (UIROOT == null)
		{
            // Instantiate SS Overlay and Get Canvas component from it.
			UIROOT = ((GameObject)GameObject.Instantiate(ScreenSpaceOverlayPrefab, Vector3.zero, Quaternion.identity)).GetComponent<Canvas>();
			UIROOT.gameObject.name = ScreenSpaceOverlayPrefab.name;
		}

		// Loads all Ability Sprites to be loaded on the buttoms
        Icons = new Sprite[10];
		Icons = Resources.LoadAll<Sprite>("Abilities");

        // Get Abilities Folder Object from Screen Overlay.
		GameObject abilityFolder = UIROOT.transform.FindChild("Abilities").gameObject;

		// Instantiate all abilities.
        GameObject newAbilityGO;
		for (int i = 0; i < 3; i++)
		{
            // Instantiate the Ability Object.
            Ability newAbility = ScriptableObject.CreateInstance<Ability>();

			// Instantiate Ability Container PreFab.
            newAbilityGO = (GameObject) GameObject.Instantiate(AbilityIconPrefab, Vector3.zero, Quaternion.identity);

            // Put the Game Object inside the Abilities Folder
			newAbilityGO.transform.SetParent(abilityFolder.transform);
			newAbilityGO.transform.localPosition = new Vector3(0.0f, (i*80.0f), 0.0f);
            newAbilityGO.name = "Ability " + (i + 1) % 10;

			
            // Set Up Abilities charges and Cooldown.
            newAbility.index = i;
		
            newAbility.cooldownDuration = Random.Range(1, 3);
            newAbility.charges = 5;
			newAbility.unlimited = false;

			// Reset Ability Cooldown counter.
            newAbility.cooldownLeft = 0;

            // Set Up Ability Background and Icon sprite.
			newAbility.AbilityContainer = newAbilityGO;
			newAbility.AbilityBackground = newAbilityGO.transform.FindChild("Background").GetComponent<Image>();
			newAbility.AbilityIcon = newAbilityGO.transform.FindChild("AbilityIcon").GetComponent<Image>();

			newAbility.AbilityIcon.sprite = Icons[i];
			newAbility.AbilityBackground.sprite = Icons[11];
			newAbility.CooldownDisplay = newAbilityGO.transform.FindChild("Cooldown").GetComponent<Image>();
			newAbility.CooldownDisplay.sprite = Icons[11];

			newAbility.ChargeDisplay = newAbilityGO.transform.FindChild("Remainder").GetComponent<Text>();
			newAbility.ChargeDisplay.text = newAbility.charges.ToString();

			Abilities.Add(newAbility);
		}

		UnpauseGame();
	}

	bool wasFullScreen;
	void Update()
	{
		for (int i = 0; i < Abilities.Count; i++)
		{
			Abilities[i].Update();
		}

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(paused)
			{
				UnpauseGame();
			}
			else
			{
				PauseGame();
			}
		}
		if (Input.GetKeyDown(KeyCode.T))
		{
			for (int i = 0; i < Abilities.Count; i++)
			{
				Abilities[i].DisableAbility();
			}
		}
		if (Input.GetKeyDown(KeyCode.Y))
		{
			for (int i = 0; i < Abilities.Count; i++)
			{
				Abilities[i].EnableAbility();
			}
		}

		if (!paused)
		{

#if !UNITY_EDITOR
			Screen.lockCursor = true;
#endif
		}
	}

	public void PauseGame()
	{
		paused = true;
		Time.timeScale = 0f;
		//pause_Menu.gameObject.SetActive(paused);
	}

	public void UnpauseGame()
	{
		paused = false;
		Time.timeScale = 1.0f;
		//pause_Menu.gameObject.SetActive(paused);
	}
}
