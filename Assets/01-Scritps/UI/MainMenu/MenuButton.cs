using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour, ISelectHandler, IDeselectHandler 
{
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
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Application.Quit();
    }
}