#!/bin/bash
docker push ${REPOSITORY_PREFIX}/notification-service-api:latest
docker push ${REPOSITORY_PREFIX}/template-service:latest
docker push ${REPOSITORY_PREFIX}/registry-service:latest

