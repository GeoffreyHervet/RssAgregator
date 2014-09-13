using System;
using NUnit.Framework;
using Nancy;
using Nancy.Testing;

namespace NancyTutorial.Web.Tests
{
    [TestFixture]
    public class SourceModuleTests
    {
        private readonly Browser _browser;

        public SourceModuleTests()
        {
            var bootstrapper = new DefaultNancyBootstrapper();
            this._browser = new Browser(bootstrapper);
        }

        protected BrowserResponse Go_to_route_with_xml(String route)
        {
            return this._browser.Get(route, with =>
            {
                with.HttpRequest();
                with.Header("accept", "application/xml");
            });
        }

        [Test]
        public void Should_return_status_ok_when_route_exists()
        {
            Assert.AreEqual(HttpStatusCode.OK, Go_to_route_with_xml("/sources").StatusCode);
        }

        [Test]
        public void Should_get_a_source()
        {
            Assert.AreEqual(HttpStatusCode.OK, Go_to_route_with_xml("/source/1").StatusCode);
        }

        [Test]
        public void Should_get_a_source_with_correct_id()
        {
            // With
            var result = Go_to_route_with_xml("/source/1");
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(result.BodyAsXml().Element("Source").Element("Id").Value, "1");
        }
    }
}