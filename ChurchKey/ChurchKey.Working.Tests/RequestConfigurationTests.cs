using ChurchKey.Working.Tests.TestDataObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChurchKey.Working.Tests
{
    [TestClass]
    public class RequestConfigurationTests
    {
        [TestMethod]
        public void RequestConfiguration_WantedFeel()
        {
            IRequestConfiguration<TestInputObject> configuration = default;

            configuration
                .HasHeaderWithValue("One", (o) => o.HeaderOne);

            configuration
                .HasUrlSegmentValue(":one", (o) => o.SegmentOne);

            configuration
                .HasQuery("one", (o) => o.QueryOne);

            configuration
                .HttpMethod = System.Net.Http.HttpMethod.Post;

            configuration
                .
        }
    }
}
