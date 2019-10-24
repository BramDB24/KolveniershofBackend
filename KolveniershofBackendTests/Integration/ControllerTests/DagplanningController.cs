using kolveniershofBackend;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KolveniershofBackendTests.Integration.ControllerTests
{
    public class DagplanningController: IClassFixture<TestFixture<Startup>>
    {
        private HttpClient httpclient;

        public DagplanningController(TestFixture<Startup> fixture)
        {
            httpclient = fixture.Client;
        }

        [Fact]
        public async Task TestOne()
        {
            var request = "api/dagplanning/2019-10-24";
            var response = await httpclient.GetAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}
