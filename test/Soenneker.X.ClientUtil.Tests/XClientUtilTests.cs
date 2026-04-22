using Soenneker.X.ClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.X.ClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class XClientUtilTests : HostedUnitTest
{
    private readonly IXClientUtil _util;

    public XClientUtilTests(Host host) : base(host)
    {
        _util = Resolve<IXClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
