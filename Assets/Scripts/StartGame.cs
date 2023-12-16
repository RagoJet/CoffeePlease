using SaveLoadSystem;
using Services;
using UnityEngine;

public class StartGame : MonoBehaviour{
    private void Awake(){
        AllServices.Instance.Register<IDataContainer>(new DataContainer());
        AllServices.Instance.Register<ISaveLoadController>(new SaveLoadController());
        AllServices.Instance.Register<IPortableObjectsFactory>(new PortableObjectsFactory());
    }
}