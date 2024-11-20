#!/bin/bash

error=false
containers=$(sudo docker compose ps --all | sed 1d | awk '{print $1}')

for c in $containers; do
    EXIT_CODE=$(sudo docker inspect --type container $c --format='{{.State.ExitCode}}')
    
    if [ "$EXIT_CODE" -ne 0 ]; then
        echo "Container: $c, Exit Code: $EXIT_CODE"
        error=true
    fi
done

if $error; then
    echo "1 or more services failed"
    exit 1
fi

exit 0
