using SaveLoadSystem;
using Services;
using UnityEngine;

public class StartGame : MonoBehaviour{
    private void Awake(){
        DataContainer dataContainer = new DataContainer();
        AllServices.Instance.Register<IDataContainer>(dataContainer);
        SaveLoadController saveLoadController = new SaveLoadController();
        AllServices.Instance.Register<ISaveLoadController>(saveLoadController);
    }
}