using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using static OrderService.Configuration.ConfigurationSettings;

namespace OrderService.Configuration
{
    public interface IConfigurationSettings
    {
        DeploymentEnvironmentEnum Environment { get; }
    }

    [Development, Production]
    public class ConfigurationSettings : IConfigurationSettings
    {
        public ConfigurationSettings()
        {
            var attribute = typeof(ConfigurationSettings)
                .GetCustomAttributes(typeof(DeploymentEnvironmentAttribute), true)
                .Single() as DeploymentEnvironmentAttribute;

            if (attribute is DevelopmentAttribute)
                this.Environment = DeploymentEnvironmentEnum.Development;
            else
            if (attribute is ProductionAttribute)
                this.Environment = DeploymentEnvironmentEnum.Production;
            else
                throw new Exception("Unknown deployment environment");
        }

        public enum DeploymentEnvironmentEnum
        {
            Development,
            Production
        }

        public DeploymentEnvironmentEnum Environment { get; private set; }

        private class DeploymentEnvironmentAttribute : Attribute
        { }

        [Conditional("DEBUG")]
        private class DevelopmentAttribute : DeploymentEnvironmentAttribute
        { }

        [Conditional("RELEASE")]
        private class ProductionAttribute : DeploymentEnvironmentAttribute
        { }
    }
}