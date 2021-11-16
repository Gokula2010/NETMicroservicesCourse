echo "Stopping K8 Services..."
kubectl delete -f .\Kubernetes\platforms-depl.yaml
kubectl delete -f .\Kubernetes\platforms-np-srv.yaml
kubectl delete -f .\Kubernetes\commands-depl.yaml
kubectl delete -f .\Kubernetes\rabbitmq-depl.yaml
