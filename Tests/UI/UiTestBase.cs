using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace QA.Automation.Exercise.Tests.UI;

public abstract class UiTestBase
{
    protected IWebDriver Driver { get; private set; } = null!;

    [SetUp]
public void StartBrowser()
{
    var options = new ChromeOptions();

    options.AddArgument("--window-size=1440,1000");

    // Disable Chrome password manager prompts during automation
    options.AddUserProfilePreference("credentials_enable_service", false);
    options.AddUserProfilePreference("profile.password_manager_enabled", false);
    options.AddUserProfilePreference("profile.password_manager_leak_detection", false);

    if (string.Equals(
        Environment.GetEnvironmentVariable("HEADLESS"),
        "true",
        StringComparison.OrdinalIgnoreCase))
    {
        options.AddArgument("--headless=new");
    }

    Driver = new ChromeDriver(options);
    Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
}

    [TearDown]
    public void StopBrowser()
    {
        if (Driver is null)
            return;

        try
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed &&
                Driver is ITakesScreenshot screenshotDriver)
            {
                var directory = Path.Combine(TestContext.CurrentContext.WorkDirectory, "artifacts", "screenshots");
                Directory.CreateDirectory(directory);
                var safeName = string.Concat(TestContext.CurrentContext.Test.Name.Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c));
                screenshotDriver.GetScreenshot().SaveAsFile(Path.Combine(directory, $"{safeName}.png"));
            }
        }
        finally
        {
            Driver.Quit();
            Driver.Dispose();
        }
    }
}
