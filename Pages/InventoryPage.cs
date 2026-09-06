using OpenQA.Selenium;

namespace QA.Automation.Exercise.Pages;

public sealed class InventoryPage : BasePage
{
    private readonly By _title = By.CssSelector("[data-test='title']");
    private readonly By _cartLink = By.CssSelector("[data-test='shopping-cart-link']");

    public InventoryPage(IWebDriver driver) : base(driver) { }

    public bool IsLoaded => FindVisible(_title).Text.Equals("Products", StringComparison.OrdinalIgnoreCase);

    public InventoryPage AddProductToCart(string productName)
    {
        var item = Wait.Until(driver => driver.FindElements(By.CssSelector("[data-test='inventory-item']"))
            .FirstOrDefault(element => element.Text.Contains(productName, StringComparison.OrdinalIgnoreCase)))
            ?? throw new NoSuchElementException($"Product '{productName}' was not found.");

        item.FindElement(By.TagName("button")).Click();
        return this;
    }

    public CartPage OpenCart()
    {
        Click(_cartLink);
        return new CartPage(Driver);
    }
}
