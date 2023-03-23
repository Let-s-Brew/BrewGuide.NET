namespace TestXunit;
using BrewGuide.BJCP;

public class BJCPTest
{
    [Fact]
    public void BJCPTest1()
    {
        Assert.NotNull(BJCPGuidelines.BJCP2021Guidelines);
    }
}