using System.Configuration;
using System.IO;

namespace Common.Application.Logging
{
    public class RollingFileAppender : log4net.Appender.RollingFileAppender
    {
        public override string File
        {
            get { return base.File; }

            set
            {
                if (Path.IsPathRooted(value))
                {
                    base.File = value;
                }
                else
                {
                    var UserConfig =
                        ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
                    base.File = Path.Combine(Path.GetDirectoryName(UserConfig.FilePath), value);
                }
            }
        }
    }
}