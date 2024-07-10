using System.Text.RegularExpressions;
using Microsoft.Playwright.NUnit;

namespace OurHouse.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class WelcomeTests : PageTest
{
    [Test]
    public async Task HasTitleEnglishAsync()
    {
        await Page.GotoAsync("https://evaristocuesta.github.io/OurHouse");

        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync(new Regex("Home - Casa Espejo"));
    }

    [Test]
    public async Task HasTitleSpanishAsync()
    {
        await Page.GotoAsync("https://evaristocuesta.github.io/OurHouse/es");

        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync(new Regex("Inicio - Casa Espejo"));
    }
}