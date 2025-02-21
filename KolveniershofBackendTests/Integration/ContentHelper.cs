﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace KolveniershofBackendTests.Integration
{
    class ContentHelper
    {
        public static StringContent GetStringContent(object obj)
          => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");
    }
}
