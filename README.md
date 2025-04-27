# bite-rush
API for a Food Delivery Application

#1 Migrations
1.)dotnet ef migrations add <name> --verbose -s E:\bite-rush\src\Web.Api -p E:\bite-rush\src\Infrastructure
2.)run the project and it will be applied automatically.

#2 Testing Webhooks
https://docs.stripe.com/stripe-cli?install-method=windows
Login with CLI:
1.)stripe login
2.)stripe listen --forward-to https://localhost:5001/stripe/webhooks

Trigger event with CLI:
stripe trigger product.created
stripe trigger price.created



