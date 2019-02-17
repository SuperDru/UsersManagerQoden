using Xunit;

namespace UsersManager.Test
{
    [CollectionDefinition(Name)]
    public class ApiCollectionFixture : ICollectionFixture<ApiFixture>
    {
        public const string Name = "ApiCollectionFixture";
    }
}