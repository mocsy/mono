//------------------------------------------------------------------------------
// <copyright file="System.Web.Util.ExceptionUtil.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace System.Web.Util {
    using System;
    using System.Web;

    static internal class ExceptionUtil {
        static internal ArgumentException ParameterInvalid(string parameter) {
            return new ArgumentException(System.Web.SR.GetString(System.Web.SR.Parameter_Invalid, parameter), parameter);
        }

        static internal ArgumentException ParameterNullOrEmpty(string parameter) {
            return new ArgumentException(System.Web.SR.GetString(System.Web.SR.Parameter_NullOrEmpty, parameter), parameter);
        }

        static internal ArgumentException PropertyInvalid(string property) {
            return new ArgumentException(System.Web.SR.GetString(System.Web.SR.Property_Invalid, property), property);
        }

        static internal ArgumentException PropertyNullOrEmpty(string property) {
            return new ArgumentException(System.Web.SR.GetString(System.Web.SR.Property_NullOrEmpty, property), property);
        }

        static internal InvalidOperationException UnexpectedError(string methodName) {
            return new InvalidOperationException(System.Web.SR.GetString(System.Web.SR.Unexpected_Error, methodName));
        }
    }
}

