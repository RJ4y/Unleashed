# Authentication flow

## Used package back end
* django-rest-framework-social-oauth2
    * ***/auth/convert-token*** third party access token to custom token
    * ***/auth/token*** refresh access token with refresh token
    * ***/auth/invalidate-sessions*** revoke all tokens of user

## Front end: login button
1. The login button is clicked
2. User is redirected to the social auth server of Google (in browser)
3. This server returns an object containing the Google access token
4. This token is sent to the API to exchange the Google access token for a custom API token
    * /auth/convert-token
    * body of POST: 
      * "grant-type": "convert-token"
      * "client_id": zie /admin
      * "client_secret": zie /admin
      * "backend": "google-oauth2"
      * "token": access token van google

## Back end
1. Validates the Google access token with Google
2. If valid: API responds with access_token, expires_in, token_type, scope, refresh_token

## Front end: save tokens
1. Save the custom API token, refresh token and expiration datetime in the AccountStore
2. Redirected to home page of the app
3. User stays authenticated until he pushes the logout button

## Front end: API calls
Every call to the API needs an authorization header (bearer) with the saved custom API access token
Before every request: 
* if expiration date of the token is < now
  * send POST with refresh token to API to get new access token
  * /auth/token
  * body:
    * "grant_type": "refresh_token"
    * "client_id": zie /admin
    * "client_secret:" zie /admin
    * "refresh_token": refresh token
  * save the new access token, refresh token and expiration date
  * add new access token to the authorization header
* if expiration date of the token is > now
  * add saved access token to the authorization header

## Front end: logout
* Send revoke request to the backend: this will revoke the access and refresh token
    * /auth/invalidate-sessions
    * with header: "Authorization: Bearer ..." (... = access token)
    * body of POST:
        * "client_id": zie /admin
    * returns {} if successfull
* Delete the saved tokens


