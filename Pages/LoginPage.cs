using OpenQA.Selenium;

namespace QA.Automation.Exercise.Pages;

public sealed class LoginPage : BasePage
{
    private readonly By _username = By.Id("user-name");
    private readonly By _password = By.Id("password");
    private readonly By _loginButton = By.Id("login-button");
    private readonly By _errorMessage = By.CssSelector("[data-test='error']");

    public LoginPage(IWebDriver driver) : base(driver) { }

    public LoginPage Open(string baseUrl)
    {
        Driver.Navigate().GoToUrl(baseUrl);
        FindVisible(_username);
        return this;
    }

    public InventoryPage LoginAs(string username, string password)
    {
        Type(_username, username);
        Type(_password, password);
        Click(_loginButton);
        return new InventoryPage(Driver);
    }

    public LoginPage AttemptLogin(string username, string password)
    {
        Type(_username, username);
        Type(_password, password);
        Click(_loginButton);
        return this;
    }

    public string ErrorMessage => FindVisible(_errorMessage).Text;
}
