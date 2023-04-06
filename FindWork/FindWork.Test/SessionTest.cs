using FindWork.Test.Helpers;
using FluentAssertions;

namespace FindWork.Test;

public class SessionTest : BaseTest
{
    [Test]
    public async Task Test1()
    {
        using (var scope = Helper.CreateTransactionScope())
        {
            var session1 = await session.GetSession();
            var dalSession = await sessionDal.Get(session1.DbSessionId);

            dalSession.Should().NotBeNull();
            session1.DbSessionId.Should().Be(dalSession.DbSessionId);

            var session2 = await session.GetSession();
            session2.DbSessionId.Should().Be(session1.DbSessionId);
        }
    }
}