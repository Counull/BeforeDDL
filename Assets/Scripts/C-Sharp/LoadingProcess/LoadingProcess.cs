using Unity.VisualScripting;

namespace C_Sharp.Launcher {
    public abstract class LoadingProcess {
        LoadingConfig _config;

        public void Start(LoadingConfig config) {
            _config = config;
        }
    }
}