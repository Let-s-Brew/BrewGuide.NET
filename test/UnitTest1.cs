namespace TestXunit;
using BrewCode.BrewGuide.BJCP;

public class BJCPTest
{
    [Fact]
    public void BJCPTest1()
    {
        Assert.NotNull(Guidelines.BJCP2021Guidelines);
    }
}