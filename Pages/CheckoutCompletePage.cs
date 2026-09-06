using OpenQA.Selenium;

namespace QA.Automation.Exercise.Pages;

public sealed class CheckoutCompletePage : BasePage
{
    private readonly By _completeHeader = By.CssSelector("[data-test='complete-header']");

    public CheckoutCompletePage(IWebDriver driver) : base(driver) { }

    public string ConfirmationMessage => FindVisible(_completeHeader).Text;
}
