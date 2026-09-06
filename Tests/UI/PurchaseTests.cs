using NUnit.Framework;
using QA.Automation.Exercise.Pages;
using QA.Automation.Exercise.Support;

namespace QA.Automation.Exercise.Tests.UI;

[TestFixture]
[Category("UI")]
public sealed class PurchaseTests : UiTestBase
{
    [Test]
    public void StandardUser_CanPurchaseAProductSuccessfully()
    {
        const string productName = "Sauce Labs Backpack";

        var inventory = new LoginPage(Driver)
            .Open(TestSettings.UiBaseUrl)
            .LoginAs(TestSettings.StandardUser, TestSettings.Password);

        Assert.That(inventory.IsLoaded, Is.True, "Inventory page should load after a valid login.");

        var cart = inventory
            .AddProductToCart(productName)
            .OpenCart();

        Assert.That(cart.ProductNames, Does.Contain(productName));

        var confirmation = cart
            .Checkout()
            .EnterCustomerDetails("QA", "Engineer", "NE1 1AA")
            .FinishOrder();

        Assert.That(confirmation.ConfirmationMessage, Is.EqualTo("Thank you for your order!"));
    }
}
