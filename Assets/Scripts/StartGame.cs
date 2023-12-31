using SaveLoadSystem;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour{
    private void Awake(){
        AllServices.Instance.Register<IDataContainer>(new DataContainer());
        AllServices.Instance.Register<ISaveLoadController>(new SaveLoadController());
        AllServices.Instance.Register<IGameObjectsFactory>(new GameObjectsFactory());

        Canvas canvas = Instantiate(Resources.Load<Canvas>("MyCanvas"));
        Joystick joystick = Instantiate(Resources.Load<Joystick>("Floating Joystick"), canvas.transform);
        AllServices.Instance.Register<IInputSystem>(new MyJoyStick(joystick));

        AudioPlayer audioPlayer = Instantiate(Resources.Load<AudioPlayer>("AudioPlayer"));
        AllServices.Instance.Register<IAudioPlayer>(audioPlayer);


        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(audioPlayer);
        SceneManager.LoadScene("Game");
    }
}