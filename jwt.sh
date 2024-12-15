#!/bin/bash

source .env

ACCESS_TOKEN=$(curl -s -X POST "$API_URL" \
  -H "apikey: $API_KEY" \
  -H "Content-Type: application/json" \
  -d '{ "email": "'$EMAIL'", "password": "'$PASSWORD'" }' | jq -r '.access_token')

if [[ "$ACCESS_TOKEN" != "null" && -n "$ACCESS_TOKEN" ]]; then
  echo -n "$ACCESS_TOKEN" | xclip -selection clipboard
  echo "Access token copied to clipboard."
else
  echo "Failed to fetch access token. Please check your credentials and API key."
fi

