using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuElement : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [Header("Canvas")]
    [SerializeField] private Canvas _mainMenuCanvas;
    [SerializeField] private Canvas _optionsMenuCanvas;

    private TMP_Text _buttonText;
    private Color _buttonTextColor;
    private void Start()
    {
        _buttonText = GetComponentInChildren<TMP_Text>();
        _buttonTextColor = _buttonText.color;
    }

    public void OnSelect(BaseEventData eventData)
    {
        _buttonText.color = Color.white;
        _buttonText.fontStyle = FontStyles.Bold;
    }
    
    public void OnDeselect(BaseEventData eventData)
    {
        _buttonText.color = _buttonTextColor;
        _buttonText.fontStyle = FontStyles.Normal;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game_EN");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ChangeMenu(GameObject menu)
    {
        menu.gameObject.SetActive(true);
        menu.GetComponentsInChildren<Button>()[0].Select();
        transform.parent.gameObject.SetActive(false);
    }

    public void ToMainMenu()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            TableTarget.ResetTableCounter();
            SceneManager.LoadScene("Menu");
            AudioManager.UpdateVolume();
        }
        else
        {
            MainMenuCanvas.Instance.gameObject.SetActive(true);
            MainMenuCanvas.Instance.gameObject.GetComponentsInChildren<Button>()[0].Select();
            OptionsMenuCanvas.Instance.gameObject.SetActive(false);
        }
    }
    public void ToOptionsMenu()
    {
        OptionsMenuCanvas.Instance.gameObject.SetActive(true);
        GameObject.FindWithTag("OptionsMenu").GetComponentsInChildren<Button>()[0].Select();
        MainMenuCanvas.Instance.gameObject.SetActive(false);
    }
    
}