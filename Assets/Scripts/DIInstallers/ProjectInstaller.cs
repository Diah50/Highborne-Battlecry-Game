using Zenject;

namespace Highborne.DIInstallers
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        public override void InstallBindings()
        {
            AppInstaller.Install(Container);
        }
    }
}