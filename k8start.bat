echo "Starting K8 Services..."
kubectl apply -f .\Kubernetes\platforms-depl.yaml
kubectl apply -f .\Kubernetes\platforms-np-srv.yaml
kubectl apply -f .\Kubernetes\commands-depl.yaml
kubectl apply -f .\Kubernetes\rabbitmq-depl.yaml
