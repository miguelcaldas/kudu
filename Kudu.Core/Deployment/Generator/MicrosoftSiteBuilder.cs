﻿using Kudu.Contracts.Settings;
using System;
using System.Text;

namespace Kudu.Core.Deployment.Generator
{

    public abstract class MicrosoftSiteBuilder : GeneratorSiteBuilder
    {
        protected string SolutionPath { get; private set; }

        protected string CommandArgument { get; private set; }

        protected string ProjectFilePath { get; private set; }

        protected string TargetFramework { get; private set; }

        protected MicrosoftSiteBuilder(IEnvironment environment, IDeploymentSettingsManager settings, IBuildPropertyProvider propertyProvider, string sourcePath, string projectFilePath, string solutionPath, string commandArgument, string targetFramework="")
            : base(environment, settings, propertyProvider, sourcePath)
        {
            ProjectFilePath = CleanPath(projectFilePath);
            SolutionPath = CleanPath(solutionPath);
            CommandArgument = commandArgument;
            TargetFramework = targetFramework;
        }

        protected override string ScriptGeneratorCommandArguments
        {
            get
            {
                var commandArguments = new StringBuilder();
                commandArguments.AppendFormat("{0} \"{1}\"", CommandArgument, ProjectFilePath);

                if (!String.IsNullOrEmpty(SolutionPath))
                {
                    commandArguments.AppendFormat(" --solutionFile \"{0}\"", SolutionPath);
                }
                else
                {
                    commandArguments.AppendFormat(" --no-solution");
                }

                if (!String.IsNullOrEmpty(TargetFramework))
                {
                    commandArguments.AppendFormat(" --targetFramework \"{0}\"", TargetFramework);
                }

                return commandArguments.ToString();
            }
        }
    }
}
