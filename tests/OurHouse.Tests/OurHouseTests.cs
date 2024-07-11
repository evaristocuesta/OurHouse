using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace OurHouse.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class OurHouseTests : PageTest
{
    [Test]
    [TestCase("", "Welcome - Casa Espejo")]
    [TestCase("en", "Welcome - Casa Espejo")]
    [TestCase("es", "Bienvenido - Casa Espejo")]
    [TestCase("en/ourhouse", "Our house - Casa Espejo")]
    [TestCase("es/nuestracasa", "Nuestra casa - Casa Espejo")]
    public async Task HasTitleAsync(string url, string title)
    {
        await Page.GotoAsync(url);

        // Expect a title 
        await Expect(Page).ToHaveTitleAsync(title);
    }

    public override BrowserNewContextOptions ContextOptions()
    {
        return new BrowserNewContextOptions()
        {
            BaseURL = TestContext.Parameters["BaseUrl"]         
        };
    }
}