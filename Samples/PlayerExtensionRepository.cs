using System;
using Xo.LiquidFramework;

public class PlayerExtensionRepository : ExtensionRepository
{
    private void Start()
    {
        IEArgsOutput output = new IEArgsOutput1();
        InvokeEvent("OnBulletHıt",new IEArgsInput1(),ref output);
    }
    
    public class IEArgsInput1 : IEArgsInput
    {
        
    }
    public class IEArgsOutput1 : IEArgsOutput
    {
        
    }
}
