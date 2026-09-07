# QA Automation Engineer Practical Exercise

A small C# / NUnit automation suite covering API and UI scenarios from the supplied technical exercise.

The framework is intentionally lightweight. The focus is on readable tests, clear separation of responsibilities, appropriate negative coverage, and CI-ready execution rather than exhaustive test coverage.

## What is covered

### API — Restful-Booker

1. **Create and retrieve a booking** — creates unique test data, validates the create response, then retrieves the booking and verifies that it persisted correctly.

2. **Retrieve a non-existent booking** — validates that the API returns `404 Not Found` and that the client handles an empty error response cleanly.

### UI — Sauce Demo

1. **Successful purchase journey** — valid login → add product → verify cart → checkout → verify completion message.

2. **Locked-out user login** — validates a negative authentication path and checks the user-facing error message.

## Tech stack

- .NET 8
- C#
- NUnit
- Selenium WebDriver
- `HttpClient` + `System.Text.Json` for API testing
- Selenium Manager for browser-driver management
- GitHub Actions for CI

I deliberately used `HttpClient` rather than adding an API client library such as RestSharp. The API surface in this exercise is small, and the built-in .NET client keeps dependencies and abstractions minimal while still allowing a reusable API client layer.

## Project structure

````text
Api/                 API client abstractions
Models/              Request/response models
Pages/               Selenium Page Objects
Support/             Shared configuration
Tests/Api/           API tests
Tests/UI/            UI tests and browser lifecycle
.github/workflows/   CI workflow


### Chrome automation note

The browser session disables Chrome password-manager and password-leak
prompts because Sauce Demo uses publicly known demo credentials. These
browser-level prompts are outside the application under test and can
otherwise interfere with Selenium execution.

Selenium Manager is used to resolve the correct ChromeDriver version
automatically, so a manually installed ChromeDriver is not required.

## Tech stack

- .NET 8
- C#
- NUnit
- Selenium WebDriver
- `HttpClient` + `System.Text.Json` for API testing
- Selenium Manager for browser-driver management

I deliberately used `HttpClient` rather than adding an API client library such as RestSharp. The API surface in this exercise is small, and the built-in .NET client keeps dependencies and abstractions minimal while still allowing a reusable API client layer.

## Project structure

```text
Api/                 API client abstractions
Models/              Request/response models
Pages/               Selenium Page Objects
Support/             Shared configuration
Tests/Api/           API tests
Tests/UI/            UI tests and browser lifecycle
````

## Page Object Model approach

Page Objects own locators and UI interactions. Tests own the business scenario and assertions.

For example, `LoginPage` knows how to enter credentials and submit the form, while the test decides whether the expected outcome is a successful login or a locked-user error. This avoids putting test assertions inside Page Objects and keeps both layers easier to change independently.

I have kept the objects relatively small rather than creating a generic Selenium wrapper for every action. A larger production framework could justify more shared components, but for this exercise that would add abstraction without enough benefit.

## Running the tests

Prerequisites:

- .NET 8 SDK
- Google Chrome

Run everything:

```bash
dotnet test
```

Run API tests only:

```bash
dotnet test --filter TestCategory=API
```

Run UI tests only:

```bash
dotnet test --filter TestCategory=UI
```

Run UI tests headlessly (PowerShell):

```powershell
$env:HEADLESS="true"
dotnet test --filter TestCategory=UI
```

Run UI tests headlessly (bash):

```bash
HEADLESS=true dotnet test --filter TestCategory=UI
```

On a UI failure, a screenshot is written beneath `artifacts/screenshots` in the test working directory.

## Test design / trade-offs

- The positive API test uses unique data to reduce collision risk on a shared public API.
- The negative API test checks the HTTP contract rather than only deserialised content.
- UI tests use explicit waits rather than fixed sleeps.
- Stable `id` / `data-test` attributes are preferred over brittle XPath or layout-based selectors.
- Assertions remain in the test classes rather than being hidden inside Page Objects.
- The framework supports headless execution for CI without forcing headless mode during local debugging.
- The suite is intentionally small because the brief prioritises quality and structure over exhaustive coverage.

Because these are public demo systems, availability and shared-state behaviour are outside the repository's control. In a production environment I would run against controlled test environments and owned test data.

## What I would build next

With more time, I would consider:

environment-specific configuration using environment variables or appsettings rather than constants;
API cleanup for test-created data where reliable authentication is available;
richer diagnostics, including API request/response logging and browser console/network evidence;
publishing test results in a more readable CI report;
parallel execution after confirming test-data and application isolation;
cross-browser coverage for the highest-value UI journeys;
a browser/environment matrix where the additional execution cost is justified;
retry handling only for explicitly identified transient infrastructure failures, never to hide genuine assertion failures.

For a larger production suite I would also consider test tagging by purpose, such as smoke/regression, and use those categories to control which suites run at different stages of the delivery pipeline.

## AI usage

AI-assisted code was treated in the same way as other code: reviewed, executed, debugged and adjusted before inclusion.

## CI/CD

A GitHub Actions workflow runs automatically on pushes and pull requests
to the `main` branch.

The pipeline:

1. Restores NuGet dependencies
2. Builds the solution in Release mode
3. Runs API tests
4. Runs UI tests in headless Chrome
5. Uploads test result files
6. Uploads Selenium screenshots if UI execution fails

The same test code is used locally and in CI. The `HEADLESS`
environment variable controls whether Chrome runs with a visible UI.

At the time of submission, all four automated tests pass locally and in the GitHub Actions pipeline.
