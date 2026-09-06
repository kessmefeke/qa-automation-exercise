using OpenQA.Selenium;

namespace QA.Automation.Exercise.Pages;

public sealed class CartPage : BasePage
{
    private readonly By _cartItems = By.CssSelector("[data-test='inventory-item-name']");
    private readonly By _checkoutButton = By.Id("checkout");

    public CartPage(IWebDriver driver) : base(driver) { }

    public IReadOnlyCollection<string> ProductNames =>
        Wait.Until(driver => driver.FindElements(_cartItems).Select(item => item.Text).ToArray());

    public CheckoutPage Checkout()
    {
        Click(_checkoutButton);
        return new CheckoutPage(Driver);
    }
}
