using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DemoApp.Core.ViewModel
{
    public class BaseViewModel<T>
    {
        public BaseViewModel()
        {
            StatusCode = HttpStatusCode.OK;
        }

        public BaseViewModel(T dataModel)
        {
            Data = dataModel;
            StatusCode = HttpStatusCode.OK;
        }

        [JsonProperty(Order = 3, NullValueHandling = NullValueHandling.Ignore)]
        public HttpStatusCode StatusCode { get; set; }

        [JsonProperty(Order = 4, NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }

        [JsonProperty(Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        [JsonProperty(Order = 2, NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }


    }
}
