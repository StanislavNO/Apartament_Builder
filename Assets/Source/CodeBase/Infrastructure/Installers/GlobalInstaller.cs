using Source.CodeBase.Configs;
using UnityEngine;
using Zenject;

namespace Source.CodeBase.Infrastructure.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private BuildConfig _buildConfig;
        
        public override void InstallBindings()
        {
            BindConfigs();
        }

        private void BindConfigs()
        {
            Container.BindInstance(_buildConfig).AsSingle();
        }
    }
}