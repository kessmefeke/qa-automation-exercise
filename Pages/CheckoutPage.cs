using OpenQA.Selenium;

namespace QA.Automation.Exercise.Pages;

public sealed class CheckoutPage : BasePage
{
    private readonly By _firstName = By.Id("first-name");
    private readonly By _lastName = By.Id("last-name");
    private readonly By _postalCode = By.Id("postal-code");
    private readonly By _continueButton = By.Id("continue");
    private readonly By _finishButton = By.Id("finish");

    public CheckoutPage(IWebDriver driver) : base(driver) { }

    public CheckoutPage EnterCustomerDetails(string firstName, string lastName, string postalCode)
    {
        Type(_firstName, firstName);
        Type(_lastName, lastName);
        Type(_postalCode, postalCode);
        Click(_continueButton);
        return this;
    }

    public CheckoutCompletePage FinishOrder()
    {
        Click(_finishButton);
        return new CheckoutCompletePage(Driver);
    }
}
