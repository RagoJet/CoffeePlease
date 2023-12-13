using Services;

namespace SaveLoadSystem{
    public class SaveLoadController : ISaveLoadController{
        public void Save(){
            throw new System.NotImplementedException();
        }

        public void Load(){
            throw new System.NotImplementedException();
        }
    }

    public interface ISaveLoadController : IService{
        public void Save();

        public void Load();
    }
}