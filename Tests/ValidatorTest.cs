using Logic.Service.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class ValidatorTest
    {
        [TestMethod]
        public void KontrolleraUrl()
        {
            var result = Validator.CheckUrlContainsRssOrXml("");

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void KontrolleraOkUrl()
        {
            var result = Validator.CheckUrlContainsRssOrXml("xml");

            Assert.AreEqual(true, result);
        }
    }
}
