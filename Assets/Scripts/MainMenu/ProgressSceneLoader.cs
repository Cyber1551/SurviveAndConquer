using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgressSceneLoader : MonoBehaviour {

    [SerializeField] private Text progressText;
    [SerializeField] private Slider slider;

    public GameObject panel;
    private AsyncOperation operation;
    
	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	public void LoadScene (string sceneName) {
        UpdateProgressUI(0);
        panel.gameObject.SetActive(true);
        StartCoroutine(BeginLoad(sceneName));
        
	}
    private IEnumerator BeginLoad(string sceneName)
    {
        operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            UpdateProgressUI(operation.progress);
            yield return null; 
        }
        UpdateProgressUI(operation.progress);
        operation = null;
        panel.gameObject.SetActive(false);
    }
    private void UpdateProgressUI(float progress)
    {
        slider.value = progress;
        progressText.text = (int)(progress * 100f) + "%";
    }
}
