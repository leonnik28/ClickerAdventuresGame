using Zenject;

public class CloudInstaller : MonoInstaller<CloudInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<StorageService>().FromNew().AsSingle();
        Container.Bind<CloudManager>().FromNew().AsSingle();
    }
}