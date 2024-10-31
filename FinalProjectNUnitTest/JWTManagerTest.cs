using FinalProject.Core.Customer.Exceptions;
using Microsoft.Extensions.Configuration;
using Moq;
using FinalProject.Core.JWT;
using System.Diagnostics.CodeAnalysis;
using FinalProject;
using System.Text.Json;
using FinalProject.Core.JWT.Interfaces;

namespace FinalProjectNUnitTest
{
    public class JWTManagerTest
    {
        private readonly Mock<IGetOptionManager> _mockGetOptionManager;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<IConfigurationSection> _mockConfigurationSectionWrapper;
        public JWTManagerTest()
        {
            _mockGetOptionManager = new Mock<IGetOptionManager>(MockBehavior.Strict);
            _mockConfiguration = new Mock<IConfiguration>(MockBehavior.Strict);
            _mockConfigurationSectionWrapper = new Mock<IConfigurationSection>(MockBehavior.Strict);
        }

        [Test]
        public void JWTGenerateWithNullData()
        {
            string? test = null;
            var manager = new JWTManager(_mockGetOptionManager.Object);

            void Act()
            {
                manager.JWTGenerate(test);
            }

            Assert.Throws<ArgumentNullException>(Act);
        }

        [Test]
        public void JWTGenerateWithEmptyData()
        {
            string test = "";
            var manager = new JWTManager(_mockGetOptionManager.Object);

            void Act()
            {
                manager.JWTGenerate(test);
            }

            Assert.Throws<ArgumentNullException>(Act);
        }
        
        [Test]
        public void JWTGenerateWithValidData()
        {
            TokenOptions testToken = new TokenOptions
            {
                Secret = "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest",
                Issuer = "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest",
                Audience = "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest",
                ExpiryDays = 7,
            };
            string testData = "testtesttesttesttesttesttesttesttest";
            var manager = new JWTManager(_mockGetOptionManager.Object);
            _mockGetOptionManager.Setup(x => x.GetTokenOptions()).Returns(testToken);

            void Act()
            {
                manager.JWTGenerate(testData);
            }

            Assert.DoesNotThrow(Act);
        }

        [Test]
        public void GetTokenCheck()
        {
            var manager = new GetOptionManager(_mockConfiguration.Object);
            _mockConfiguration.Setup(x => x.GetSection(It.IsAny<string>())).Returns(_mockConfigurationSectionWrapper.Object);
            _mockConfigurationSectionWrapper.Setup(x => x.GetChildren()).Returns(Enumerable.Empty<IConfigurationSection>);
            _mockConfigurationSectionWrapper.Setup(x => x.Value).Returns("");
            _mockConfigurationSectionWrapper.Setup(x => x.Key).Returns("");
            _mockConfigurationSectionWrapper.Setup(x => x.Path).Returns("");

            void Act()
            {
                manager.GetTokenOptions();
            }

            Assert.DoesNotThrow(Act);
        }
    }
}