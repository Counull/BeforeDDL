using C_Sharp.Models;
using UnityEngine;

namespace C_Sharp.Launcher {
    public class GameLauncher : MonoBehaviour {
        TitleStartUI _titleStartUI;
        LauncherLoadingInfo _loadingInfo;


        private void Awake() { }

        void LoadProcessStart() {
            _loadingInfo = new LauncherLoadingInfo {
                Progress = 0,
                LoadingText = "Loading..."
            };
            _titleStartUI.StartLoadingProcess();
            _titleStartUI.LoadingProcessChange(_loadingInfo);
        }
    }
}