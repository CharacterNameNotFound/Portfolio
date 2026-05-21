using Newtonsoft.Json;
using Zenject;

namespace Structure
{
    public class UtilityInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallJsonSettings();
        }

        private void InstallJsonSettings()
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
            };
            
            Container.Bind<JsonSerializerSettings>().FromInstance(jsonSerializerSettings).AsSingle();
        }
        
    }
}