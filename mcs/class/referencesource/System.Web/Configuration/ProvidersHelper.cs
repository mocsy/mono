//------------------------------------------------------------------------------
// <copyright file="ProvidersHelper.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace System.Web.Configuration
{
    using System.Configuration;
    using System.Configuration.Provider;
    using System.Web.Compilation;
    using System.Collections.Specialized;
    using System;
    using System.Security;
    using System.Security.Permissions;
    

    public static class ProvidersHelper {
        ///////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////        
        public static ProviderBase InstantiateProvider(ProviderSettings providerSettings, Type providerType)
        {
            ProviderBase provider = null;
            try {
                string pnType = (providerSettings.Type == null) ? null : providerSettings.Type.Trim();
                if (string.IsNullOrEmpty(pnType))
                    throw new ArgumentException(System.Web.SR.GetString(System.Web.SR.Provider_no_type_name));
                Type t = ConfigUtil.GetType(pnType, "type", providerSettings, true, true);

                if (!providerType.IsAssignableFrom(t))
                    throw new ArgumentException(System.Web.SR.GetString(System.Web.SR.Provider_must_implement_type, providerType.ToString()));
                provider = (ProviderBase)HttpRuntime.CreatePublicInstanceByWebObjectActivator(t);

                // Because providers modify the parameters collection (i.e. delete stuff), pass in a clone of the collection
                NameValueCollection pars = providerSettings.Parameters;
                NameValueCollection cloneParams = new NameValueCollection(pars.Count, StringComparer.Ordinal);
                foreach (string key in pars)
                    cloneParams[key] = pars[key];
                provider.Initialize(providerSettings.Name, cloneParams);

                TelemetryLogger.LogProvider(t);
            } catch (Exception e) {
                if (e is ConfigurationException)
                    throw;
                throw new ConfigurationErrorsException(e.Message, e, providerSettings.ElementInformation.Properties["type"].Source, providerSettings.ElementInformation.Properties["type"].LineNumber);
            }

            return provider;
        }
        
        internal static ProviderBase InstantiateProvider(NameValueCollection providerSettings, Type providerType) {
            ProviderBase provider = null;
            try {
                string pnName = GetAndRemoveStringValue(providerSettings, "name");
                string pnType = GetAndRemoveStringValue(providerSettings, "type");
                if (string.IsNullOrEmpty(pnType))
                    throw new ArgumentException(System.Web.SR.GetString(System.Web.SR.Provider_no_type_name));
                Type t = ConfigUtil.GetType(pnType, "type", null, null, true, true);

                if (!providerType.IsAssignableFrom(t))
                    throw new ArgumentException(System.Web.SR.GetString(System.Web.SR.Provider_must_implement_type, providerType.ToString()));
                provider = (ProviderBase)HttpRuntime.CreatePublicInstanceByWebObjectActivator(t);

                // Because providers modify the parameters collection (i.e. delete stuff), pass in a clone of the collection
                NameValueCollection cloneParams = new NameValueCollection(providerSettings.Count, StringComparer.Ordinal);
                foreach (string key in providerSettings)
                    cloneParams[key] = providerSettings[key];
                provider.Initialize(pnName, cloneParams);

                TelemetryLogger.LogProvider(t);
            }
            catch (Exception e) {
                if (e is ConfigurationException)
                    throw;
                throw new ConfigurationErrorsException(e.Message, e);
            }

            return provider;
        }
        
        public static void InstantiateProviders(ProviderSettingsCollection configProviders, ProviderCollection providers, Type providerType)
        {
            foreach (ProviderSettings ps in configProviders) {
                providers.Add(InstantiateProvider(ps, providerType));
            }
        }

        private static string GetAndRemoveStringValue(NameValueCollection collection, string key) {
            string strValue = collection[key] as string;
            if (!string.IsNullOrEmpty(strValue))
                strValue = strValue.Trim();
            collection.Remove(key);
            return strValue;
        }
    }
}
