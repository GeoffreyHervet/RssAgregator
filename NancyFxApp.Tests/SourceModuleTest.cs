using System;
using NUnit.Framework;
using Nancy;
using Nancy.Testing;

namespace NancyTutorial.Web.Tests
{
    [TestFixture]
    public class SourceModuleTests
    {
        protected BrowserResponse Go_to_route_with_xml(String route)
        {
            var bootstrapper = new DefaultNancyBootstrapper();
            var browser = new Browser(bootstrapper);
            return browser.Get(route, with =>
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
            Console.WriteLine(Go_to_route_with_xml("/source/1").StatusCode);
            Assert.AreEqual(HttpStatusCode.OK, Go_to_route_with_xml("/source/1").StatusCode);
        }
    }
}