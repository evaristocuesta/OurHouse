using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System.Text.RegularExpressions;

namespace OurHouse.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class OurHouseTests : PageTest
{
    private string _baseUrl = string.Empty;

    [Test]
    [TestCase("", "Welcome - La Casita del Zorro")]
    [TestCase("en", "Welcome - La Casita del Zorro")]
    [TestCase("es", "Bienvenido - La Casita del Zorro")]
    [TestCase("en/our-house", "Our house - La Casita del Zorro")]
    [TestCase("es/nuestra-casa", "Nuestra casa - La Casita del Zorro")]
    [TestCase("en/contact", "Contact - La Casita del Zorro")]
    [TestCase("es/contacto", "Contacto - La Casita del Zorro")]
    [TestCase("en/discover-cortelazor", "Discover Cortelazor - La Casita del Zorro")]
    [TestCase("es/descubre-cortelazor", "Descubre Cortelazor - La Casita del Zorro")]
    //[TestCase("pagenoexists", "Page not found - La Casita del Zorro")]
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
    [TestCase("en/our-house", "es/nuestra-casa", "lang-es")]
    [TestCase("es/nuestra-casa", "en/our-house", "lang-en")]
    [TestCase("en/contact", "es/contacto", "lang-es")]
    [TestCase("es/contacto", "en/contact", "lang-en")]
    [TestCase("en/discover-cortelazor", "es/descubre-cortelazor", "lang-es")]
    [TestCase("es/descubre-cortelazor", "en/discover-cortelazor", "lang-en")]
    public async Task ChangesToLangAsync(string origin, string target, string lang)
    {
        await Page.GotoAsync(origin);

        await Page.GetByLabel("Toggle navigation").ClickAsync();
        await Page.Locator($"id={lang}").ClickAsync();

        // Expect a url
        await Expect(Page).ToHaveURLAsync(new Regex($"{_baseUrl}{target}\\/?$"));
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