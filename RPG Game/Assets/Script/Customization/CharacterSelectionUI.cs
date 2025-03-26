using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform previewPoint;
    private GameObject currentPreview;

    private void Start()
    {
        ClearPreviewPoint();
        LoadSelectedCharacter();
        UpdateCharacterPreview();
    }

    public void NextCharacter()
    {
        int currentIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        currentIndex = (currentIndex + 1) % characterPrefabs.Length;
        PlayerPrefs.SetInt("SelectedCharacter", currentIndex);
        UpdateCharacterPreview();
    }

    public void PreviousCharacter()
    {
        int currentIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        currentIndex = (currentIndex - 1 + characterPrefabs.Length) % characterPrefabs.Length;
        PlayerPrefs.SetInt("SelectedCharacter", currentIndex);
        UpdateCharacterPreview();
    }

    private void UpdateCharacterPreview()
    {
        int selectedIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);

        if (selectedIndex < 0 || selectedIndex >= characterPrefabs.Length)
        {
            Debug.LogWarning("Invalid index, resetting to 0");
            selectedIndex = 0;
            PlayerPrefs.SetInt("SelectedCharacter", 0);
        }

        ClearPreviewPoint();

        currentPreview = Instantiate(characterPrefabs[selectedIndex], previewPoint);
        currentPreview.transform.localPosition = Vector3.zero;
        currentPreview.transform.localRotation = Quaternion.Euler(0, 135, 0);
        currentPreview.transform.localScale = Vector3.one * 1.2f;
    }

    private void LoadSelectedCharacter()
    {
        if (!PlayerPrefs.HasKey("SelectedCharacter"))
        {
            PlayerPrefs.SetInt("SelectedCharacter", 0);
        }
        UpdateCharacterPreview();
    }

    private void ClearPreviewPoint()
    {
        foreach (Transform child in previewPoint)
        {
            Destroy(child.gameObject);
        }
    }
}