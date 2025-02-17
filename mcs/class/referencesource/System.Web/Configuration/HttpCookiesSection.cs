//------------------------------------------------------------------------------
// <copyright file="HttpCookiesSection.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace System.Web.Configuration {
    using System;
    using System.Xml;
    using System.Configuration;
    using System.Collections.Specialized;
    using System.Collections;
    using System.IO;
    using System.Text;
    using System.Security.Permissions;
    using System.Web;

    public sealed class HttpCookiesSection : ConfigurationSection {
        private static ConfigurationPropertyCollection _properties;
        private static readonly ConfigurationProperty _propHttpOnlyCookies =
            new ConfigurationProperty("httpOnlyCookies", typeof(bool), false, ConfigurationPropertyOptions.None);
        private static readonly ConfigurationProperty _propRequireSSL =
            new ConfigurationProperty("requireSSL", typeof(bool), false, ConfigurationPropertyOptions.None);
        private static readonly ConfigurationProperty _propDomain =
            new ConfigurationProperty("domain", typeof(string), String.Empty, ConfigurationPropertyOptions.None);
        private static readonly ConfigurationProperty _propSameSite =
            new ConfigurationProperty("sameSite", typeof(SameSiteMode), (SameSiteMode)(-1) /* Unspecified */, new SameSiteConverter(), null, ConfigurationPropertyOptions.None);

                /*         <!--
                httpCookies Attributes:
                  httpOnlyCookies="[true|false]" - enables output of the "HttpOnly" cookie attribute
                  requireSSL="[true|false]" - enables output of the "secure" cookie attribute as described in RFC 2109
                  domain="[domain]" - enables output of the "domain" cookie attribute set to the specified value
                  sameSite="[None|Lax|Strict|Unspecified]" - Set SameSite cookie headers to the given value, or omit the header entirely.
                -->
                <httpCookies
                    httpOnlyCookies="false"
                    requireSSL="false"
                    sameSite="Unspecified"
        />
  */
        static HttpCookiesSection() {
            // Property initialization
            _properties = new ConfigurationPropertyCollection();
            _properties.Add(_propHttpOnlyCookies);
            _properties.Add(_propRequireSSL);
            _properties.Add(_propDomain);
            _properties.Add(_propSameSite);
        }

        public HttpCookiesSection() {
        }


        protected internal override ConfigurationPropertyCollection Properties {
            get {
                return _properties;
            }
        }

        [ConfigurationProperty("httpOnlyCookies", DefaultValue = false)]
        public bool HttpOnlyCookies {
            get {
                return (bool)base[_propHttpOnlyCookies];
            }
            set {
                base[_propHttpOnlyCookies] = value;
            }
        }

        [ConfigurationProperty("requireSSL", DefaultValue = false)]
        public bool RequireSSL {
            get {
                return (bool)base[_propRequireSSL];
            }
            set {
                base[_propRequireSSL] = value;
            }
        }

        [ConfigurationProperty("domain", DefaultValue = "")]
        public string Domain {
            get {
                return (string)base[_propDomain];
            }
            set {
                base[_propDomain] = value;
            }
        }

        [ConfigurationProperty("sameSite", DefaultValue = (SameSiteMode)(-1) /* Unspecified */)]
        public SameSiteMode SameSite {
            get {
                return (SameSiteMode)base[_propSameSite];
            }
            set {
                base[_propSameSite] = value;
            }
        }
    }
}
