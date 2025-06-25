using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using TaskManagerAPI.Services;

namespace TaskManager.Tests
{
    public class TokenServiceTests
    {
        private IConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"Jwt:Key", "superlongsecretkey_superlongsecretkey_1234567890!"},
                {"Jwt:Issuer", "taskmanagerapi"},
                {"Jwt:Audience", "taskmanagerapi"}
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
        }

        [Test]
        public void GenerateToken_Returns_NonEmptyString()
        {
            var token = TokenService.GenerateToken("testuser", _configuration);
            Assert.That(token, Is.Not.Null.And.Not.Empty);
        }
    }
}
