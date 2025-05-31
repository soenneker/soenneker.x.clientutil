using Soenneker.X.ClientUtil.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.X.ClientUtil.Tests;

[Collection("Collection")]
public sealed class XClientUtilTests : FixturedUnitTest
{
    private readonly IXClientUtil _util;

    public XClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IXClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
