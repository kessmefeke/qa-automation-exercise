using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace QA.Automation.Exercise.Pages;

public abstract class BasePage
{
    protected IWebDriver Driver { get; }
    protected WebDriverWait Wait { get; }

    protected BasePage(IWebDriver driver, TimeSpan? timeout = null)
    {
        Driver = driver;
        Wait = new WebDriverWait(driver, timeout ?? TimeSpan.FromSeconds(10));
    }

    protected IWebElement FindVisible(By locator) =>
        Wait.Until(driver =>
        {
            var element = driver.FindElement(locator);
            return element.Displayed ? element : null;
        })!;

    protected void Click(By locator) => FindVisible(locator).Click();

    protected void Type(By locator, string value)
    {
        var element = FindVisible(locator);
        element.Clear();
        element.SendKeys(value);
    }
}
