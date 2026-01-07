using System.Reflection;
namespace Postify.ArchitectureTests.Base;

public abstract class BaseTest
{
    protected static readonly Assembly ProfileCoreAssembly = typeof(Postify.Modules.Profile.Core.Entities.ProfileBase).Assembly;
    protected static readonly Assembly MediaCoreAssembly = typeof(Postify.Modules.Media.Core.Entities.ObjectFile).Assembly;
    protected static readonly Assembly NotifyCoreAssembly = typeof(Postify.Modules.Notify.Core.Entities.SmsNotificationEvent).Assembly;
    protected static readonly Assembly ProofCoreAssembly = typeof(Postify.Modules.Proof.Core.Entities.MessageProof).Assembly;
    protected static readonly Assembly ShortakCoreAssembly = typeof(Postify.Modules.Shortak.Core.Entities.ShortUrl).Assembly;

    protected static readonly Assembly ProfileInfrastructureAssembly = typeof(Postify.Modules.Profile.Infrastructure.Persistence.ProfileDbContext).Assembly;
    protected static readonly Assembly MediaInfrastructureAssembly = typeof(Postify.Modules.Media.Infrastructure.Persistence.MediaDbContext).Assembly;
    protected static readonly Assembly NotifyInfrastructureAssembly = typeof(Postify.Modules.Notify.Infrastructure.Persistence.SmsDbContext).Assembly;
    protected static readonly Assembly ProofInfrastructureAssembly = typeof(Postify.Modules.Proof.Infrastructure.Persistence.ProofDbContext).Assembly;
    protected static readonly Assembly ShortakInfrastructureAssembly = typeof(Postify.Modules.Shortak.Infrastructure.Persistence.ShortakDbContext).Assembly;

    protected static readonly Assembly ProfileEntryAssembly = typeof(Postify.Modules.Profile.ModuleExtensions).Assembly;
    protected static readonly Assembly MediaEntryAssembly = typeof(Postify.Modules.Media.ModuleExtensions).Assembly;
    protected static readonly Assembly NotifyEntryAssembly = typeof(Postify.Modules.Notify.ModuleExtensions).Assembly;
    protected static readonly Assembly ProofEntryAssembly = typeof(Postify.Modules.Proof.ModuleExtensions).Assembly;
    protected static readonly Assembly ShortakEntryAssembly = typeof(Postify.Modules.Shortak.ModuleExtensions).Assembly;
    
    protected static readonly string[] ModuleNames = { "Profile", "Media", "Notify", "Proof", "Shortak" };

    public static TheoryData<string> GetModuleNames()
    {
        var data = new TheoryData<string>();
        foreach (var name in ModuleNames)
        {
            data.Add(name);
        }
        return data;
    }
    
    protected static readonly Assembly[] ModuleEntryAssemblies = [
        ProfileEntryAssembly,
        MediaEntryAssembly,
        NotifyEntryAssembly,
        ProofEntryAssembly,
        ShortakEntryAssembly
    ];

    protected static readonly Assembly[] CoreAssemblies = {
        ProfileCoreAssembly,
        MediaCoreAssembly,
        NotifyCoreAssembly,
        ProofCoreAssembly,
        ShortakCoreAssembly
    };

    protected static readonly Assembly[] InfrastructureAssemblies = {
        ProfileInfrastructureAssembly,
        MediaInfrastructureAssembly,
        NotifyInfrastructureAssembly,
        ProofInfrastructureAssembly,
        ShortakInfrastructureAssembly
    };

    protected static readonly Assembly[] AllAssemblies = [
        ..CoreAssemblies, 
        ..InfrastructureAssemblies, 
        ..ModuleEntryAssemblies
    ];


    public static TheoryData<Assembly> GetCoreAssemblies()
    {
        var data = new TheoryData<Assembly>();
        
        foreach (var asm in CoreAssemblies)
        {
            data.Add(asm); 
        }
        
        return data;
    }
    
}