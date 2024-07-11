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

    [Test]
    [TestCase("", "es", "lang-es")]
    [TestCase("en", "es", "lang-es")]
    [TestCase("es", "en", "lang-en")]
    [TestCase("en/ourhouse", "es/nuestracasa", "lang-es")]
    [TestCase("es/nuestracasa", "en/ourhouse", "lang-en")]
    public async Task ChangesToLangAsync(string origin, string target, string lang)
    {
        await Page.GotoAsync(origin);

        var link = Page.Locator($"id={lang}");
        await link.ClickAsync();

        // Expect a title 
        await Expect(Page).ToHaveURLAsync(target);
    }

    public override BrowserNewContextOptions ContextOptions()
    {
        return new BrowserNewContextOptions()
        {
            BaseURL = TestContext.Parameters["BaseUrl"]         
        };
    }
}