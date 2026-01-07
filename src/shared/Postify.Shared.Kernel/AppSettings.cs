namespace Postify.Shared.Kernel;

public class AppSettings
{
    public ModulesSettings Modules { get; set; }
}

public class ModulesSettings
{
    public SmsSettings Sms { get; set; }
}
public class SmsSettings
{
    public KavehNegarSettings KavehNegar { get; set; }

}

public class KavehNegarSettings
{
    public string ApiKey { get; set; }
    public string SenderNumber { get; set; }
}
