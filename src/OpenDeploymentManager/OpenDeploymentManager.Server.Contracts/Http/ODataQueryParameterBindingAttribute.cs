﻿using System;
using System.Globalization;
using System.Net.Http;
using System.Text;

namespace OpenDeploymentManager.Server.Contracts.Http
{
    public class ODataQueryParameterBindingAttribute : HttpParameterBindingAttribute
    {
        #region Overrides of ParameterBindingAttribute
        public override void BindParameter(HttpParameterDescriptor parameter, HttpRequestMessage request)
        {
            var query = request.GetUriQuery();
            var odataQuery = (IPagingQuery)parameter.ParameterValue;

            if (odataQuery.Top != 0)
            {
                query.Add("$top", odataQuery.Top);
            }

            if (odataQuery.Skip != 0)
            {
                query.Add("$top", odataQuery.Top);
            }
        }
        #endregion
    }
}