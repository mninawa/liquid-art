# This will create a regcreds file to apply to the notification-service namespace 

kubectl create secret docker-registry regcred -n notification-service--docker-server=<REGISTRY> --docker-username=<USERNAME> --docker-password=<PASSWORD> --docker-email=<EMAIL_ADDRESS> --dry-run=client -o yaml > ./k8s/init-namespace/02-regcreds.yaml
