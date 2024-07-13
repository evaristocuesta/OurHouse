using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace OurHouse.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class OurHouseTests : PageTest
{
    private string _baseUrl = string.Empty;

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
    [TestCase("", "es/", "Español")]
    [TestCase("en", "es/", "Español")]
    [TestCase("es", "en/", "English")]
    [TestCase("en/ourhouse", "es/nuestracasa/", "Español")]
    [TestCase("es/nuestracasa", "en/ourhouse/", "English")]
    public async Task ChangesToLangAsync(string origin, string target, string lang)
    {
        await Page.GotoAsync(origin);

        await Page.GetByLabel("Toggle navigation").ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = lang }).ClickAsync();

        // Expect a url
        await Expect(Page).ToHaveURLAsync($"{_baseUrl}{target}");
    }

    public override BrowserNewContextOptions ContextOptions()
    {
        _baseUrl = TestContext.Parameters["BaseUrl"] ?? string.Empty;

        return new BrowserNewContextOptions()
        {
            BaseURL = _baseUrl        
        };
    }
}