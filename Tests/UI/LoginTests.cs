using NUnit.Framework;
using QA.Automation.Exercise.Pages;
using QA.Automation.Exercise.Support;

namespace QA.Automation.Exercise.Tests.UI;

[TestFixture]
[Category("UI")]
public sealed class LoginTests : UiTestBase
{
    [Test]
    public void LockedOutUser_CannotLogin_AndSeesUsefulErrorMessage()
    {
        var loginPage = new LoginPage(Driver)
            .Open(TestSettings.UiBaseUrl)
            .AttemptLogin(TestSettings.LockedOutUser, TestSettings.Password);

        Assert.That(loginPage.ErrorMessage, Does.Contain("locked out"));
    }
}
