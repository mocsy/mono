﻿//------------------------------------------------------------------------------
// <copyright file="BinaryCompatibility.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Web.Util {
    using System;
    using System.Runtime.Versioning;

    // This class contains utility methods that mimic the mscorlib internal System.Runtime.Versioning.BinaryCompatibility type.

    internal sealed class BinaryCompatibility {

        // We need to use this AppDomain key instead of AppDomainSetup.TargetFrameworkName since we don't want applications
        // which happened to set TargetFrameworkName and are calling into ASP.NET APIs to suddenly start getting new behaviors.
        internal const string TargetFrameworkKey = "ASPNET_TARGETFRAMEWORK";

        // quick accessor for the current AppDomain's instance
        public static readonly BinaryCompatibility Current;

        static BinaryCompatibility() {
            Current = new BinaryCompatibility(AppDomain.CurrentDomain.GetData(TargetFrameworkKey) as FrameworkName);

            //TelemetryLogger.LogTargetFramework(Current.TargetFramework);
        }

        public BinaryCompatibility(FrameworkName frameworkName) {
            // parse version from FrameworkName, otherwise use a default value
            Version version = VersionUtil.FrameworkDefault;
            if (frameworkName != null && frameworkName.Identifier == ".NETFramework") {
                version = frameworkName.Version;
            }

            TargetFramework = version;
            TargetsAtLeastFramework45 = (version >= VersionUtil.Framework45);
            TargetsAtLeastFramework451 = (version >= VersionUtil.Framework451);
            TargetsAtLeastFramework452 = (version >= VersionUtil.Framework452);
            TargetsAtLeastFramework46 = (version >= VersionUtil.Framework46);
            TargetsAtLeastFramework461 = (version >= VersionUtil.Framework461);
            TargetsAtLeastFramework463 = (version >= VersionUtil.Framework463);
            TargetsAtLeastFramework472 = (version >= VersionUtil.Framework472);
            TargetsAtLeastFramework48 = (version >= VersionUtil.Framework48);
        }

        public bool TargetsAtLeastFramework45 { get; private set; }
        public bool TargetsAtLeastFramework451 { get; private set; }
        public bool TargetsAtLeastFramework452 { get; private set; }
        public bool TargetsAtLeastFramework46 { get; private set; }
        public bool TargetsAtLeastFramework461 { get; private set; }
        public bool TargetsAtLeastFramework463 { get; private set; }
        public bool TargetsAtLeastFramework472 { get; private set; }
        public bool TargetsAtLeastFramework48 { get; private set; }

        public Version TargetFramework { get; private set; }

    }
}
