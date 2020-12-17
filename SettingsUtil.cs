using System.Configuration;

namespace MOTDetails
{
    public static class SettingsUtil
    {
        public static void SetSettingOrThrow(string settingKey, out string target)
        {
            target = ConfigurationManager.AppSettings[settingKey];
            if (string.IsNullOrWhiteSpace(target))
                throw new ConfigurationErrorsException($"Must provide config value {settingKey}, application will terminate.");
        }
    }
}
