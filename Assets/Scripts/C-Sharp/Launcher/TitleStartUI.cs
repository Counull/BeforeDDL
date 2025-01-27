using C_Sharp.Models;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace C_Sharp.Launcher {
    public class TitleStartUI : MonoBehaviour {
        [ShowInInspector] private Slider _loadingBar;
        [ShowInInspector] Button _startButton;
        private Text _loadingText;


        // private LauncherLoadingInfo _loadingInfo;

        private void HideAllFunctionalComponents() {
            _loadingBar.gameObject.SetActive(false);
            _startButton.gameObject.SetActive(false);
        }

        public void StartLoadingProcess() {
            HideAllFunctionalComponents();
            _loadingBar.gameObject.SetActive(true);
        }

        public void LoadingProcessChange(LauncherLoadingInfo info) {
            _loadingText.text = info.LoadingText;
            _loadingBar.value = info.Progress;
        }

        public void LoadingProcessComplete() { }

        public void ShowStartButton() {
            HideAllFunctionalComponents();
            _startButton.gameObject.SetActive(true);
        }
    }
}