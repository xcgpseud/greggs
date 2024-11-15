using AutoFixture;
using AutoFixture.AutoFakeItEasy;
using NUnit.Framework;

namespace Greggs.Products.UnitTests;

public class TestBase
{
    protected Fixture Fixture;

    [SetUp]
    public void Setup()
    {
        Fixture = new Fixture();

        Fixture.Customize(new AutoFakeItEasyCustomization());
    }
}