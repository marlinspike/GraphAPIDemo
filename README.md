## Graph API Demo
A demonstration of using the Graph API SDK to query the MS Graph API for various types of info about a user

## Scopes
As configured, the app queries the following scopes (configured in the appsettings.json file):
- User.Read
- Calendars.Read
- Group.Read.All
- Mail.Read
- Member.Read.Hidden
- Contacts.Read

To learn how to configure scopes, see the [Microsoft Graph permissions reference](https://docs.microsoft.com/en-us/graph/permissions-reference#contacts-permissions)

## Prerequisites
- Create an Azure AD Application in your tenant
- Click the **Authentication** section in your App Registration, and toggle the *Treat application as a public client* setting to True

## How to use
- Run the app
- You will need to use the console login flow (follow the directions in the console to go to the MS login page, using the unique string as the passcode)
