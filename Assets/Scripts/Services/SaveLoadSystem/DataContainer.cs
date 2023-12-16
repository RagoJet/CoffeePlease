using Services;

namespace SaveLoadSystem{
    public class DataContainer : IDataContainer{
        public int money;
    }

    public interface IDataContainer : IService{
    }
}