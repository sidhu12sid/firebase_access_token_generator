# FCM PUSH NOTIFICATION ACCESS TOKEN GENERATOR

This is a simple dotnet minimal api for getting the access token for sending the push notification thorugh firebase.
All you have to do is to psas the service account json. In return you will get the accesstoken.

# CURL TO THE ACCESS TOKEN API
`curl -X POST https://localhost:44351/get-token \
  -H "Content-Type: application/json" \
  -d @serviceAccount.json
`
You can pass the service account json and get the accesstoken

- Then you can pass the accesstoken in the FCM Api to send the requests

`curl --location 'https://fcm.googleapis.com/v1/projects/your-project-id/messages:send' \
--header 'Authorization: your_access_token
--header 'Content-Type: application/json' \
--data '{
  "message": {
    "notification": {
      "body": "YOUR_BODY_HERE",
      "title": "YOUR_TITLE_HERE"
    },
    "data": {
      "type_id": "123",
      "type": "example_type",
      "notification_type": "nType_value",
      "is_english": "1",
      "is_arabic": "0",
      "request_no": "REQ001"
    },
    "token":"your_device_token"
  }
}'`
