namespace Xo.LiquidFramework
{
    public interface IExtension
    { 
        void Init(ExtensionRepository extensionRepository);
        IEArgsOutput Execute(IEArgsInput ieArgsInput);
    }
}

