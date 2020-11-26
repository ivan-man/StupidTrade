using Common.Helpers;
using NUnit.Framework;

namespace CommonTests
{
    public class HashHelperTests
    {
        [Test]
        public void HashHelper_GetMD5Hash_Equal()
        {
            var obj1 = new { Id = 1, Name = "name" };
            var obj2 = new { Id = 1, Name = "name" };

            var hash1 = HashHelper.GetMD5Hash(obj1, true, "test");
            var hash2 = HashHelper.GetMD5Hash(obj2, true, "test");

            var result = hash1.Equals(hash2);

            Assert.True(result);
        }

        [Test]
        public void HashHelper_GetMD5Hash_NotEqual()
        {
            var obj1 = new { Id = 1, Name = "name" };
            var obj2 = new { Id = 1, Name = "name" };

            var hash1 = HashHelper.GetMD5Hash(obj1, true, "test");
            var hash2 = HashHelper.GetMD5Hash(obj2, false, "test");

            var result = hash1.Equals(hash2);

            Assert.False(result);
        }
    }
}